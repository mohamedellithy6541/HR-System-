using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Models.AttendancesDTO;
using HRMS.Application.Repository;
using HRMS.Domain.Models;

namespace HRMS.Application.Services.AttendanceServices
{
	public class AttendanceService
	{
		public Attendance GetAttendanceModel(AttendanceDTO attendanceDTO)
		{
			return new Attendance
			{
				Id = attendanceDTO.ID,
				ArrivalTime = attendanceDTO.ArrivalTime,
				DepartureTime = attendanceDTO.DepartureTime,
				AttendaceDate = attendanceDTO.AttendanceDate,
				EmpID = attendanceDTO.Emp_ID
			};
		}
		public AttendanceDTO GetAttendanceDTO(Attendance attendance)
		{
			return new AttendanceDTO {
				ID = attendance.Id,
				ArrivalTime = attendance.ArrivalTime,
				DepartureTime = attendance.DepartureTime,
				AttendanceDate = attendance.AttendaceDate,
				Emp_Name = attendance.Employee.FirstName + " " + attendance.Employee.LastName,
				Dept_Name = attendance.Employee.Department.Name,
				Emp_ID = attendance.EmpID
			};
		}
		public List<AttendanceDTO> GetListAttendanceDTO(List<Attendance> attendances)
		{
			List<AttendanceDTO> list = new List<AttendanceDTO>();

			foreach (var item in attendances)
            {
				AttendanceDTO attendanceDTO = new AttendanceDTO {
					ID = item.Id,
					ArrivalTime = item.ArrivalTime,
					DepartureTime = item.DepartureTime,
					AttendanceDate = item.AttendaceDate,
					Emp_Name = item.Employee.FirstName + " " + item.Employee.LastName,
					Dept_Name = item.Employee.Department.Name,
					Emp_ID = item.EmpID
				};
				list.Add(attendanceDTO);
            }
			return list;
		}
	}
}
