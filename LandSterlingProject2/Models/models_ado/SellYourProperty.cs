using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandSterlingProject2.Models.models_ado
{
    public class SellYourProperty
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Enter Your FullName")]
        public string FullName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        

        [Required(ErrorMessage = "Enter Your Property Type ")]
        public string Property_Type { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter Your MobileNumber ")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessage = "Enter Your Location ")]
        public string Location { get; set; }

        public string CountryCode { get; set; }
    }
}