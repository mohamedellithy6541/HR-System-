using AutoMapper;
using HRMS.Application.Services.SalaryServices.Queries.GetSalaries;
using HRMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, SalaryDTO>()
                .ForMember(dest => dest.EmpName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(dest => dest.Attendance, opt => opt.MapFrom(s => s.Attendance.Count()))
                .ForMember(dest => dest.Absence, opt => opt.MapFrom(s => 22 - s.Attendance.Count()));


        }
    }
}
