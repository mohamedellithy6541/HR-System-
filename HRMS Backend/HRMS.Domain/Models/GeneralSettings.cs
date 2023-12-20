using HRMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Models
{
    public class GeneralSettings :BaseEntity
    {
        public int Bonus { get; set; }
        public int Penality { get; set; }
        public string VacationDay1 { get; set; }
        public string VacationDay2 { get; set; }
		public virtual ICollection <Employee>? Employee { get; set; }
	}
}
