using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Models.AddedRolesAndClaimsDTO
{
    public class AddedRolesAndClaimsDTO
    {
        public string RoleName { get; set; }
        public List<ClaimPermissionsDTO> Claims { get; set; }
    }
}
