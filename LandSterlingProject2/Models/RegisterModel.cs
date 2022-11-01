using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace LandSterlingProject2.Models
{
    public class RegisterModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Enter Your name")]
        public string Name { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter Your Phone Number")]
        public string PhoneNumber { get; set; }
        public string PhoneNumber1 { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IamNotArobot { get; set; }

    }
}