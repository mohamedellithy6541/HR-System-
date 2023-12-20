using HRMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Models.SeasonalVacationDto
{
    public class SeasonalVacationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime VacationDate { get; set; }
    }
}
