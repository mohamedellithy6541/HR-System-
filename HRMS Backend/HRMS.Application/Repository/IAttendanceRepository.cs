using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Models;

namespace HRMS.Application.Repository
{
	public interface IAttendanceRepository
	{
		List<Attendance> getAllAttendances(int Pnum);
		List<Attendance> getAttendancesByName(string name, int Pnum);
		List<Attendance> getAllAttendancesByDates(int Pnum, DateTime FDate, DateTime TDate);
		List<Attendance> getAttendancesByNameByDates(string name, int Pnum, DateTime FDate, DateTime TDate);
		List<Attendance> getAttendancesByEmployeeName(string emp_name);
		Attendance getAttendancesByID(int id);
		List<Attendance> getAttendancesByDepartmentName(string dept_name);
		void addAttendance(Attendance attendance);
		void editAttendance(Attendance attendance);
		void deleteAttendance(int AttendanceID);
		int saveChanges();
	}
}
