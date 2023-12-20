using HRMS.Application.Models.SeasonalVacationDto;
using HRMS.Application.Repository;
using HRMS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeasonalVacationController : ControllerBase
    {
        private readonly ISeasonalVacationRepository _seasonalVacationRepository;

        public SeasonalVacationController(ISeasonalVacationRepository seasonalVacationRepository)
        {
            _seasonalVacationRepository = seasonalVacationRepository;
        }

        [HttpPost]
        [Authorize(Policy ="Permission:SeasonalVacation.Create")]
        public IActionResult Create(SeasonalVacationDto seasonalVacationDto)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Create the seasonal vacation
                _seasonalVacationRepository.Create(seasonalVacationDto);
                return Ok(seasonalVacationDto);
            }
            else
            {
                // Return a bad request with model state errors
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Authorize(Policy = "Permission:SeasonalVacation.View")]
        public IActionResult Get()
        {
            // Get all seasonal vacations
            var seasonalVacations = _seasonalVacationRepository.GetAll();
            return Ok(seasonalVacations);
        }

        [HttpGet("{id}", Name = "GetSeasonalVacationById")]
        [Authorize(Policy = "Permission:SeasonalVacation.View")]
        public IActionResult GetById(int id)
        {
            // Get a seasonal vacation by ID
            var seasonalVacation = _seasonalVacationRepository.GetById(id);

            if (seasonalVacation == null)
            {
                // Seasonal vacation not found
                return NotFound();
            }

            return Ok(seasonalVacation);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Permission:SeasonalVacation.Edit")]
        public IActionResult Update(int id, [FromBody] SeasonalVacationDto seasonalVacationDto)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Get the existing seasonal vacation
                var existingSeasonalVacation = _seasonalVacationRepository.GetById(id);

                if (existingSeasonalVacation == null)
                {
                    // Seasonal vacation not found
                    return NotFound("Seasonal vacation not found");
                }

                // Update the seasonal vacation
                existingSeasonalVacation.Name = seasonalVacationDto.Name;
                existingSeasonalVacation.VacationDate = seasonalVacationDto.VacationDate;

                if (_seasonalVacationRepository.Update(id, seasonalVacationDto))
                {
                    // Seasonal vacation updated successfully
                    return Ok(new { Message = "Seasonal vacation updated successfully" } );
                }
                else
                {
                    // Failed to save changes
                    return BadRequest(new { Message = "Failed to save the changes" });

                }
            }
            else
            {
                // Return a bad request with model state errors
                return BadRequest(new { message = "Failed to update the seasonal vacation" });
            }
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    // Get the seasonal vacation by ID
        //    var seasonalVacation = _seasonalVacationRepository.GetById(id);

        //    if (seasonalVacation == null)
        //    {
        //        // Seasonal vacation not found
        //        return NotFound("There is no ID like you want");
        //    }

        //    if (_seasonalVacationRepository.Delete(id))
        //    {
        //        // Seasonal vacation deleted successfully
        //        return Ok("The seasonal vacation deleted successfully");
        //    }
        //    else
        //    {
        //        // Failed to delete the seasonal vacation
        //        return BadRequest("Failed to delete the seasonal vacation");
        //    }
        //}

        [HttpDelete("{id}")]
        [Authorize(Policy = "Permission:SeasonalVacation.Delete")]
        public IActionResult Delete(int id)
        {
            // Get the seasonal vacation by ID
            var seasonalVacation = _seasonalVacationRepository.GetById(id);

            if (seasonalVacation == null)
            {
                // Seasonal vacation not found
                //return a JSON object that includes a message 
                return NotFound(new { message = "Seasonal vacation not found" });
            }

            if (_seasonalVacationRepository.Delete(id))
            {
                // Seasonal vacation deleted successfully
                return Ok(new { message = "The seasonal vacation deleted successfully" });
            }
            else
            {
                // Failed to delete the seasonal vacation
                return BadRequest(new { message = "Failed to delete the seasonal vacation" });
            }
        }

    }
}
