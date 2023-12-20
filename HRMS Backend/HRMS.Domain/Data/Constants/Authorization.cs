using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Data.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            HumanResource,
            User
        }

        public const Roles default_role = Roles.User;
    }
}
