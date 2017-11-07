using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDream.Model.Models
{
    public class Users
    {
        [Required]
        [Display(Name = "User Name :")]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "Username Cannot Have Spaces and Comma")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Length Invalid")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password :")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{6,32})", ErrorMessage = "The password contains at least 1 special character, 1 upper-case character, 1 lower-case character, 1 numeric character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password :")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Max Length is 256")]
        [Display(Name = "Email :")]
        public string Email { get; set; }
        [Display(Name = "Role :")]
        public string Role { get; set; }

    }
    public class ChangePasswordModel
    {
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password :")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password :")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{6,32})", ErrorMessage = "The password contains at least 1 special character, 1 upper-case character, 1 lower-case character, 1 numeric character")]
        public string newPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password:")]
        [Compare("newPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string confirmNewpassword { get; set; }
    }
    public class UpdateUser
    {
        [Display(Name = "User Name")]
        public string username { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Max Length is 256")]
        [Display(Name = "Email :")]
        public string Email { get; set; }


    }
}
