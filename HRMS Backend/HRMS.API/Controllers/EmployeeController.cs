using HRMS.Application.Models;
using HRMS.Application.Repository;
using HRMS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HRMS.Domain.Data.Constants.Authorization;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenaricrepository<Employee> _genaricrepository;
        public EmployeeController(IGenaricrepository<Employee> genaricrepository)
        {
            _genaricrepository = genaricrepository;
               
        }

        [HttpGet]
        [Authorize(Policy = "Permission:Employee.View")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var Employee = await _genaricrepository.GetAllAsync();
            if (Employee is null)
                return NotFound();

            List<EmployeeDto> employees = new List<EmployeeDto>();
            foreach (var employee in Employee)
            {
		        var employeeDTO = new EmployeeDto()
		        {
                    id = employee.Id,
			        firstName = employee.FirstName,
			        lastName = employee.LastName,
			        address = employee.Address,
			        Phone = employee.Phone,
			        Gender = employee.Gender,
			        Nationality = employee.Nationality,
			        BirthDate = employee.BirthDate,
			        NationalId = employee.NationalId,
			        HireDate = employee.HireDate,
			        salary = employee.salary,
			        ArrivalTime = employee.ArrivalTime,
			        LeaveTime = employee.LeaveTime,
                    DepartID = employee.DepartID
		        };
                employees.Add(employeeDTO);
	        }
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
		[Authorize(Policy = "Permission:Employee.View")]
		public ActionResult<Employee> GetEmployeeByID(int id)
        {
            var Employee = _genaricrepository.GetById(id);
            if (Employee is null)
                return NotFound();

            var employeeDTO = new EmployeeDto()
            {
                firstName = Employee.FirstName,
                lastName = Employee.LastName,
                address = Employee.Address,
                Phone = Employee.Phone,
                Gender = Employee.Gender,
                Nationality = Employee.Nationality,
                BirthDate = Employee.BirthDate,
                NationalId = Employee.NationalId,
                HireDate = Employee.HireDate,
                salary = Employee.salary,
                ArrivalTime = Employee.ArrivalTime,
                LeaveTime = Employee.LeaveTime,
                DepartID= Employee.DepartID
            };

            return Ok(employeeDTO);
        }

        [HttpPost]
        [Authorize(Policy = "Permission:Employee.Create")]
        public ActionResult<EmployeeDto> Create( EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                FirstName = employeeDto.firstName,
                LastName = employeeDto.lastName,
                Address = employeeDto.address,
                Phone = employeeDto.Phone,
                Gender = employeeDto.Gender,
                Nationality = employeeDto.Nationality,
                BirthDate = employeeDto.BirthDate,
                NationalId = employeeDto.NationalId,
                HireDate = employeeDto.HireDate,
                salary = employeeDto.salary,
                ArrivalTime = employeeDto.ArrivalTime,
                LeaveTime = employeeDto.LeaveTime,
                DepartID= employeeDto.DepartID
            };
            _genaricrepository.Create(employee);
            return Ok(employeeDto);
        }

        [HttpPut("{id}")]
		[Authorize(Policy = "Permission:Employee.Edit")]
		public IActionResult Edite(int id,EmployeeDto employeeDto)
        {
            var emp = _genaricrepository.GetById(id);
            if (emp == null)
                return NotFound($"No Employee with this {emp.Id}");
            emp.FirstName = employeeDto.firstName;
            emp.Address = employeeDto.address;
            emp.Phone = employeeDto.Phone;
            emp.Gender = employeeDto.Gender;
            emp.Nationality = employeeDto.Nationality;
            emp.BirthDate = employeeDto.BirthDate;
            emp.NationalId = employeeDto.NationalId;
            emp.HireDate = employeeDto.HireDate;
            emp.salary = employeeDto.salary;
            emp.ArrivalTime = employeeDto.ArrivalTime;
            emp.LeaveTime = employeeDto.LeaveTime;
            emp.DepartID = employeeDto.DepartID;

			_genaricrepository.Edite(emp);
            return Ok();

        }

        [HttpDelete("{id}")]
		[Authorize(Policy = "Permission:Employee.Delete")]
		public async Task<IActionResult> DeleteAsync(int id)
        {
            var employee = _genaricrepository.GetById(id);

            if (employee == null)
                return NotFound($"No employee was found with ID {id}");
            await _genaricrepository.Delete(employee);
            return Ok();
        }
    }
}

