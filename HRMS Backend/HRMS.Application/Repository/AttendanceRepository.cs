using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain;
using HRMS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Repository
{
	public class AttendanceRepository : IAttendanceRepository
	{
		private readonly DBContext dBContext;

		public AttendanceRepository(DBContext dBContext)
        {
			this.dBContext = dBContext;
		}

		public void addAttendance(Attendance attendance)
		{
			dBContext.Add(attendance);
		}

		public void deleteAttendance(int AttendanceID)
		{
			dBContext.Remove(getAttendancesByID(AttendanceID));
		}

		public void editAttendance(Attendance attendance)
		{
			dBContext.Update(attendance);
		}

		public List<Attendance> getAllAttendancesByDates(int Pnum,DateTime FDate,DateTime TDate)
		{
			return dBContext.Attendances.Include(a=>a.Employee).ThenInclude(e => e.Department)
				.Where(a=>a.AttendaceDate>= FDate&&a.AttendaceDate<=TDate).Skip(Pnum*10).Take(11).ToList();
		}

		public List<Attendance> getAllAttendances(int Pnum)
		{
			return dBContext.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department)
					.Skip(Pnum * 10).Take(11).ToList();
		}

		public List<Attendance> getAttendancesByDepartmentName(string dept_name)
		{
			return dBContext.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department)
				.Where(x => x.Employee.Department.Name.ToLower().Equals(dept_name.ToLower())).ToList();

			//return dBContext.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department)
			//	.Where(x => x.Employee.Department.Name.Equals(dept_name,StringComparison.InvariantCultureIgnoreCase)).ToList();
		}

		public List<Attendance> getAttendancesByEmployeeName(string emp_name)
		{
			return dBContext.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department)
				.Where(att => emp_name.ToLower().Equals(att.Employee.FirstName.ToLower())
				|| emp_name.ToLower().Equals(att.Employee.LastName.ToLower())).ToList();
			//return dBContext.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department)
			//	.Where(att => emp_name.Equals(att.Employee.FirstName,StringComparison.InvariantCultureIgnoreCase)
			//	|| emp_name.Equals(att.Employee.LastName,StringComparison.InvariantCultureIgnoreCase)).ToList();
		}

		public Attendance getAttendancesByID(int id)
		{
			return dBContext.Attendances.Include(a => a.Employee).ThenInclude(e => e.Department).Where(x => x.Id==id).FirstOrDefault();
		}

		public List<Attendance> getAttendancesByNameByDates(string name, int Pnum, DateTime FDate, DateTime TDate)
		{
			var list = getAttendancesByDepartmentName(name)?.ToList();
			return (list.Count != 0) ? list.Where(a => a.AttendaceDate >= FDate && a.AttendaceDate <= TDate).Skip(Pnum * 10).Take(11).ToList()
				: getAttendancesByEmployeeName(name).Where(a => a.AttendaceDate >= FDate && a.AttendaceDate <= TDate).Skip(Pnum * 10).Take(11).ToList();
		}

		public List<Attendance> getAttendancesByName(string name, int Pnum)
		{
			var list = getAttendancesByDepartmentName(name)?.ToList();
			return (list.Count != 0) ? list.Skip(Pnum * 10).Take(11).ToList()
					: getAttendancesByEmployeeName(name).Skip(Pnum * 10).Take(11).ToList();
		}


		int IAttendanceRepository.saveChanges()
		{
			return dBContext.SaveChanges();
		}
	}
}
