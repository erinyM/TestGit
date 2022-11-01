using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace LandSterlingProject2.Models.models_ado
{
    public class Subscribe
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Enter Your FullName")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}