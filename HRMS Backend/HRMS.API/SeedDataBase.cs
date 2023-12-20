using HRMS.Application;
using HRMS.Application.Context;
using Microsoft.AspNetCore.Identity;

namespace HRMS.API
{
    public class SeedDataBase
    {
        public static async Task SeedAdminAndRoles(WebApplication app)
        {
            using (var ServiceScope = app.Services.CreateScope())
            {
                var roleManager = ServiceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = ServiceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                await AppDbContextInitializer.SeedRolesAsync(roleManager);
                await AppDbContextInitializer.SeedUsersAsync(userManager);
            }
        }
    }
}
