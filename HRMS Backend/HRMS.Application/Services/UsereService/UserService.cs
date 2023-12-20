using HRMS.Application.Models.UserDTOModels;
using HRMS.Domain.Data.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services.UsereService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration config;
        public UserService(UserManager<AppUser> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.config = config;
        }
        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            var userEmailExists = await _userManager.FindByEmailAsync(registerDTO.Email);

            if(userEmailExists != null)
            {
                throw new Exception($"Email '{registerDTO.Email}' already exists") ;
            }

            var userNameExists = await _userManager.FindByNameAsync(registerDTO.UserName);

            if (userNameExists != null)
            {
                throw new Exception($"Username '{registerDTO.UserName}' already exists");
            }

            AppUser user = new AppUser()
            {
                FullName = registerDTO.FullName,
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                PasswordHash = registerDTO.Password
            };

            IdentityResult result = null;

            //should be drop down list in angular
            if (!string.IsNullOrEmpty(registerDTO.Role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(registerDTO.Role);
                if(roleExists)
                {

                    result = await _userManager.CreateAsync(user, registerDTO.Password);

                    if (result.Succeeded)
                    {
                        var addToRole = await _userManager.AddToRoleAsync(user, registerDTO.Role);

                        if(addToRole.Succeeded)
                            return $"User with name '{user.FullName}' registered successfully and assigned to role {registerDTO.Role}";
                    }
                }
                else
                {
                    throw new Exception($"No such role exists.");
                }
            }

            result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(!result.Succeeded)
            {
                StringBuilder errors = new StringBuilder();

                foreach(var error in result.Errors)
                {
                    errors.AppendLine(error.Description);
                }

                throw new Exception(errors.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
            }

            return $"User with name '{user.FullName}' registered successfully and assigned to role {Authorization.default_role} because role was not specified";

        }

        public async Task<AuthenticationDTO> LoginAsync(LoginDTO loginDTO)
        {
            var authentication = new AuthenticationDTO();

            var user = await _userManager.FindByNameAsync(loginDTO.UserName);

            if(user == null)
            {
                authentication.IsAuthenticated = false;
                authentication.Message = $"No account with username {loginDTO.UserName} exists";

                return authentication;
            }

            bool checkPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (checkPassword)
            {
                authentication.IsAuthenticated = true;
                JwtSecurityToken token = await CreateToken(user);
                authentication.Token = new JwtSecurityTokenHandler().WriteToken(token);
                authentication.Email = user.Email;
                authentication.UserName = user.UserName;
                var roles = await _userManager.GetRolesAsync(user);
                authentication.Roles = roles.ToList();
                authentication.Expiration = token.ValidTo;

                return authentication;
            }

            authentication.IsAuthenticated = false;
            authentication.Message = $"Invalid password";

            return authentication;
        }

        private async Task<JwtSecurityToken> CreateToken(AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            IdentityRole identityRole;
            IList<Claim> roleClaims;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

                identityRole = await _roleManager.FindByNameAsync(role);
                roleClaims = await _roleManager.GetClaimsAsync(identityRole);

                foreach(var claim in roleClaims)
                {
                    claims.Add(new Claim("permissions", claim.Type + "." + claim.Value));
                }
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var signInCred = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: config["JWT:Issuer"],
                    audience: config["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(double.Parse(config["JWT:DurationInMinutes"])),
                    signingCredentials: signInCred
                );

            return token;
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles;
        }
    }
}
