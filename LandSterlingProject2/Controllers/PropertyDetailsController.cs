using LandSterlingProject2.Helpers;
using LandSterlingProject2.Models;
using LandSterlingProject2.Models.models_ado;
using LandSterlingProject2.Properties;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;
//using PerpetuumSoft.Knockout;

namespace LandSterlingProject2.Controllers
{
    public class PropertyDetailsController : Controller
    {
        // GET: PropertyDetails
        public ActionResult Index(string Id = "", string PropertyType = "Unit")
        {
            if (!ValidLicense())
            {
                return View("UpdateRequired");
            }
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.Parameters.AddWithValue("@RecordId", Id);
            if (PropertyType == "Unit")
                DBT.CMD.CommandText = @" select '' PostHandover,u.Record_Id,u.Remarks as Description,u.Property_Name+', Ref# '+u.Ref_No as Name,U.Floor,pm.Record_Id AS property_record_Id,
                                         Replace(Replace((select top 1 Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1, 
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 1 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath2, 
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 2 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath3,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 3 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath4,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 4 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath5,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 5 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath6,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 6 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath7,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 7 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath8,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 8 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath9,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 9 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath10,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 10 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath11,
                                         Replace(Replace((select Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc OFFSET 11 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath12,                                  
                                         pm.Longitude, pm.city as CountryLocation,pm.Exp9 as FristInstallment,pm.Exp11 as Handover,pm.Exp10 as Underconstruction,pm.Exp12 as Posthandover,pm.Latitude,u.Adv_Remarks2 VideoUrl,Replace(Replace(U.Unit_Class,'Rent','For Rent'),'Buy','For Sale') as Type,pm.City+', '+pm.Community_No as Location,pm.Community_No as Address,U.Created_On,pm.Developer,
                                         replace(u.Total_Value,'.00','') as Price,replace(u.Total_Value,'.00','') as PriceTo,REPLACE(u.Total_Area,'.00','') as Size,REPLACE(u.Total_Area,'.00','') as SizeTo,u.Exp1 as MarketingText,u.Adv_Remarks1 as Image360,u.Bedrooms as Rooms,u.Floor,u.Ref_No,pm.Property_No,
                                         
                                          pm.Exp6 CompletionDate
                                          from tbUnit_Master u,tbProperty_Master pm where pm.Property_No=u.Property_No and u.Record_Id=@RecordId ;";
            else
                DBT.CMD.CommandText = @"select round((select min(cast(replace(u.Total_Value,'.00','') as float))  from tbUnit_Master u where u.Property_No=pm.Property_No ),0) as Price,
                                        round((select MAX(cast(replace(u.Total_Value,'.00','') as float))  from tbUnit_Master u where u.Property_No=pm.Property_No ),0) as PriceTo,
                                        round((select min(cast(replace(u.Total_Area,'.00','') as float))  from tbUnit_Master u where u.Property_No=pm.Property_No ),0) as Size,
                                        round((select MAX(cast(replace(u.Total_Area,'.00','') as float))  from tbUnit_Master u where u.Property_No=pm.Property_No ),0) as SizeTo,
                                        pm.Exp5 as MarketingText,pm.Exp8 as Image360,Remarks as Description,pm.Exp9 as Floor,pm.Exp11 as Rooms,pm.Exp12 as PostHandover,pm.Exp10 as Type,pm.Record_Id,pm.Description Name,
                                        Replace(Replace((select top 1 Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id  asc OFFSET 1 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath2,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 2 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath3,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 3 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath4,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 4 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath5,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 5 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath6,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 6 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath7,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 7 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath8,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 8 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath9,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 9 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath10,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 10 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath11,
                                        Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 11 Rows FETCH  Next 1 Rows ONLY),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath12,
                                        pm.city as CountryLocation,pm.City+', '+pm.Community_No as Location,pm.Exp9 as FristInstallment,pm.Exp11 as  Handover,pm.Exp10 as Underconstruction,pm.Exp12 as Posthandover,pm.Community_No as Address,pm.CreatedOn as Created_On,pm.Developer,pm.Latitude,pm.Longitude,pm.Exp7 as VideoUrl,pm.Property_No,
                                        pm.Exp6 as CompletionDate
                                        from tbProperty_Master pm where pm.Record_Id=@RecordId ;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            Property property = new Property();
            if (DBT.TBL.Rows.Count > 0)
            {

                Property obj = new Property();

                if (PropertyType == "Unit")
                {
                    string recId = DBT.TBL.Rows[0]["property_record_Id"].ToString();

                    if (DBT.TBL.Rows[0]["ImagePath1"].ToString() == "")
                    {
                        obj = GetImages(recId);
                    }
                    else
                    {

                        obj.ImagePath1 = DBT.TBL.Rows[0]["ImagePath1"].ToString();
                        obj.ImagePath2 = DBT.TBL.Rows[0]["ImagePath2"].ToString();
                        obj.ImagePath3 = DBT.TBL.Rows[0]["ImagePath3"].ToString();
                        obj.ImagePath4 = DBT.TBL.Rows[0]["ImagePath4"].ToString();
                        obj.ImagePath5 = DBT.TBL.Rows[0]["ImagePath5"].ToString();
                        obj.ImagePath6 = DBT.TBL.Rows[0]["ImagePath6"].ToString();
                        obj.ImagePath7 = DBT.TBL.Rows[0]["ImagePath7"].ToString();
                        obj.ImagePath8 = DBT.TBL.Rows[0]["ImagePath8"].ToString();
                        obj.ImagePath9 = DBT.TBL.Rows[0]["ImagePath9"].ToString();
                        obj.ImagePath10 = DBT.TBL.Rows[0]["ImagePath10"].ToString();
                        obj.ImagePath11 = DBT.TBL.Rows[0]["ImagePath11"].ToString();
                        obj.ImagePath12 = DBT.TBL.Rows[0]["ImagePath12"].ToString();
                    }

                    //obj.ImagePath1 = obj.ImagePath2 = obj.ImagePath3 = obj.ImagePath4 = obj.ImagePath5 = obj.ImagePath6 = obj.ImagePath7 = obj.ImagePath8 = obj.ImagePath9 = obj.ImagePath10 = obj.ImagePath11 = obj.ImagePath12 = "~/images/P10208_637925296414690000.jpg";
                }


                property = new Property
                {
                    CountryLocation = DBT.TBL.Rows[0]["CountryLocation"].ToString(),
                    RefNumber = DBT.TBL.Rows[0]["Ref_No"].ToString(),
                    PropertyNo = DBT.TBL.Rows[0]["Property_No"].ToString(),
                    Address = DBT.TBL.Rows[0]["Address"].ToString(),
                    CreatedOn = Convert.ToDateTime(DBT.TBL.Rows[0]["Created_On"]).ToString("dd/MMM/yyyy", new CultureInfo("en-US")),
                    Developer = DBT.TBL.Rows[0]["Developer"].ToString() == "0" ? "" : DBT.TBL.Rows[0]["Developer"].ToString(),
                    Floor = DBT.TBL.Rows[0]["Floor"].ToString(),
                    Location = DBT.TBL.Rows[0]["Location"].ToString(),
                    Price = DBT.TBL.Rows[0]["Price"].ToString().KiloFormat(),
                    Rooms = DBT.TBL.Rows[0]["Rooms"].ToString(),
                    Size = DBT.TBL.Rows[0]["Size"].ToString(),
                    SizeTo = DBT.TBL.Rows[0]["SizeTo"].ToString(),
                    PriceTo = DBT.TBL.Rows[0]["PriceTo"].ToString().KiloFormat(),
                    Image360 = DBT.TBL.Rows[0]["Image360"].ToString(),
                    MarketingText = DBT.TBL.Rows[0]["MarketingText"].ToString(),
                    Type = DBT.TBL.Rows[0]["Type"].ToString(),
                    Name = DBT.TBL.Rows[0]["Name"].ToString(),
                    RecordId = DBT.TBL.Rows[0]["Record_Id"].ToString(),
                    Description = DBT.TBL.Rows[0]["Description"].ToString(),
                    PostHandover = DBT.TBL.Rows[0]["Posthandover"].ToString(),
                    FristInstallment = DBT.TBL.Rows[0]["FristInstallment"].ToString(),
                    UnderConstruction = DBT.TBL.Rows[0]["Underconstruction"].ToString(),
                    Handover = DBT.TBL.Rows[0]["Handover"].ToString(),
                    ImagePath1 = obj.ImagePath1 == null ? DBT.TBL.Rows[0]["ImagePath1"].ToString() : obj.ImagePath1,
                    ImagePath2 = obj.ImagePath2 == null ? DBT.TBL.Rows[0]["ImagePath2"].ToString() : obj.ImagePath2,
                    ImagePath3 = obj.ImagePath3 == null ? DBT.TBL.Rows[0]["ImagePath3"].ToString() : obj.ImagePath3,
                    ImagePath4 = obj.ImagePath4 == null ? DBT.TBL.Rows[0]["ImagePath4"].ToString() : obj.ImagePath4,
                    ImagePath5 = obj.ImagePath5 == null ? DBT.TBL.Rows[0]["ImagePath5"].ToString() : obj.ImagePath5,
                    ImagePath6 = obj.ImagePath6 == null ? DBT.TBL.Rows[0]["ImagePath6"].ToString() : obj.ImagePath6,
                    ImagePath7 = obj.ImagePath7 == null ? DBT.TBL.Rows[0]["ImagePath7"].ToString() : obj.ImagePath7,
                    ImagePath8 = obj.ImagePath8 == null ? DBT.TBL.Rows[0]["ImagePath8"].ToString() : obj.ImagePath8,
                    ImagePath9 = obj.ImagePath9 == null ? DBT.TBL.Rows[0]["ImagePath9"].ToString() : obj.ImagePath9,
                    ImagePath10 = obj.ImagePath10 == null ? DBT.TBL.Rows[0]["ImagePath10"].ToString() : obj.ImagePath10,
                    ImagePath11 = obj.ImagePath11 == null ? DBT.TBL.Rows[0]["ImagePath11"].ToString() : obj.ImagePath11,
                    ImagePath12 = obj.ImagePath12 == null ? DBT.TBL.Rows[0]["ImagePath12"].ToString() : obj.ImagePath12,

                    Latitude = DBT.TBL.Rows[0]["Latitude"].ToString(),
                    Longitude = DBT.TBL.Rows[0]["Longitude"].ToString(),
                    //VideoUrl = obj.VideoUrl == null ? DBT.TBL.Rows[0]["VideoUrl"].ToString() : videoUrl,
                    PropertyType = PropertyType,
                    CompletionDate = DBT.TBL.Rows[0]["CompletionDate"].ToString()

                };
                string videoUrl = "";
                if (DBT.TBL.Rows[0]["VideoUrl"].ToString() == "0")
                {
                    videoUrl = GetVideoPro(property.PropertyNo);
                    obj.VideoUrl = videoUrl;

                }
                else
                {
                    obj.VideoUrl = DBT.TBL.Rows[0]["VideoUrl"].ToString();
                }
                property.VideoUrl = obj.VideoUrl == null ? DBT.TBL.Rows[0]["VideoUrl"].ToString() : videoUrl;
                property.Purpos = GetProppertyPurpos(property.PropertyNo);
                ViewBag.Property = property;

                /* get the features of unit and property*/
                if (PropertyType == "unite")
                {
                    Feature[] featrures = GetUnitFeature(property.RefNumber);
                    featrures.Concat(GetPropertyFeature(property.PropertyNo));
                    ViewBag.features = featrures;

                }
                else
                {
                    Feature[] featrures = GetPropertyFeature(property.PropertyNo);

                    ViewBag.features = featrures;
                }
                DBT = new DataBaseTools();
                DBT.CMD.Parameters.AddWithValue("@RecordId", Id);
                DBT.CMD.Parameters.AddWithValue("@Ref_No", property.RefNumber);
                DBT.CMD.CommandText = "select distinct Views_No from tbUnit_detail  where Ref_No=@Ref_No ;";

                DBT.TBL.Load(DBT.CMD.ExecuteReader());
                tbUnit_detail[] CountList = new tbUnit_detail[DBT.TBL.Rows.Count];

                if (DBT.TBL.Rows.Count > 0)
                {
                    DataBaseTools DBT1 = new DataBaseTools();

                    for (int i = 0; i < CountList.Length; i++)
                    {
                        CountList[i] = new tbUnit_detail
                        {
                            Views_No = (int)DBT.TBL.Rows[i]["Views_No"]
                        };
                        DBT1.CMD.Parameters.AddWithValue("@Views_No", CountList[i].Views_No + 1);
                        DBT1.CMD.Parameters.AddWithValue("@Ref_No", property.RefNumber);
                        DBT1.CMD.CommandText = "update tbUnit_Detail set Views_No = @Views_No where Ref_No = @Ref_No";


                        DBT1.CMD.ExecuteNonQuery();
                    }
                }
                else

                {
                    DataBaseTools DBT1 = new DataBaseTools();

                    DBT1.CMD.Parameters.AddWithValue("@Views_No", 1);
                    DBT1.CMD.Parameters.AddWithValue("@Ref_No", property.RefNumber);
                    //    DBT1.CMD.Parameters.AddWithValue("@Record_Id", property.RecordId);

                    DBT1.CMD.CommandText = "insert into tbUnit_Detail(Views_No,Ref_No) values(@Views_No,@Ref_No)";

                    DBT1.CMD.ExecuteNonQuery();
                    //  ViewBag.CountList = CountList;
                }

                /*get recommended property*/
                Property[] properties = GetRecommenedProparty(property.Location, property.Type);
                ViewBag.RecommendedProperty = properties;

            }
            //get the listing user
            ListingUser listinguser = GetListingUser(property.RefNumber);
            ViewBag.listinguser = listinguser;
            /* get otherCountries apartment */
            tbProperty_Master[] othercountries = GetOtherCountries();
            ViewBag.Othercountries = othercountries;
            /* get AbuDahbiApartments*/
            tbProperty_Master[] abuDahbiapartments = AbuDahbiApartments();
            ViewBag.AbuDahbiApartments = abuDahbiapartments;
            /*get  DubaiApartments*/
            tbProperty_Master[] dubaiapartments = DubaiApartments();
            ViewBag.DubaiApartments = dubaiapartments;
            tbUnit_detail model = new tbUnit_detail();

            // model.RegisterClick();

            return View();
        }
        //get the listing Owner {
        public ListingUser GetListingUser(string RefNo)
        {
            DataBaseTools DBT = new DataBaseTools();
            Property property = new Property();

            DBT.CMD.CommandText = $@" select User_Fullname ,Image_path from ePMS..tbUser_Master where company = '2040' and User_Name = (select ListingOwner from tbUnit_CRMDetail where Ref_No = '{RefNo}')";
            DBT.CMD.Parameters.AddWithValue("@RefNo", RefNo);
            ListingUser listinguser = new ListingUser();
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                listinguser = new ListingUser()
                {
                    Name = DBT.TBL.Rows[0]["User_Fullname"].ToString(),
                    ImagePath = DBT.TBL.Rows[0]["Image_path"].ToString()
                };
            }
            return listinguser;

        }

        //videos if null
        public string GetVideoPro(string Id)
        {
            DataBaseTools DBT = new DataBaseTools();
            Property property = new Property();
            DBT.CMD.CommandText = $@"select REPLACE(pm.Exp7,0,'https://www.youtube.com/embed/-JEiV5fuYZU') as VideoUrl from tbProperty_Master pm where pm.Property_No='{Id}' ;";
            string VideoUrl = "";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                VideoUrl = DBT.TBL.Rows[0]["VideoUrl"].ToString();
            };
            return VideoUrl;

        }
        /* get the purpos of property */
        public string GetProppertyPurpos(string propNo)
        {
            DataBaseTools DBT = new DataBaseTools();
            var PropertyPurpos = "";
            DBT.CMD.CommandText = $@"select RentOrSale from tbProperty_details  where  Property_No='{propNo}' ";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            tbProperty_Master[] DubaiApartments = new tbProperty_Master[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                PropertyPurpos = DBT.TBL.Rows[0]["RentOrSale"].ToString();
            }
            return PropertyPurpos;
        }
        /* get dubi apartment*/

        public tbProperty_Master[] DubaiApartments()
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
        /* get abudubhi apartment*/
        public tbProperty_Master[] AbuDahbiApartments()
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
        /* get otherCountries apartment*/
        public tbProperty_Master[] GetOtherCountries()
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
        /* get recomended feature by the same location and price */
        public Property[] GetRecommenedProparty(string location, string type)
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT = new DataBaseTools();
            DBT.CMD.Parameters.AddWithValue("@location", location);
            DBT.CMD.Parameters.AddWithValue("@Type", type);

            DBT.CMD.CommandText = "  select top 3 u.Record_Id,pm.Property_No,u.Property_Name as Name,U.Floor,Replace(Replace((select top 1 Exp1 from tbUnit_Image  ui where ui.Ref_No=u.Ref_No order by Record_Id asc),'\','/'),'C:/inetpub/wwwroot/CRM/','https://crm.landsterling.belsio.io/') ImagePath1, Replace(Replace(U.Unit_Class,'Rent','For Rent'),'Buy','For Sale') as Type,pm.City+', '+pm.Community_No as Location,pm.Community_No as Address,U.Created_On,pm.Developer,replace(u.Total_Value,'.00','') as Price,ROUND(REPLACE(u.Total_Area,'.00',''),0) as Size,u.Bedrooms as Rooms,u.Floor from tbUnit_Master u,tbProperty_Master pm where pm.Property_No=u.Property_No and u.PublishStatus='yes' and pm.City+', '+pm.Community_No=@location and Replace(Replace(U.Unit_Class,'Rent','For Rent'),'Buy','For Sale')=@Type order by u.Record_Id desc;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            Property[] properties = new Property[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {

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
            }
            return properties;
        }
        /* Get unit feature*/
        private Feature[] GetUnitFeature(string RefNumber)
        {
            DataBaseTools DBT = new DataBaseTools();

            DBT.CMD.Parameters.AddWithValue("@RefNumber", RefNumber);
            DBT.CMD.CommandText = "select tbUnitFeatures_Master.Description as Name, tbUnitFeatures_Master.Description_BI as Icon\r\nfrom tbUnit_Feature\r\nINNER JOIN tbUnitFeatures_Master  ON tbUnitFeatures_Master.Description=tbUnit_Feature.Description\r\nwhere tbUnit_Feature.Ref_No=@RefNumber;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            Feature[] Unitefeatures = new Feature[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < Unitefeatures.Length; i++)
                {
                    Unitefeatures[i] = new Feature
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        Img = DBT.TBL.Rows[i]["Icon"].ToString(),
                    };
                }
            }
            return Unitefeatures;
        }
        private Feature[] GetPropertyFeature(string PropertyNumber)
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.Parameters.AddWithValue("@PropertyNumber", PropertyNumber);
            DBT.CMD.CommandText = "select tbUnitFeatures_Master.Description as Name , tbUnitFeatures_Master.Description_BI as Icon\r\nfrom tbUnitFeatures_Master\r\nINNER JOIN tbProperty_Feature  ON tbUnitFeatures_Master.Description=tbProperty_Feature.Description\r\nwhere tbProperty_Feature.Property_No=@PropertyNumber;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            Feature[] Unitefeatures = new Feature[DBT.TBL.Rows.Count];
            if (DBT.TBL.Rows.Count > 0)
            {
                for (int i = 0; i < Unitefeatures.Length; i++)
                {
                    Unitefeatures[i] = new Feature
                    {
                        Name = DBT.TBL.Rows[i]["Name"].ToString(),
                        Img = DBT.TBL.Rows[i]["Icon"].ToString(),
                    };
                }
            }
            return Unitefeatures;
        }

        public Property GetImages(string Id)
        {
            DataBaseTools DBT = new DataBaseTools();
            Property property = new Property();
            DBT.CMD.Parameters.AddWithValue("@Record_Id", Id);
            DBT.CMD.CommandText = $@"select  Replace(Replace((select top 1 Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath1,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id  asc OFFSET 1 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath2,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 2 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath3,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 3 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath4,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 4 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath5,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 5 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath6,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 6 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath7,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 7 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath8,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 8 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath9,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 9 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath10,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 10 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath11,
                                            Replace(Replace((select Exp1 from tbProperty_Image  pi where pi.Ref_No=pm.Property_No order by Record_Id asc OFFSET 11 Rows FETCH  Next 1 Rows ONLY),'\','/'),'" + Settings.Default.FolderPath + @"','" + Settings.Default.CRMLink + @"') ImagePath12
                                            from tbProperty_Master pm where pm.Record_Id=@Record_Id ;";
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            List<string> imgs = new List<string>();
            if (DBT.TBL.Rows.Count > 0)
            {

                property.ImagePath1 = DBT.TBL.Rows[0]["ImagePath1"].ToString();
                property.ImagePath2 = DBT.TBL.Rows[0]["ImagePath2"].ToString();
                property.ImagePath3 = DBT.TBL.Rows[0]["ImagePath3"].ToString();
                property.ImagePath4 = DBT.TBL.Rows[0]["ImagePath4"].ToString();
                property.ImagePath5 = DBT.TBL.Rows[0]["ImagePath5"].ToString();
                property.ImagePath6 = DBT.TBL.Rows[0]["ImagePath6"].ToString();
                property.ImagePath7 = DBT.TBL.Rows[0]["ImagePath7"].ToString();
                property.ImagePath8 = DBT.TBL.Rows[0]["ImagePath8"].ToString();
                property.ImagePath9 = DBT.TBL.Rows[0]["ImagePath9"].ToString();
                property.ImagePath10 = DBT.TBL.Rows[0]["ImagePath10"].ToString();
                property.ImagePath11 = DBT.TBL.Rows[0]["ImagePath11"].ToString();
                property.ImagePath12 = DBT.TBL.Rows[0]["ImagePath12"].ToString();
            };
            return property;

        }
        private bool ValidLicense()
        {
            return true;
        }
        public JsonResult SendEmail(Emailmodel emailmodel)
        {
            var EmailSent = false;
            DataBaseTools DBT = new DataBaseTools();
            DBT = new DataBaseTools();
            DBT.CMD.CommandText = "msdb.dbo.sp_send_dbmail @profile_name='SendPaymentValueEmail',@recipients='" + emailmodel.Email + "',@subject='ePMS FM',@body='the mothely payment" + emailmodel.MonthlyPayment + " and total invest value " + emailmodel.InvestmentValue + "';--,@body_format ='HTML';";
            DBT.CMD.ExecuteReader();
            EmailSent = true;
            return Json(EmailSent);
        }
        public JsonResult ContactUs(ContactUs contact)
        {
            var IsSent = false;
            try
            {

                DataBaseTools DBT = new DataBaseTools();

                DBT.CMD.Parameters.AddWithValue("@FullName", contact.FullName);
                DBT.CMD.Parameters.AddWithValue("@Email", contact.Email);
                DBT.CMD.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber1 != null ? contact.PhoneNumber1 : "");
                DBT.CMD.Parameters.AddWithValue("@Message", string.IsNullOrEmpty(contact.Message) ? "" : contact.Message);
                DBT.CMD.Parameters.AddWithValue("@KeepMeInformed", contact.KeepMeInformed);
                DBT.CMD.CommandText = "insert into eCC_ClientLeads(FullName,Email,PhoneNumber,Exp1,KeepMeInformed) values(@FullName,@Email,@PhoneNumber,@Message,@KeepMeInformed)";
                DBT.CMD.ExecuteNonQuery();
                IsSent = true;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                IsSent = false;
            }
            return Json(IsSent);
        }

    }
}
