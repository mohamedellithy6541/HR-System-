using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS.Domain;
using HRMS.Domain.Models;
using HRMS.Application.Repository;
using HRMS.Application.Services.AttendanceServices;
using HRMS.Application.Models.AttendancesDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class AttendanceController : ControllerBase
    {
		private IAttendanceRepository attendanceRepository;
		AttendanceService attendanceService ;

		public AttendanceController(IAttendanceRepository attendanceRepository)
        {
			this.attendanceRepository = attendanceRepository;
            attendanceService = new AttendanceService();
		}

		// GET: api/Attendance
		[Route("/FDattendance", Name = "GetAttendancesByDates")]
		[HttpGet]
		[Authorize(Policy = "Permission:Attendance.View")]
		public ActionResult<List<AttendanceDTO>> GetAttendancesByDates()
        {
			Request.Headers.TryGetValue("Pnum", out var PageNumber);
			Request.Headers.TryGetValue("FDate", out var FromDate);
			Request.Headers.TryGetValue("TDate", out var ToDate);

			List<AttendanceDTO> list = attendanceService.GetListAttendanceDTO(attendanceRepository.getAllAttendancesByDates(
				int.Parse(PageNumber), DateTime.Parse(FromDate), DateTime.Parse(ToDate)));


			//List<AttendanceDTO> list = attendanceService.GetListAttendanceDTO(attendanceRepository.getAllAttendances(
			//	1, new DateTime(2020,1,20), DateTime.Now.AddDays(10)));

			HttpContext.Response.Headers.Add("next",list.Count>10?"true":"false");

			//Response.Headers.Add("Access-Control-Expose-Headers", "next,name");

			return list.Take(10).ToList();
		}

		[Route("/attendance", Name = "GetAttendances")]
		[HttpGet]
        [Authorize(Policy = "Permission:Attendance.View")]
        public ActionResult<List<AttendanceDTO>> GetAttendances()
		{
			Request.Headers.TryGetValue("Pnum", out var PageNumber);

			List<AttendanceDTO> list = attendanceService.GetListAttendanceDTO(attendanceRepository.getAllAttendances(
				int.Parse(PageNumber)));

			HttpContext.Response.Headers.Add("next", list.Count > 10 ? "true" : "false");

			return list.Take(10).ToList();
		}

		[Route("/FDsearch/{name:alpha}", Name = "GetAttendancesByNameByDates")]
		[HttpGet]
		[Authorize(Policy = "Permission:Attendance.View")]
		public ActionResult<List<AttendanceDTO>> GetAttendancesByNameByDates(string name)
		{
			Request.Headers.TryGetValue("Pnum", out var PageNumber);
			Request.Headers.TryGetValue("FDate", out var FromDate);
			Request.Headers.TryGetValue("TDate", out var ToDate);

			List<AttendanceDTO> list = attendanceService.GetListAttendanceDTO(attendanceRepository.getAttendancesByNameByDates
				(name,int.Parse(PageNumber), DateTime.Parse(FromDate), DateTime.Parse(ToDate)));

			HttpContext.Response.Headers.Add("next", list.Count > 10 ? "true" : "false");
			return list.Take(10).ToList();
		}

		[Route("/search/{name:alpha}", Name = "GetAttendancesByName")]
		[HttpGet]
        [Authorize(Policy = "Permission:Attendance.View")]
        public ActionResult<List<AttendanceDTO>> GetAttendancesByName(string name)
		{
			Request.Headers.TryGetValue("Pnum", out var PageNumber);

			List<AttendanceDTO> list = attendanceService.GetListAttendanceDTO(attendanceRepository.getAttendancesByName
				(name, int.Parse(PageNumber)));

			HttpContext.Response.Headers.Add("next", list.Count > 10 ? "true" : "false");
			return list.Take(10).ToList();
		}

		[Route("/emp/{emp_name:alpha}", Name = "GetAttendancesByEmployeeName")]
        [HttpGet]
		[Authorize(Policy = "Permission:Attendance.View")]
		public ActionResult<List<AttendanceDTO>> GetAttendancesByEmployeeName(string emp_name)
        {
			return attendanceService.GetListAttendanceDTO(attendanceRepository.getAttendancesByEmployeeName(emp_name));
        }
        [Route("/dept/{dept_name:alpha}", Name = "GetAttendancesByDepartmentName")]
		[HttpGet]
		[Authorize(Policy = "Permission:Attendance.View")]
		public ActionResult<List<AttendanceDTO>> GetAttendancesByDepartmentName(string dept_name)
		{
			return attendanceService.GetListAttendanceDTO(attendanceRepository.getAttendancesByDepartmentName(dept_name));
		}
		[HttpPost]
		[Authorize(Policy = "Permission:Attendance.Create")]
		public ActionResult AddAttendance(AttendanceDTO attendanceDTO)
		{
            try
            {
                attendanceRepository.addAttendance(attendanceService.GetAttendanceModel(attendanceDTO));
				attendanceRepository.saveChanges();
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
			return NoContent();
		}
		[HttpPut]
		[Authorize(Policy = "Permission:Attendance.Edit")]
		public ActionResult EditAttendance(AttendanceDTO attendanceDTO)
		{
			try
			{
				attendanceRepository.editAttendance(attendanceService.GetAttendanceModel(attendanceDTO));
				attendanceRepository.saveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return NoContent();
		}
		[HttpDelete]
		[Authorize(Policy = "Permission:Attendance.Delete")]
		public ActionResult deleteAttendance(int id)
		{
			try
			{
				attendanceRepository.deleteAttendance(id);
				attendanceRepository.saveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return NoContent();
		}

	}
}
