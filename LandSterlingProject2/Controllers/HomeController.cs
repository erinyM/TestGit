using LandSterlingProject2.Helpers;
//using LandSterlingProject2.Models;
//using LandSterlingProject2.Models;
using LandSterlingProject2.Models.models_ado;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PagedList;
namespace LandSterlingProject2.Controllers
{
    public class HomeController : Controller
    {
       // ePMS2040Entities3 context = new ePMS2040Entities3();
        public ActionResult Index()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            DBT.CMD.CommandText = "select distinct Property_Type from tbProperty_Master where Property_Type<>'0'";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                tbProperty_Master[] tbProperty_Masters = new tbProperty_Master[DBT.TBL.Rows.Count];
                ViewBag.eUnitCategory_MasterCount = tbProperty_Masters.Length;
                for (int i = 0; i < tbProperty_Masters.Length; i++)
                {
                    tbProperty_Masters[i] = new tbProperty_Master
                    {
                        // Unit_Type = DBT.TBL.Rows[i]["Unit_Type"].ToString(),
                        Property_Type = DBT.TBL.Rows[i]["Property_Type"].ToString(),
                     //   Record_Id = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                    };
                }
                ViewBag.tbProperty_Masters = tbProperty_Masters;
            }
            DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            DBT.CMD.CommandText = "select * from eBedroom_Master order by Record_Id asc";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                eBedroom_Master[] eBedroom_Masters = new eBedroom_Master[DBT.TBL.Rows.Count];
                ViewBag.eBedroom_MasterCount = eBedroom_Masters.Length;
                for (int i = 0; i < eBedroom_Masters.Length; i++)
                {
                    eBedroom_Masters[i] = new eBedroom_Master
                    {
                        Bedrooms = DBT.TBL.Rows[i]["Bedrooms"].ToString(),
                        Record_Id = DBT.TBL.Rows[i]["Record_Id"].ToString()
                    };
                }
                ViewBag.eBedroom_Masters = eBedroom_Masters;
            }
            DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select City_Desc Name,Record_Id RecordId from eCountryCity_Master where Country_Desc = 'United Arab Emirates' order by City_Desc";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            City[] cities = new City[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < cities.Length; i++)
                {
                    cities[i] = new City
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["RecordId"].ToString(),
                    };
                }
            }
            ViewBag.cities = cities;
            DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select Name,Comment,Starcount from tbTestimonials order by testId asc";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbTestimonials[] tbTestimonialsList = new tbTestimonials[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < tbTestimonialsList.Length; i++)
                {
                    tbTestimonialsList[i] = new tbTestimonials
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        Comment = DBT.TBL.Rows[i]["Comment"].ToString(),
                        Starcount =DBT.TBL.Rows[i]["Starcount"].ToString(),
                             
                    };
                }
            }
            ViewBag.tbTestimonialsList = tbTestimonialsList;

            DBT = new DataBaseTools();
            DBT.CMD.CommandText = "  select top 50 u.Record_Id,pm.Property_No,u.Property_Name+', Ref# '+u.Ref_No as Name,U.Floor,Replace(Replace((select top 1 Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1, Replace(Replace(U.Unit_Class,'Rent','For Rent'),'Buy','For Sale') as Type,pm.City+', '+pm.Community_No as Location,pm.Community_No as Address,U.Created_On,pm.Developer,replace(u.Roundoff_Value,'.00','') as Price,ROUND(REPLACE(u.Total_Area,'.00',''),0) as Size,u.Bedrooms as Rooms,u.Floor from tbUnit_Master u,tbProperty_Master pm where pm.Property_No=u.Property_No and u.PublishStatus='yes' order by u.Record_Id desc;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                Property[] properties = new Property[DBT.TBL.Rows.Count];
                for (int i = 0; i < properties.Length; i++)
                {
                    DataBaseTools DBT2 = new DataBaseTools();
                    string propery_No = DBT.TBL.Rows[i]["Property_No"].ToString();
                    string Imagepath = "";
                    if (DBT.TBL.Rows[i]["ImagePath1"] == null || DBT.TBL.Rows[i]["ImagePath1"].ToString() == "")
                    {

                        DBT2.CMD.CommandText = "select Replace(Replace((select top 1 Exp1 from tbProperty_Image pi where pi.Ref_No = @RefNo order by Record_Id asc),'\\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath2";

                        DBT2.CMD.Parameters.AddWithValue("@RefNo", propery_No);
                        DBT2.TBL.Load(DBT2.CMD.ExecuteReader());
                        if (DBT2.TBL.Rows.Count > 0)
                        {
                            Imagepath = DBT2.TBL.Rows[0]["ImagePath2"].ToString();
                        }
                    }
                    else
                    {
                        Imagepath = DBT.TBL.Rows[i]["ImagePath1"].ToString();
                    }

                    properties[i] = new Property
                    {
                        Address = DBT.TBL.Rows[i]["Address"].ToString(),
                        CreatedOn = Convert.ToDateTime(DBT.TBL.Rows[i]["Created_On"]).ToString("dd/MMM/yyyy", new CultureInfo("en-US")),
                        Developer = DBT.TBL.Rows[i]["Developer"].ToString() == "0" ? "" : DBT.TBL.Rows[i]["Developer"].ToString(),
                        Floor = DBT.TBL.Rows[i]["Floor"].ToString(),
                        ImagePath1 = Imagepath,
                        Location = DBT.TBL.Rows[i]["Location"].ToString(),
                        Price = DBT.TBL.Rows[i]["Price"].ToString().KiloFormat(),
                        Rooms = DBT.TBL.Rows[i]["Rooms"].ToString(),
                        Size = DBT.TBL.Rows[i]["Size"].ToString(),
                        Type = DBT.TBL.Rows[i]["Type"].ToString(),
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                        PropertyType = "Unit"
                    };

                }
                ViewBag.Properties = properties;
            }
            DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            // DBT.CMD.CommandText = "select distinct City from tbProperty_Master  join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No  where City not in('Abu Dhabi','Dubai') and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct City from tbProperty_Master   where City not in('Abu Dhabi','Dubai','0') ";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                tbProperty_Master[] otherCountries = new tbProperty_Master[DBT.TBL.Rows.Count];
                ViewBag.eBedroom_MasterCount = otherCountries.Length;
                for (int i = 0; i < otherCountries.Length; i++)
                {
                    otherCountries[i] = new tbProperty_Master
                    {
                        City = DBT.TBL.Rows[i]["City"].ToString()
                    };
                }
                ViewBag.otherCountries = otherCountries;
            }
            DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            //DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No where City ='Abu Dhabi' and Community_No<>'0' and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master  where City ='Abu Dhabi' and Community_No<>'0' ";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                tbProperty_Master[] AbuDahbiApartments = new tbProperty_Master[DBT.TBL.Rows.Count];
                ViewBag.eBedroom_MasterCount = AbuDahbiApartments.Length;
                for (int i = 0; i < AbuDahbiApartments.Length; i++)
                {
                    AbuDahbiApartments[i] = new tbProperty_Master
                    {
                        Community_No = DBT.TBL.Rows[i]["Community_No"].ToString()
                    };
                }
                ViewBag.AbuDahbiApartments = AbuDahbiApartments;
            }
            DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            // DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No where City ='Dubai' and Community_No<>'0' and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master where City ='Dubai' and Community_No<>'0' ";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                tbProperty_Master[] DubaiApartments = new tbProperty_Master[DBT.TBL.Rows.Count];
                ViewBag.eBedroom_MasterCount = DubaiApartments.Length;
                for (int i = 0; i < DubaiApartments.Length; i++)
                {
                    DubaiApartments[i] = new tbProperty_Master
                    {
                        Community_No = DBT.TBL.Rows[i]["Community_No"].ToString()
                    };
                }
                ViewBag.DubaiApartments = DubaiApartments;
            }
            DBT = new DataBaseTools();
             // DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No where City ='Dubai' and Community_No<>'0' and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct Roundoff_Value from tbUnit_Master order by Roundoff_Value asc";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                tbUnit_Master[] pricesList = new tbUnit_Master[DBT.TBL.Rows.Count];
                ViewBag.prices = pricesList.Length;
                for (int i = 0; i < pricesList.Length; i++)
                {
                    pricesList[i] = new tbUnit_Master
                    {
                        Roundoff_Value = DBT.TBL.Rows[i]["Roundoff_Value"].ToString()
                    };
                }
                ViewBag.pricesList = pricesList;
            }


            // DBT = new DataBaseTools();
            // ViewBag.DeveloperCount = 0;
            //DBT.CMD.CommandText = "select Name,Comment,Starcount from tbTestimonials order by testId asc";
            //DBT.TBL.Load(DBT.CMD.ExecuteReader());
            //tbTestimonials[] tbTestimonialss = new tbTestimonials[DBT.TBL.Rows.Count];
            //if (DBT.TBL.Rows.Count > 0)
            //{

            //    for (int i = 0; i < tbTestimonialss.Length; i++)
            //    {
            //        tbTestimonialss[i] = new tbTestimonials
            //        {

            //            Name = DBT.TBL.Rows[i]["Name"].ToString(),
            //            Comment = DBT.TBL.Rows[i]["Comment"].ToString(),
            //            //Starcount =int.Parse(DBT.TBL.Rows[i]["Starcount"].ToString()),
            //            Starcount=  DBT.TBL.Rows[i]["Starcount"].ToString() == null ? "0": DBT.TBL.Rows[i]["Starcount"].ToString()
            //        };
            //    }
            //    ViewBag.tbTestimonials = tbTestimonialss;
            //}
            return View();
        }
        public ActionResult SellYourProperty()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select Name,Comment,Starcount from tbTestimonials order by testId asc";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbTestimonials[] tbTestimonialsList = new tbTestimonials[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < tbTestimonialsList.Length; i++)
                {
                    tbTestimonialsList[i] = new tbTestimonials
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        Comment = DBT.TBL.Rows[i]["Comment"].ToString(),
                        Starcount = DBT.TBL.Rows[i]["Starcount"].ToString(),

                    };
                }
            }
            ViewBag.tbTestimonialsList = tbTestimonialsList;
            ViewBag.otherCountries = LoadOtherCountries();
            ViewBag.AbuDahbiApartments = LoadAboDahbi();
            ViewBag.DubaiApartments = LoadDubai();
             DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            DBT.CMD.CommandText = "select distinct Property_Type from tbProperty_Master where Property_Type<>'0'";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                tbProperty_Master[] tbProperty_Masters = new tbProperty_Master[DBT.TBL.Rows.Count+1];
             //   ViewBag.tbProperty_Masters = tbProperty_Masters.Length;
                tbProperty_Masters[0] = new tbProperty_Master
                {
                    Property_Type = "Property Type",
                };
                for (int i = 0; i < tbProperty_Masters.Length-1; i++)
                {
                   
                        tbProperty_Masters[i+1] = new tbProperty_Master
                        {
                           Property_Type = DBT.TBL.Rows[i]["Property_Type"].ToString(),
                        };
                    
                }
                ViewBag.tbProperty_Masters = tbProperty_Masters;
            }
            DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            DBT.CMD.CommandText = "select question,answer from tb_Questions where pageName='SellYourProperty'";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                tb_Questions[] tb_Questions = new tb_Questions[DBT.TBL.Rows.Count];
                ViewBag.tb_Questions = tb_Questions.Length;
                for (int i = 0; i < tb_Questions.Length; i++)
                {
                    tb_Questions[i] = new tb_Questions
                    {
                        // Unit_Type = DBT.TBL.Rows[i]["Unit_Type"].ToString(),
                        question = DBT.TBL.Rows[i]["question"].ToString(),
                        answer = DBT.TBL.Rows[i]["answer"].ToString(),
                        //   Record_Id = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                    };
                }
                ViewBag.tb_QuestionsList = tb_Questions;
            }
            
               DBT = new DataBaseTools();
                DBT.CMD.CommandText = "select City_Desc Name,Record_Id RecordId from eCountryCity_Master where Country_Desc = 'United Arab Emirates' order by City_Desc";
                DBT.TBL.Load(DBT.CMD.ExecuteReader());
                City[] cities = new City[DBT.TBL.Rows.Count+1];
                cities[0] = new City
                {
                    Name = "Enter Location",
                };
                if (DBT.TBL.Rows.Count > 0)
                {
                    for (int i = 0; i < cities.Length-1; i++)
                    {
                        cities[i+1] = new City
                        {
                            Name = DBT.TBL.Rows[i]["Name"].ToString(),
                            RecordId = DBT.TBL.Rows[i]["RecordId"].ToString(),
                        };
                    }
                    ViewBag.cities = cities;
                }
            
            return View();
        }
        [HttpPost]
        public ActionResult SellYourProperty(SellYourProperty sellyourproperty)
        {
            try
            {

                // if (contact.Cap != "ePMSCap4321#")
                //   throw new Exception();
                ViewBag.IsSent = false;
                DataBaseTools DBT = new DataBaseTools();
                DBT.CMD.Parameters.AddWithValue("@FullName", string.IsNullOrEmpty(sellyourproperty.FullName)?"": sellyourproperty.FullName);
                DBT.CMD.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(sellyourproperty.Email)?"": sellyourproperty.Email);
                DBT.CMD.Parameters.AddWithValue("@MobileNumber", string.IsNullOrEmpty(sellyourproperty.CountryCode)? "":sellyourproperty.CountryCode );
                DBT.CMD.Parameters.AddWithValue("@Location", sellyourproperty.Location=="Location"? "" : sellyourproperty.Location);
             //   DBT.CMD.Parameters.AddWithValue("@CountryCode", sellyourproperty.CountryCode);
                DBT.CMD.Parameters.AddWithValue("@Property_Type", sellyourproperty.Property_Type=="Property_Type"?"": sellyourproperty.Property_Type);
                DBT.CMD.CommandText = "insert into tb_SellYourProperty(FullName,Email,MobileNumber,Country,Property_Type) values(@FullName,@Email,@MobileNumber,@Location,@Property_Type)";
                DBT.CMD.ExecuteNonQuery();
                ViewBag.IsSent = true;
                ModelState.Clear();
                ViewBag.Msg = "Your request was sent";
            }
            catch (Exception ex)
            {
                ViewBag.IsSent = false;
            }
            TempData["notice"] = "<script>swal(\"Good job!\", successfully inserted, \"success\")</script>";
          
            return RedirectToAction("SellYourProperty");//View(sellyourproperty);//
        }
        public ActionResult AboutUS()
        {
            return View();
        }
            public ActionResult Subscribe()
        {
            
            ViewBag.Page = "Subscibe";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Subscribe(Subscribe subscribe)
        {
            try
            {
                
                DataBaseTools DBT = new DataBaseTools();
                DBT.CMD.Parameters.AddWithValue("@Name", subscribe.Name);
                DBT.CMD.Parameters.AddWithValue("@Email", subscribe.Email);
                DBT.CMD.CommandText = "insert into eCC_ClientLeads(FullName,Email) values(@Name,@Email)";
                DBT.CMD.ExecuteNonQuery();
              //  DataBaseTools DBT1 = new DataBaseTools();
              //  DBT1.CMD.Parameters.AddWithValue("@Name", subscribe.Name);
            //    DBT1.CMD.Parameters.AddWithValue("@Email", subscribe.Email);

                DBT.CMD.CommandText = "insert into Subscribe(Name,Email) values(@Name,@Email)";
                DBT.CMD.ExecuteNonQuery();
                ViewBag.IsSent = true;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.IsSent = false;
            }
            //if (contact.IsEnquiry == "1")
            // {
            //   ViewBag.IsSentEnquiry = ViewBag.IsSent;
            //  return PartialView("~/Views/Shared/_Enquire.cshtml");
            // }
            return RedirectToAction("Index");//this.View("Index");
        }
        public ActionResult ContactUs()
        {

            ViewBag.Page = "ContactUs";
            return View();//RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ContactUs(ContactUs contact)
        {
            try
            {

                // if (contact.Cap != "ePMSCap4321#")
                //   throw new Exception();
                ViewBag.IsSent = false;
                DataBaseTools DBT = new DataBaseTools();
                DBT.CMD.Parameters.AddWithValue("@FullName", contact.FullName);
                DBT.CMD.Parameters.AddWithValue("@Email", contact.Email);
                DBT.CMD.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber != null ? contact.PhoneNumber : "");
                DBT.CMD.Parameters.AddWithValue("@Exp1", string.IsNullOrEmpty(contact.Exp1) ? "" : contact.Exp1);
                DBT.CMD.Parameters.AddWithValue("@Exp2", contact.Exp2);
                DBT.CMD.CommandText = "insert into eCC_ClientLeads(FullName,Email,PhoneNumber,Exp1,Exp2) values(@FullName,@Email,@PhoneNumber,@Exp1,@Exp2)";
                DBT.CMD.ExecuteNonQuery();
                ViewBag.IsSent = true;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.IsSent = false;
            }
            //if (contact.IsEnquiry == "1")
            // {
            //   ViewBag.IsSentEnquiry = ViewBag.IsSent;
            //  return PartialView("~/Views/Shared/_Enquire.cshtml");
            // }
            return Redirect(Request.UrlReferrer.PathAndQuery);// RedirectToAction("Index");
        }
        public ActionResult EmailUS()
        {

            ViewBag.Page = "EmailUs";
            return View();
        }
        [HttpPost]
        public ActionResult EmailUs(ContactUs contact)
        {
            try
            {

                // if (contact.Cap != "ePMSCap4321#")
                //   throw new Exception();
                ViewBag.IsSent = false;
                DataBaseTools DBT = new DataBaseTools();
                DBT.CMD.Parameters.AddWithValue("@FullName", contact.FullName);
                DBT.CMD.Parameters.AddWithValue("@Email", contact.Email);
                DBT.CMD.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber != null ? contact.PhoneNumber : "");
                DBT.CMD.Parameters.AddWithValue("@Exp1", string.IsNullOrEmpty(contact.Exp1) ? "" : contact.Exp1);
                DBT.CMD.Parameters.AddWithValue("@Exp2", contact.Exp2);
                DBT.CMD.Parameters.AddWithValue("@Exp3", "Property Mail");
                DBT.CMD.CommandText = "insert into eCC_ClientLeads(FullName,Email,PhoneNumber,Exp1,Exp2,Exp3) values(@FullName,@Email,@PhoneNumber,@Exp1,@Exp2,@Exp3)";
                DBT.CMD.ExecuteNonQuery();
                ViewBag.IsSent = true;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.IsSent = false;
            }

            return View();// RedirectToAction("Properties");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Properties(string code = "", string AnyStatus = "", string ProjectName = "", string Rooms = "", string Bathrooms = "", string Location = "", string min_area = "", string max_area = "", string min_price = "", string max_price = "", string SortBy = "", int TargetPage=0, String PropertyType = "Unit")
        {
            
            if (code == "Developer")
                code = "";
            ViewBag.ProjectNameList = LoadProjects();
            ViewBag.PropertiesList = LoadProperties();
            ViewBag.RoomList = LoadBedrooms();
            ViewBag.Cities = LoadCities();
            ViewBag.Developers = LoadDevelopers();
            Feature[] features = LoadFeatures();
            ViewBag.FeatureList = features;
            ViewBag.pricesList = LoadPrices();
            ViewBag.areasList = LoadAreas();
            ViewBag.othercountriesList = LoadOtherCountries();
            ViewBag.AboDahbiList = LoadAboDahbi();
            ViewBag.DubaiList = LoadDubai();
            DataBaseTools DBT = new DataBaseTools();
            string UnitCondition = "";
            string ProjectCondition = "";
            string PropertyCondition = "";
            string PropertyCondition1 = "";
            if (code != "")
            {
                DBT.CMD.Parameters.AddWithValue("@DeveloperCode", code);
                UnitCondition += " and pm.Developer=@DeveloperCode ";
                ProjectCondition += " and pm.Developer=@DeveloperCode ";
            }
            if (AnyStatus != "" && AnyStatus != "Any Status" && AnyStatus != "Rent or Buy")
            {
                DBT.CMD.Parameters.AddWithValue("@Type", AnyStatus == "For Sale" ? "Buy" : "Rent");
                UnitCondition += " and u.Unit_Class=@Type ";
            }

            if (ProjectName != "" && ProjectName != "Project")
            {
                DBT.CMD.Parameters.AddWithValue("@ProjectName", ProjectName);
                UnitCondition += " and pm.Description=@ProjectName ";
                ProjectCondition += " and pm.Description=@ProjectName ";
            }

            if (Rooms != "" && Rooms != "Bed & Bath")
            {
                DBT.CMD.Parameters.AddWithValue("@Rooms", Rooms);
                UnitCondition += " and u.Bedrooms=@Rooms ";
            }

            if (Location != "" && Location != "Enter Location" && Location != "City")
            {
                DBT.CMD.Parameters.AddWithValue("@Location", "%" + Location + "%");
                UnitCondition += " and pm.City like @Location ";
                ProjectCondition += " and pm.City like @Location ";
            }

            if (min_area != "" && min_area!= "Area (sqft)")
            {
                DBT.CMD.Parameters.AddWithValue("@min_area", min_area);
                UnitCondition += " and Cast(REPLACE(u.Total_Area,'.00','') as float)<=@min_area ";
            }

            if (max_area != "")
            {
                DBT.CMD.Parameters.AddWithValue("@max_area", max_area);
                UnitCondition += " and Cast(REPLACE(u.Total_Area,'.00','') as float)<=@max_area ";
            }


            if (min_price != ""&& min_price != "Min Price" && min_price!= "Price (AED)")
            {
                DBT.CMD.Parameters.AddWithValue("@min_price", min_price);
                UnitCondition += " and cast(replace(u.Roundoff_Value,'.00','') as float) <= @min_price ";
            }

            if (max_price != "" && max_price != "Max Price")
            {
                DBT.CMD.Parameters.AddWithValue("@max_price", max_price);
                UnitCondition += " and cast(replace(u.Roundoff_Value,'.00','') as float) <= @max_price ";
            }
            if (PropertyType != "" && PropertyType != "Property Type")
            {
                DBT.CMD.Parameters.AddWithValue("@PropertyType", PropertyType);
                PropertyCondition += " and pm.Property_Type like @PropertyType ";
                PropertyCondition1+= " where Property_Type like @PropertyType ";
            }
            // Get Count
            //DBT.CMD.CommandText = @"
            //                    select count(*) from
            //                    (
            //                            (select 'Project' as PropertyType,Property_Type from tbProperty_Master pm where pm.Exp4 = 'yes' " + ProjectCondition + @")
            //                            Union All 
            //                            (select 'Unit' as PropertyType ,Property_Type from tbUnit_Master u, tbProperty_Master pm where pm.Property_No = u.Property_No and u.PublishStatus = 'yes' " + UnitCondition + @")
            //                    ) a " + PropertyCondition + " ";
            DBT.CMD.CommandText = @"
                                select count(*) from
                                        (select 'u.Unit' as PropertyType ,pm.Property_Type from tbUnit_Master u inner join tbProperty_Master pm on pm.Property_No=u.Property_No  left outer join tbUnit_Detail ud on u.Ref_No=ud.Ref_No   where  u.PublishStatus = 'yes' " + UnitCondition + " " + PropertyCondition + @") a";
          //  DBT.CMD.Parameters.AddWithValue("@PropertyType", PropertyType);
            DBT.TBL.Load(DBT.CMD.ExecuteReader());

            int TotalCount = int.Parse(DBT.TBL.Rows[0][0].ToString());
            int PageSize =16;
            int Pages = TotalCount / PageSize + ((TotalCount % PageSize == 0) ? 0 : 1);
            ViewBag.TargetPage = TargetPage;
            ViewBag.Pages = Pages;


            //Get properties
            DBT.TBL = new DataTable();
            //DBT.CMD.CommandText = @" SELECT * from ((select 'Project' as PropertyType,Property_Type,pm.Record_Id, Remarks as Description,pm.Description Name,'0' as Floor,'0' as Property_No,
            //                            Replace(Replace((select top 1 Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1,
            //                            pm.Latitude,pm.Longitude,pm.Exp7 as VideoUrl,'' as Type,pm.City+', '+pm.Community_No as Location,pm.Community_No as Address,pm.CreatedOn as Created_On,pm.Developer,round((select min(cast(replace(u.Roundoff_Value,'.00','') as float))  from tbUnit_Master u where u.Property_No=pm.Property_No ),0) as Price,round((select min(cast(replace(u.Total_Area,'.00','') as float))  from tbUnit_Master u where u.Property_No=pm.Property_No ),0) as Size,
            //                            '' as Rooms
            //                            from tbProperty_Master pm where pm.Exp4='yes'" + ProjectCondition + @")
            //        Union All

            //         (select 'Unit' as PropertyType,Property_Type,u.Record_Id,u.Remarks as Description,u.Property_Name+', Ref# '+u.Ref_No as Name,U.Floor,u.Property_No,
            //         Replace(Replace((select top 1 Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1, 
            //         pm.Longitude,pm.Latitude,u.Adv_Remarks2 VideoUrl,Replace(Replace(U.Unit_Class,'Rent','For Rent'),'Buy','For Sale') as Type,pm.City+', '+pm.Community_No as Location,pm.Community_No as Address,U.Created_On,pm.Developer,
            //         cast(replace(u.Roundoff_Value,'.00','') as float) as Price,ROUND(REPLACE(u.Total_Area,'.00',''),0) as Size,u.Bedrooms as Rooms
            //         from tbUnit_Master u,tbProperty_Master pm where pm.Property_No=u.Property_No and u.PublishStatus='yes'" + UnitCondition + @")) a" + PropertyCondition + "";
            DBT.CMD.CommandText = @" 

                     select 'Unit' as PropertyType,Property_Type,u.Record_Id,u.Remarks as Description,u.Property_Name+', Ref# '+u.Ref_No as Name,U.Floor,u.Property_No,
                     Replace(Replace((select top 1 Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1, 
                     pm.Longitude,pm.Latitude,u.Adv_Remarks2 VideoUrl,Replace(Replace(U.Unit_Class,'Rent','For Rent'),'Buy','For Sale') as Type,pm.City+', '+pm.Community_No as Location,pm.Community_No as Address,U.Created_On,pm.Developer,
                     cast(replace(u.Roundoff_Value,'.00','') as float) as Price,ROUND(REPLACE(u.Total_Area,'.00',''),0) as Size,u.Bedrooms as Rooms
                     from tbProperty_Master pm inner join tbUnit_Master u
                        on pm.Property_No=u.Property_No  left outer join tbUnit_Detail ud on u.Ref_No=ud.Ref_No where pm.Property_No=u.Property_No and u.PublishStatus='yes'" + UnitCondition + " " + PropertyCondition + @"";
            string order = " order by PropertyType asc ";

            if (SortBy == "Price: High to Low")
            {
                order = " order by Price desc";
            }
            if (SortBy == "Price: Low to High")
            {
                order = " order by Price asc";
            }
            if (SortBy == "Newest Properties")
            {
                order = " order by Created_On desc";
            }
            if (SortBy == "Oldest Properties")
            {
                order = " order by Created_On asc";
            }
            if (SortBy == "Most Viewed")
            {
                order = " order by Views_No desc";
            }
           

            DBT.CMD.CommandText += order;

            DBT.CMD.CommandText += " OFFSET  " + (TargetPage * PageSize) + " ROWS FETCH NEXT 16 ROWS ONLY";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            List<Property> properties = new List<Property>();
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < DBT.TBL.Rows.Count; i++)
                {

                    DataBaseTools DBT2 = new DataBaseTools();
                   // string propery_No = DBT.TBL.Rows[i]["Ref_No"].ToString();
                    string propery_No = DBT.TBL.Rows[i]["Property_No"].ToString();
                    //saved property
                   bool saved_property = IsExistSavedProperty(Convert.ToInt32( Session["UserID"]), DBT.TBL.Rows[i]["Record_Id"].ToString());
                    string Imagepath = "";
                    if (DBT.TBL.Rows[i]["ImagePath1"] == null || DBT.TBL.Rows[i]["ImagePath1"].ToString() == "")
                    {

                        DBT2.CMD.CommandText = "select Replace(Replace((select top 1 Exp1 from tbProperty_Image pi where pi.Ref_No = @RefNo order by Record_Id asc),'\\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath2";

                        DBT2.CMD.Parameters.AddWithValue("@RefNo", propery_No);
                        DBT2.TBL.Load(DBT2.CMD.ExecuteReader());
                        if (DBT2.TBL.Rows.Count > 0)
                        {
                            Imagepath = DBT2.TBL.Rows[0]["ImagePath2"].ToString();
                        }
                    }
                    else
                    {
                        Imagepath = DBT.TBL.Rows[i]["ImagePath1"].ToString();
                    }
                   properties.Add(new Property
                    {
                        Address = DBT.TBL.Rows[i]["Address"].ToString(),
                        CreatedOn = Convert.ToDateTime(DBT.TBL.Rows[i]["Created_On"]).ToString("dd/MMM/yyyy", new CultureInfo("en-US")),
                        Developer = DBT.TBL.Rows[i]["Developer"].ToString() == "0" ? "" : DBT.TBL.Rows[i]["Developer"].ToString(),
                        Floor = DBT.TBL.Rows[i]["Floor"].ToString(),
                        ImagePath1 = Imagepath,
                        Location = DBT.TBL.Rows[i]["Location"].ToString(),
                        Price = DBT.TBL.Rows[i]["Price"].ToString().KiloFormat(),
                        Rooms = DBT.TBL.Rows[i]["Rooms"].ToString(),
                        Size = DBT.TBL.Rows[i]["Size"].ToString(),
                        Type = DBT.TBL.Rows[i]["Type"].ToString(),
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                        PropertyType = DBT.TBL.Rows[i]["PropertyType"].ToString(),
                        saved_property= saved_property,
                });
                }
            }

            ViewBag.Properties = properties;
           // int page_index = 1;
          //  page_index = TargetPage.HasValue ? Convert.ToInt32(TargetPage) : 1;
           // ViewBag.Properties = properties.ToPagedList(page_index, PageSize);
            ViewBag.Code = code;
            ViewBag.AnyStatus = AnyStatus;
            ViewBag.ProjectName = ProjectName;
            ViewBag.Rooms = Rooms;
            ViewBag.Developer = code;
            ViewBag.Location = Location;
            ViewBag.min_area = min_area;
            ViewBag.max_area = max_area;
            ViewBag.min_price = min_price;
            ViewBag.max_price = max_price;
            ViewBag.SortBy = SortBy;
            ViewBag.TargetPage = TargetPage;
            ViewBag.PropertyType = PropertyType;

            List<string> PageList = new List<string>();
            PageList.Add("1");
            if (TargetPage - 2 > 0)
                PageList.Add("..");
            if (TargetPage - 1 > 0)
                PageList.Add(TargetPage.ToString());
            if (TargetPage != 0)
                PageList.Add((TargetPage + 1).ToString());
            if (TargetPage + 1 < Pages - 1)
                PageList.Add((TargetPage + 2).ToString());
            if (Pages - 1 > TargetPage + 2)
                PageList.Add("..");
            if (Pages > TargetPage + 1)
                PageList.Add(Pages.ToString());
            ViewBag.PageList = PageList;

            return View();
        }
        public ActionResult SavedProperties( int TargetPage = 0)
        {

            
            ViewBag.ProjectNameList = LoadProjects();
            ViewBag.PropertiesList = LoadProperties();
            ViewBag.RoomList = LoadBedrooms();
            ViewBag.Cities = LoadCities();
            ViewBag.Developers = LoadDevelopers();
            Feature[] features = LoadFeatures();
            ViewBag.FeatureList = features;
            ViewBag.pricesList = LoadPrices();
            ViewBag.areasList = LoadAreas();
            ViewBag.othercountriesList = LoadOtherCountries();
            ViewBag.AboDahbiList = LoadAboDahbi();
            ViewBag.DubaiList = LoadDubai();
            DataBaseTools DBT = new DataBaseTools();
            string UnitCondition = "";
             string PropertyCondition = "";
                 DBT.CMD.CommandText = @"
                                select count(*) from
                                        (select 'u.Unit' as PropertyType ,pm.Property_Type from tbUnit_Master u inner join tbProperty_Master pm on pm.Property_No=u.Property_No inner join favourites f on u.Record_Id=f.RecordId left outer join tbUnit_Detail ud on u.Ref_No=ud.Ref_No   where  u.PublishStatus = 'yes' " + @") a";
            //  DBT.CMD.Parameters.AddWithValue("@PropertyType", PropertyType);
            DBT.TBL.Load(DBT.CMD.ExecuteReader());

            int TotalCount = int.Parse(DBT.TBL.Rows[0][0].ToString());
            int PageSize = 16;
            int Pages = TotalCount / PageSize + ((TotalCount % PageSize == 0) ? 0 : 1);
            ViewBag.TargetPage = TargetPage;
            ViewBag.Pages = Pages;


            //Get properties
            DBT.TBL = new DataTable();
           
            DBT.CMD.CommandText = @" 

                     select 'Unit' as PropertyType,Property_Type,u.Record_Id,u.Remarks as Description,u.Property_Name+', Ref# '+u.Ref_No as Name,U.Floor,u.Property_No,
                     Replace(Replace((select top 1 Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1, 
                     pm.Longitude,pm.Latitude,u.Adv_Remarks2 VideoUrl,Replace(Replace(U.Unit_Class,'Rent','For Rent'),'Buy','For Sale') as Type,pm.City+', '+pm.Community_No as Location,pm.Community_No as Address,U.Created_On,pm.Developer,
                     cast(replace(u.Roundoff_Value,'.00','') as float) as Price,ROUND(REPLACE(u.Total_Area,'.00',''),0) as Size,u.Bedrooms as Rooms
                     from tbProperty_Master pm inner join tbUnit_Master u inner join favourites f on u.Record_Id=f.RecordId
                        on pm.Property_No=u.Property_No  left outer join tbUnit_Detail ud on u.Ref_No=ud.Ref_No where pm.Property_No=u.Property_No and u.PublishStatus='yes'" + UnitCondition + " " + PropertyCondition + @"";
            string order = " order by PropertyType asc ";

           

            DBT.CMD.CommandText += order;

            DBT.CMD.CommandText += " OFFSET  " + (TargetPage * PageSize) + " ROWS FETCH NEXT 16 ROWS ONLY";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            List<Property> properties = new List<Property>();
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < DBT.TBL.Rows.Count; i++)
                {

                    DataBaseTools DBT2 = new DataBaseTools();
                    // string propery_No = DBT.TBL.Rows[i]["Ref_No"].ToString();
                    string propery_No = DBT.TBL.Rows[i]["Property_No"].ToString();
                    //saved property
                    bool saved_property = IsExistSavedProperty(Convert.ToInt32(Session["UserID"]), DBT.TBL.Rows[i]["Record_Id"].ToString());
                    string Imagepath = "";
                    if (DBT.TBL.Rows[i]["ImagePath1"] == null || DBT.TBL.Rows[i]["ImagePath1"].ToString() == "")
                    {

                        DBT2.CMD.CommandText = "select Replace(Replace((select top 1 Exp1 from tbProperty_Image pi where pi.Ref_No = @RefNo order by Record_Id asc),'\\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath2";

                        DBT2.CMD.Parameters.AddWithValue("@RefNo", propery_No);
                        DBT2.TBL.Load(DBT2.CMD.ExecuteReader());
                        if (DBT2.TBL.Rows.Count > 0)
                        {
                            Imagepath = DBT2.TBL.Rows[0]["ImagePath2"].ToString();
                        }
                    }
                    else
                    {
                        Imagepath = DBT.TBL.Rows[i]["ImagePath1"].ToString();
                    }
                    properties.Add(new Property
                    {
                        Address = DBT.TBL.Rows[i]["Address"].ToString(),
                        CreatedOn = Convert.ToDateTime(DBT.TBL.Rows[i]["Created_On"]).ToString("dd/MMM/yyyy", new CultureInfo("en-US")),
                        Developer = DBT.TBL.Rows[i]["Developer"].ToString() == "0" ? "" : DBT.TBL.Rows[i]["Developer"].ToString(),
                        Floor = DBT.TBL.Rows[i]["Floor"].ToString(),
                        ImagePath1 = Imagepath,
                        Location = DBT.TBL.Rows[i]["Location"].ToString(),
                        Price = DBT.TBL.Rows[i]["Price"].ToString().KiloFormat(),
                        Rooms = DBT.TBL.Rows[i]["Rooms"].ToString(),
                        Size = DBT.TBL.Rows[i]["Size"].ToString(),
                        Type = DBT.TBL.Rows[i]["Type"].ToString(),
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                        PropertyType = DBT.TBL.Rows[i]["PropertyType"].ToString(),
                        saved_property = saved_property,
                    });
                }
            }

            ViewBag.Properties = properties;
            // int page_index = 1;
            //  page_index = TargetPage.HasValue ? Convert.ToInt32(TargetPage) : 1;
            // ViewBag.Properties = properties.ToPagedList(page_index, PageSize);
           
            ViewBag.TargetPage = TargetPage;
           
            List<string> PageList = new List<string>();
            PageList.Add("1");
            if (TargetPage - 2 > 0)
                PageList.Add("..");
            if (TargetPage - 1 > 0)
                PageList.Add(TargetPage.ToString());
            if (TargetPage != 0)
                PageList.Add((TargetPage + 1).ToString());
            if (TargetPage + 1 < Pages - 1)
                PageList.Add((TargetPage + 2).ToString());
            if (Pages - 1 > TargetPage + 2)
                PageList.Add("..");
            if (Pages > TargetPage + 1)
                PageList.Add(Pages.ToString());
            ViewBag.PageList = PageList;

            return View();
        }
        public bool IsExistSavedProperty(int UserId,string RecordId)
        {
            //test changes
            //test changes
            DataBaseTools DBT = new DataBaseTools();
            bool IsPropertyExist = false;
            DBT.CMD.CommandText = "SELECT id from Favourites where UserId=@UserId and RecordId=@RecordId";
            DBT.CMD.Parameters.AddWithValue("@UserId", UserId);
            DBT.CMD.Parameters.AddWithValue("@RecordId", RecordId);
            DBT.TBL.Load(DBT.CMD.ExecuteReader());

            if (DBT.TBL.Rows.Count > 0)
            {
                IsPropertyExist = true;
                return IsPropertyExist;
            }
            return IsPropertyExist;
        }
        private City[] LoadProjects()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select Record_Id,Description from tbProperty_Master where Exp4='Yes' order by Description asc";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            City[] cities = new City[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < cities.Length; i++)
                {
                    cities[i] = new City
                    {
                        Name = DBT.TBL.Rows[i]["Description"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                    };
                }
            }
            return cities;
        }
        private tbUnit_Master[] LoadPrices()
        {
            DataBaseTools DBT = new DataBaseTools();
          
            // DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No where City ='Dubai' and Community_No<>'0' and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct Roundoff_Value from tbUnit_Master order by Roundoff_Value asc";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbUnit_Master[] tbUnit_Masters = new tbUnit_Master[DBT.TBL.Rows.Count];
                    if (DBT.TBL.Rows.Count > 0)
                    {
                        for (int i = 0; i < tbUnit_Masters.Length; i++)
                        {
                         tbUnit_Masters[i] = new tbUnit_Master
                            {
                              Roundoff_Value = DBT.TBL.Rows[i]["Roundoff_Value"].ToString(),
                             
                            };
                        }
                    }
                    return tbUnit_Masters;
          }
        private tbProperty_Master[] LoadOtherCountries()
        {
            DataBaseTools DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            // DBT.CMD.CommandText = "select distinct City from tbProperty_Master  join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No  where City not in('Abu Dhabi','Dubai') and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct City from tbProperty_Master   where City not in('Abu Dhabi','Dubai','0') ";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbProperty_Master[] otherCountries = new tbProperty_Master[DBT.TBL.Rows.Count];

            if (DBT.TBL.Rows.Count > 0)
            {
                ViewBag.eBedroom_MasterCount = otherCountries.Length;
                for (int i = 0; i < otherCountries.Length; i++)
                {
                    otherCountries[i] = new tbProperty_Master
                    {
                        City = DBT.TBL.Rows[i]["City"].ToString()
                    };
                }
             
            }
            return otherCountries;
        }
        private tbProperty_Master[] LoadAboDahbi()
        {
            DataBaseTools DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            //DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No where City ='Abu Dhabi' and Community_No<>'0' and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master  where City ='Abu Dhabi' and Community_No<>'0' ";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbProperty_Master[] AbuDahbiApartments = new tbProperty_Master[DBT.TBL.Rows.Count];

            if (DBT.TBL.Rows.Count > 0)
            {
                ViewBag.eBedroom_MasterCount = AbuDahbiApartments.Length;
                for (int i = 0; i < AbuDahbiApartments.Length; i++)
                {
                    AbuDahbiApartments[i] = new tbProperty_Master
                    {
                        Community_No = DBT.TBL.Rows[i]["Community_No"].ToString()
                    };
                }

            }
            return AbuDahbiApartments;
        }
        private tbProperty_Master[] LoadDubai()
        {
            DataBaseTools DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            // DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No where City ='Dubai' and Community_No<>'0' and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master where City ='Dubai' and Community_No<>'0' ";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbProperty_Master[] DubaiApartments = new tbProperty_Master[DBT.TBL.Rows.Count];

            if (DBT.TBL.Rows.Count > 0)
            {
                ViewBag.eBedroom_MasterCount = DubaiApartments.Length;
                for (int i = 0; i < DubaiApartments.Length; i++)
                {
                    DubaiApartments[i] = new tbProperty_Master
                    {
                        Community_No = DBT.TBL.Rows[i]["Community_No"].ToString()
                    };
                }
           
            }
            return DubaiApartments;
        }
        private tbUnit_Master[] LoadAreas()
        {
            DataBaseTools DBT = new DataBaseTools();
            
            // DBT.CMD.CommandText = "select distinct Community_No from tbProperty_Master join tbUnit_Master on tbUnit_Master.Property_No=tbProperty_Master.Property_No where City ='Dubai' and Community_No<>'0' and PublishStatus='yes'";
            DBT.CMD.CommandText = "select distinct Total_Area from tbUnit_Master order by Total_Area asc";

            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbUnit_Master[] areasList = new tbUnit_Master[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {

                ViewBag.prices = areasList.Length;
                for (int i = 0; i < areasList.Length; i++)
                {
                    areasList[i] = new tbUnit_Master
                    {
                        Total_Area = DBT.TBL.Rows[i]["Total_Area"].ToString()
                    };
                }

            }
            return areasList;
        }
        private tbProperty_Master[] LoadProperties()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT = new DataBaseTools();
            ViewBag.DeveloperCount = 0;
            DBT.CMD.CommandText = "select distinct Property_Type from tbProperty_Master where Property_Type<>'0'";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbProperty_Master[] tbProperty_Masters = new tbProperty_Master[DBT.TBL.Rows.Count];

            if (DBT.TBL.Rows.Count > 0)
            {
                 ViewBag.eUnitCategory_MasterCount = tbProperty_Masters.Length;
                for (int i = 0; i < tbProperty_Masters.Length; i++)
                {
                    tbProperty_Masters[i] = new tbProperty_Master
                    {
                        // Unit_Type = DBT.TBL.Rows[i]["Unit_Type"].ToString(),
                        Property_Type = DBT.TBL.Rows[i]["Property_Type"].ToString(),
                        //   Record_Id = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                    };
                }
            }
         return  tbProperty_Masters;           
        }
        private City[] LoadDevelopers()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select * from Developer order by Name asc;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            City[] cities = new City[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < cities.Length; i++)
                {
                    cities[i] = new City
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["RecordId"].ToString(),
                    };
                }
            }
            return cities;
        }

        private Feature[] LoadFeatures()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select Description Name,Record_Id RecordId from tbUnitFeatures_Master order by Description asc";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            Feature[] features = new Feature[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < features.Length; i++)
                {
                    features[i] = new Feature
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["RecordId"].ToString(),
                    };
                }
            }
            return features;
        }
        private City[] LoadBedrooms()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select * from eBedroom_Master order by Bedrooms desc;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            City[] cities = new City[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < cities.Length; i++)
                {
                    cities[i] = new City
                    {
                        Name = DBT.TBL.Rows[i]["Bedrooms"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["Record_Id"].ToString(),
                    };
                }
            }
            return cities;
        }
        private City[] LoadCities()
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.CommandText = "select City_Desc Name,Record_Id RecordId from eCountryCity_Master where Country_Desc = 'United Arab Emirates' order by City_Desc";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            City[] cities = new City[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < cities.Length; i++)
                {
                    cities[i] = new City
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        RecordId = DBT.TBL.Rows[i]["RecordId"].ToString(),
                    };
                }
            }
            return cities;
        }
    }
}