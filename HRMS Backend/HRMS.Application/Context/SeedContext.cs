using HRMS.Domain;
using HRMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Context
{
    public class SeedContext
    {
        private static DBContext context;
        private static IServiceProvider _serviceProvider;

        public static void Seed(DBContext appDbContext, IServiceProvider serviceProvider)
        {
            context = appDbContext;
            _serviceProvider = serviceProvider;
            context.Database.EnsureCreated();
            SeedDepartment();
            SeedSettings();
            SeedEmployee();
            SeedAttendance();

        }
        private static void SeedDepartment()
        {
            if (!context.Departments.Any())
            {
                string departmentsAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Departments.json");
                List<Department> departments = JsonConvert.DeserializeObject<List<Department>>(departmentsAsJson);

                context.Departments.AddRange(departments);
                context.SaveChanges();
            }
        }
        private static void SeedSettings()
        {
            if (!context.GeneralSettings.Any())
            {
                string settingsAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Settings.json");
                List<GeneralSettings> settings = JsonConvert.DeserializeObject<List<GeneralSettings>>(settingsAsJson);

                context.GeneralSettings.AddRange(settings);
                context.SaveChanges();
            }
        }

        private static void SeedEmployee()
        {
            if (!context.Employees.Any())
            {
                string empolyeesAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Empolyees.json");
                List<Employee> emps = JsonConvert.DeserializeObject<List<Employee>>(empolyeesAsJson);
                var DepId = context.Departments.FirstOrDefault().Id;
                var setting = context.GeneralSettings.FirstOrDefault().Id;
                foreach (var item in emps)
                {
                    item.SpecialSetting = setting;
                    item.DepartID = DepId ?? 1
                   ;
                }

                context.Employees.AddRange(emps);
                context.SaveChanges();
            }
        }
        private static void SeedAttendance()
        {
            if (!context.Attendances.Any())
            {
                string attendanceAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Attendance.json");
                List<Attendance> attendance = JsonConvert.DeserializeObject<List<Attendance>>(attendanceAsJson);
                var emp = context.Employees.FirstOrDefault().Id;

                //foreach (var item in attendance)
                //{
                //    item.EmpID = emp ?? 1;

                //}

                context.Attendances.AddRange(attendance);
                context.SaveChanges();
            }
        }


    }
    }
