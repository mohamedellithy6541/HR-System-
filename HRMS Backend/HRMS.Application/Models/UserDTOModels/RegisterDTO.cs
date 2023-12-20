using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Models.UserDTOModels
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 150 characters")]
        public string FullName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 50 characters")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
