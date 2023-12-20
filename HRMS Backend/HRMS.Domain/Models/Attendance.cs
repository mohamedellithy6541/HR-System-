using HRMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Models
{
	public class Attendance : BaseEntity
	{
        public DateTime ArrivalTime { get; set; }
		public DateTime DepartureTime { get; set; }
        public DateTime AttendaceDate { get; set; }

		[ForeignKey("Employee")]
		public int EmpID { get; set; }
		public virtual Employee? Employee { get; set; }

		[ForeignKey("SeasonalVacation")]
		public int? SeasonalVacationID { get; set; }
		public virtual SeasonalVacation? SeasonalVacation { get; set; }

	}
}
