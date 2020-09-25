using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainBox.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ID is Required.")]
        public string IDNumber { get; set; }

        [Display(Name = "Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is Required.")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is Required.")]
        public string Email { get; set; }

        [Display(Name = "Select Role")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is Required.")]
        public string Role { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm-Password is Required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password Should be Same.")]
        public string ConfirmPassword { get; set; }
    }
}
