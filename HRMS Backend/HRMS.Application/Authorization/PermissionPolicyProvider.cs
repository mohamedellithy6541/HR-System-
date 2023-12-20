using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        const string POLICY_PREFIX = "Permission";

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var permision = policyName.Substring(POLICY_PREFIX.Length + 1).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);

                //check if the loged user has the required permission
                //but if the user is an HR admin the policy will succeed regardless of the permissions
                policy.RequireAssertion(c => c.User.HasClaim(claim => 
                (claim.Type == "permissions" && permision.Contains(claim.Value)) ||
                (claim.Type == ClaimTypes.Role && claim.Value == "HumanResource")
                ));
;
                return Task.FromResult(policy.Build());
            }

            return Task.FromResult(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .RequireClaim(ClaimTypes.Role, "HumanResource")
                .Build());
        }
    }
}
