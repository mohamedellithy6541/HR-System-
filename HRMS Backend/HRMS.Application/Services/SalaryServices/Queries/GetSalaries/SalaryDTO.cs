using AutoMapper;
using HRMS.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services.SalaryServices.Queries.GetSalaries
{
    public class SalaryDTO
    {
      
        public string EmpName { get; set; }
        public string DepartmentName { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public decimal Salary { get; set; }
        public int Attendance { get; set; }
        public int Absence { get; set; }
        public int SpecialSettingsBonus { get; set; }
        public int SpecialSettingsPenality { get; set; }
        public decimal TotalBouns { get; set; }
        public decimal TotalPenality { get; set; }
        public decimal TotalSalary { get; set; }


    }
}
