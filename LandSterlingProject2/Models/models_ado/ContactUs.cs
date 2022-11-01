using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandSterlingProject2.Models.models_ado
{
    public class ContactUs
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter your name.")]
        public string FullName { get; set; }


        [EmailAddress(ErrorMessage = "Please enter valid email.")]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter message subject.")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "Please enter your mobile number")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessage = "Please enter your PhoneNumber.")]
        public string PhoneNumber { get; set; }
        public string PhoneNumber1 { get; set; }

       
        public string CountryCode { get; set; }


        [Required(ErrorMessage = "Please enter your subject.")]

        public string Exp1 { get; set; }
        [Required(ErrorMessage = "Please enter your message.")] 
        public string Exp2 { get; set; }
        [Required(ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }
        public string IsEnquiry { get; set; }
        public string Cap { get; set; }
        public string Exp3 { get; set; }
        public bool KeepMeInformed { get; set; }
    }
}