using HRMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Models.GeneralSettingDTO
{

    public class GeneralDataDTO : BaseEntity

    {
        [Required]
        public int Bonus { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string VacationDay1 { get; set; }
        [Required]
        public string VacationDay2 { get; set; }
    }
}
