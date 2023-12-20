using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Models.AttendancesDTO
{
	public class AttendanceDTO
	{
        public int? ID { get; set; }
        public int Emp_ID { get; set; }
		public string? Emp_Name { get; set; }
		public string? Dept_Name { get; set; }
		public DateTime ArrivalTime { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime AttendanceDate { get; set; }
	}
}
