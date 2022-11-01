using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandSterlingProject2.Models.models_ado
{
    public class Property
    {
        public string CountryLocation { get; set; }
        public string RecordId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }
        public string ImagePath5 { get; set; }
        public string ImagePath6 { get; set; }
        public string ImagePath7 { get; set; }
        public string ImagePath8 { get; set; }
        public string ImagePath9 { get; set; }
        public string ImagePath10 { get; set; }
        public string ImagePath11 { get; set; }
        public string ImagePath12 { get; set; }

        public string Location { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string VideoUrl { get; set; }
        public string Price { get; set; }
        public string PriceTo { get; set; }
        public string Size { get; set; }
        public string SizeTo { get; set; }
        public string MarketingText { get; set; }
        public string Image360 { get; set; }
        public string Rooms { get; set; }
        public string Floor { get; set; }
        public string PostHandover { get; set; }
        public string CreatedOn { get; set; }
        public string Developer { get; set; }
        public string Type { get; set; }
        public string PropertyType { get; set; }
        public string CompletionDate { get; set; }
        public string RefNumber { get; set; }
        public string PropertyNo { get; set; }
        public string Purpos { get; internal set; }
        public bool saved_property { get; set; }
        public string FristInstallment { get; set; }
        public string Handover { get; set; }
       
        public string UnderConstruction { get; set; }
    }
}