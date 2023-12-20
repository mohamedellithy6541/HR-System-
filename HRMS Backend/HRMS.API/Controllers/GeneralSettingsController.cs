using HRMS.Application.Repository;
using HRMS.Application.Models;
using HRMS.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.Models.GeneralSettingDTO;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.Collections;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeneralSettingsController : ControllerBase
    {
        IGeneralSettingRepository generalSettingRepository;
        IGenaricrepository<Employee> genaricrepository2;
        public GeneralSettingsController(IGeneralSettingRepository generalSettingRepository, IGenaricrepository<Employee> genaricrepository2)
        {
            this.genaricrepository2 = genaricrepository2;
            this.generalSettingRepository = generalSettingRepository;

        }
        [HttpGet]
        [Authorize(Policy = "Permission:GeneralSettings.View")]
        public ActionResult GeneralSettings()
        {
            int? id = generalSettingRepository.GetGeneralSettingingID();
            Console.WriteLine(id);
            if (id != null)
            {
                var data = generalSettingRepository.GetById((int)id);
                GeneralDataDTO general = new GeneralDataDTO()
                {
                    Id = (int)id,
                    Bonus = data.Bonus,
                    Discount = data.Penality,
                    VacationDay1 = data.VacationDay1,
                    VacationDay2 = data.VacationDay2
                };
                return Ok(general);
            }
            else
            {
                return NotFound("There's No GeneralSetting Yet");
            }

        }
        [HttpGet]
        [Route("/GeneralSetting/getbyempid/{empID:int}")]
        [Authorize(Policy = "Permission:GeneralSettings.View")]
        public ActionResult GetSettingByEmpID(int empID)
        {
            if (empID != 0)
            {
                var empData = genaricrepository2.GetById(empID);
                if (empData != null)
                {
                    var settingID = empData.SpecialSetting;
                    if (settingID != null)
                    {
                        var settingData = generalSettingRepository.GetById((int)settingID);
                        GeneralDataDTO general = new GeneralDataDTO()
                        {
                            Id = settingData.Id,
                            Bonus = settingData.Bonus,
                            Discount = settingData.Penality,
                            EmployeeID = empID,
                            VacationDay1 = settingData.VacationDay1,
                            VacationDay2 = settingData.VacationDay2
                        };
                        return Ok(general);
                    }
                    else
                    {
                        return NotFound("employee has no special settings");
                    }
                }
            }
            return NotFound("this employee id not found");
        }
        [HttpGet]
        [Route("/GeneralSetting/getbyid/{id:int}")]
        [Authorize(Policy = "Permission:GeneralSettings.View")]
        public ActionResult GetSettingByID(int id)
        {
            if (id != 0)

            {

                var settingData = generalSettingRepository.GetById(id);
                if (settingData != null)
                {
                    GeneralDataDTO general = new GeneralDataDTO()
                    {
                        Id = settingData.Id,
                        Bonus = settingData.Bonus,
                        Discount = settingData.Penality,
                        VacationDay1 = settingData.VacationDay1,
                        VacationDay2 = settingData.VacationDay2
                    };
                    return Ok(general);
                }

            }
            return NotFound("this id is not available");

        }
        [HttpPost]
        [Route("/General/SaveNew")]
        [Authorize(Policy ="Permission:GeneralSettings.Create")]

        public async Task<ActionResult> AddGeneralSettings(GeneralDataDTO Data)
        {
            GeneralSettings exist;
            bool old = true;
            exist = generalSettingRepository.IsExist(Data);
            //check if not exist ===> create 
            if (exist == null)
            {
                exist = new GeneralSettings();
                old = false;
                exist.Bonus = Data.Bonus;
                exist.Penality = Data.Discount;
                exist.VacationDay1 = Data.VacationDay1;
                exist.VacationDay2 = Data.VacationDay2;
                generalSettingRepository.Create(exist);
                exist = generalSettingRepository.IsExist(Data);
            }
            //pass to employee

            if (Data.EmployeeID != 0)
            {
                var empData = genaricrepository2.GetById(Data.EmployeeID);
                empData.SpecialSetting = exist.Id;
                genaricrepository2.Edite(empData);
            }
            else
            {
                IEnumerable<Employee> employees = await genaricrepository2.GetAllAsync();
                foreach (var employee in employees)
                {
                    employee.SpecialSetting = exist.Id;
                    genaricrepository2.Edite(employee);
                }
            }
            if (!old)
            {
                GeneralDataDTO data = new GeneralDataDTO()
                {
                    Id = exist.Id,
                    Bonus = exist.Bonus,
                    Discount = exist.Penality,
                    VacationDay1 = exist.VacationDay1,
                    VacationDay2 = exist.VacationDay2,
                };
                return CreatedAtAction("GetSettingByID", new { id = data.Id }, data);
            }

            return Content("Not Created But Passed to Employee(s) ,cause There's Setting With The Same Data You Entered ");
        }

        [HttpPut]
        [Route("/Setting/EditGeneral")]
        [Authorize(Policy = "Permission:GeneralSettings.Edit")]
        public async Task<ActionResult> EditGeneralSettings(GeneralDataDTO Data)
        {
            if (Data.Id != null && Data.Id != 0)
            {
                var result = generalSettingRepository.GetById((int)Data.Id);

                if (result != null)
                {
                    result.Bonus = Data.Bonus;
                    result.VacationDay1 = Data.VacationDay1;
                    result.VacationDay2 = Data.VacationDay2;
                    result.Penality = Data.Discount;
                    generalSettingRepository.Edite(result);
                    return NoContent();
                }
            }
            return BadRequest("id is incorrect");


        }
    }
}

