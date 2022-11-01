using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandSterlingProject2.Models
{
    public class Emailmodel
    {
        [EmailAddress(ErrorMessage = "Please enter valid email.")]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }
        public decimal InvestmentValue { get; set; }
        public decimal MonthlyPayment { get; set; }
    }
}