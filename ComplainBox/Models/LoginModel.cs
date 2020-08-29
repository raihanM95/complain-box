using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainBox.Models
{
    public class LoginModel
    {
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is Required.")]
        public string Email { get; set; }

        [Display(Name = "Select Role:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is Required.")]
        public string Role { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
