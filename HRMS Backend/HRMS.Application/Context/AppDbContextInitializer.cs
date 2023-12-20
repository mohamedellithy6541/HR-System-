using HRMS.Domain.Data.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Context
{
    public class AppDbContextInitializer
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {  

            if (!await roleManager.RoleExistsAsync(Authorization.Roles.HumanResource.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.HumanResource.ToString()));
			}


            if (!await roleManager.RoleExistsAsync(Authorization.Roles.User.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
        }

        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            string humanResourceAdminEmail = "humanresource@hrms.com";
            string humanResourceAdminUserName = "humanresource_admin";

            var hrExists = await userManager.FindByEmailAsync(humanResourceAdminEmail);

            if(hrExists == null)
            {
                AppUser Hr_Admin = new AppUser()
                {
                    FullName = humanResourceAdminUserName,
                    UserName = humanResourceAdminUserName,
                    Email = humanResourceAdminEmail,
                    PasswordHash = "Coding@123"
                };

                await userManager.CreateAsync(Hr_Admin, Hr_Admin.PasswordHash);

                await userManager.AddToRoleAsync(Hr_Admin, Authorization.Roles.HumanResource.ToString());
                await userManager.AddToRoleAsync(Hr_Admin, Authorization.Roles.User.ToString());
            }
        }

    }
}
