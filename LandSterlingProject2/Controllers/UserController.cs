using LandSterlingProject2.Helpers;
using LandSterlingProject2.Models;
using LandSterlingProject2.Models.models_ado;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncryptHandler = LandSterlingProject2.Helpers.EncryptHandler;

namespace LandSterlingProject2.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult Login(UserloginViewModel model)
        {
/*            if (!ModelState.IsValid)
            {
                return false;
            }*/
            DataBaseTools DBT = new DataBaseTools();
            var encryptpassowrd = EncryptHandler.Encrypt(model.Password);
            bool IsValid = false;
            DBT.CMD.CommandText = "SELECT Id,Email,UserName FROM Users where Email=@Email AND HashedPassword=@Password_hash ";
            DBT.CMD.Parameters.AddWithValue("@Email", model.Email);
            DBT.CMD.Parameters.AddWithValue("@Password_hash", encryptpassowrd);
            DBT.TBL.Load(DBT.CMD.ExecuteReader());
            if (DBT.TBL.Rows.Count > 0)
            {
                var UserName = DBT.TBL.Rows[0]["UserName"].ToString();
                Session["UserName"] = UserName;
                Session["UserID"] = DBT.TBL.Rows[0]["Id"];
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
       
        public ActionResult  Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
               // return PartialView("~/Views/Shared/_registeration.cshtml", model);
            }
            //if (IsUserExist(model.Email))
            //{
            //  //  ViewBag.ErrorMsg = " Error, Email Exist";
              //  return PartialView("~/Views/Shared/_registeration.cshtml", model);//model); // PartialView("~/Views/Shared/_registeration.cshtml", model);
                //return Json(new
                //{
                //    Status = 1,
                //    Message = "Email Exist Before",
                //    AjaxReturn = PartialView("_registeration", model)//.RenderToString()
                //}); //View(model);
            //}

            DataBaseTools DBT = new DataBaseTools();
            var encryptpassowrd = EncryptHandler.Encrypt(model.Password);
        
            DBT.CMD.CommandText = "INSERT INTO Users(UserName,Email,HashedPassword,PhoneNumber) values(@UserName,@Email,@Password_hash,@PhoneNumber) ";
            DBT.CMD.Parameters.AddWithValue("@UserName", model.Name);
            DBT.CMD.Parameters.AddWithValue("@Email", model.Email);
            DBT.CMD.Parameters.AddWithValue("@Password_hash", encryptpassowrd);
            DBT.CMD.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber1);
            
            DBT.TBL.Load(DBT.CMD.ExecuteReader());

            var UserName = model.Name;
            Session["UserName"] = UserName;
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult SaveFavourites(string RecordId)
        {
            if (Session["UserID"] != null)
            {
                DataBaseTools DBT = new DataBaseTools();

                DBT.CMD.CommandText = "INSERT INTO Favourites(UserId,RecordId) values(@UserId,@RecordId) ";
                DBT.CMD.Parameters.AddWithValue("@UserId", Session["UserID"]);
                DBT.CMD.Parameters.AddWithValue("@RecordId", RecordId);

                DBT.TBL.Load(DBT.CMD.ExecuteReader());
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UnSaveFavourites(string RecordId)
        {
            if (Session["UserID"] != null)
            {

                DataBaseTools DBT = new DataBaseTools();

                DBT.CMD.CommandText = "Delete from Favourites where UserId=@UserId and RecordId=@RecordId ";
                DBT.CMD.Parameters.AddWithValue("@UserId", Session["UserID"]);
                DBT.CMD.Parameters.AddWithValue("@RecordId", RecordId);

                DBT.TBL.Load(DBT.CMD.ExecuteReader());
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        [HttpPost]
        public JsonResult IsUserExist(string Email)
        {
            //test changes
            //test changes
            DataBaseTools DBT = new DataBaseTools();
            bool IsUserExist = false;
            DBT.CMD.CommandText = "SELECT Email from Users where Email=@Email";
            DBT.CMD.Parameters.AddWithValue("@Email", Email);
            DBT.TBL.Load(DBT.CMD.ExecuteReader());

            if (DBT.TBL.Rows.Count > 0)
            {
                 IsUserExist = true;
               return Json(IsUserExist);
            }
            return Json(IsUserExist);
        }
        public ActionResult GetUserName()
        {
            string UserName = "";
            if (Session["UserName"] != null)
            {
              UserName = Session["UserName"].ToString();

            }     
            return Content(UserName);
        }
        public ActionResult Logout()
        {

            Session["UserName"] = null;
            Session.Abandon();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}