using HRMS.Application.Models.AddedRolesAndClaimsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddRolesWithClaimsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AddRolesWithClaimsController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Authorize(Roles = "HumanResource")]
        public async Task<IActionResult> SaveRolesWithClaims(AddedRolesAndClaimsDTO addedRolesAndClaimsDTO)
        {
            // Check if the role already exists.
            if (await roleManager.RoleExistsAsync(addedRolesAndClaimsDTO.RoleName))
            {
                return BadRequest("Role already exists.");
            }

            // Create a new role and a list to store claims.
            var role = new IdentityRole(addedRolesAndClaimsDTO.RoleName);
            var claimsToAdd = new List<Claim>();

            foreach (var permission in addedRolesAndClaimsDTO.Claims)
            {
                // Check if permission is granted and the PageName is not empty.
                if ( !string.IsNullOrEmpty(permission.PageName))
                {
                    // Split PageName into type and value.
                
                        var type = permission.PageName;
                        var value = permission.Permission;
                        // Create a new claim and add it to the list.
                        claimsToAdd.Add(new Claim(type, value));
                    
                }
            }

            // Create the role and add associated claims.
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                foreach (var claim in claimsToAdd)
                {
                    // Add claims to the role.
                    await roleManager.AddClaimAsync(role, claim);
                }
                return Ok(new { Message = "Role added with claims and permissions." });
            }

            // If role creation fails, return an error response.
            return BadRequest("Role creation failed.");
        }
    }
}