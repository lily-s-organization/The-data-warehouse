﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Mooc.Common.Utils;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;

namespace Mooc.Web.UI.Controllers
{
    public class LoginController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var result = db.Users.Where(x => x.UserName == user.UserName && x.PassWord == user.PassWord && x.UserState == 0).SingleOrDefault();
            if (result == null)
            {
                return Json(300);
            }
            else
            {
                //用户登录后 默认存cookie一小时
                CookieHelper.SetCookie(CommonVariables.LoginCookieName, result.UserName, CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieID, result.Id.ToString(), CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieType, result.RoleType.ToString(), CookieHelper.TimeUtil.H, "1");
                return Json(0);
            }
        }


        public ActionResult Logout()
        {

            CookieHelper.DelCookie(CommonVariables.LoginCookieName);
            CookieHelper.DelCookie(CommonVariables.LoginCookieID);
            CookieHelper.DelCookie(CommonVariables.LoginCookieType);
            return RedirectToAction("Index", "Home");
          
        }

        public JsonResult RegainPassword(string username)
        {
            var result = db.Users.Where(x => x.UserName == username).SingleOrDefault();
            if (result == null)
            {
                return Json(300);
            }
            else
            {
                //发送邮件
               
                MailMessage mailMessage = new MailMessage("from_@gmail.com", "to_@gmail.com");            
                mailMessage.Body = "Hi,this is your password:" + result.PassWord; 
                mailMessage.Subject = "Password of Your Mooc account";                
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);             
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "from_@gmail.com",
                    Password = "password"
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return Json(0);
            }
        }

       
    }
}