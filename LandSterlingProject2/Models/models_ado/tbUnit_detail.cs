using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandSterlingProject2.Models.models_ado
{
    public class tbUnit_detail

    {
        public string Record_Id { get; set; }
        public string Ref_No { get; set; }
        public int NumberOfClicks { get; set; }
        public int Views_No { get; set; }
        public void RegisterClick()

        {

            NumberOfClicks++;

        }
    }
}