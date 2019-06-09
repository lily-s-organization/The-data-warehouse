using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpGet]
        public ActionResult Login()
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
                CookieHelper.SetCookie(CommonVariables.LoginCookieName, user.UserName, CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieID, user.Id.ToString(), CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieType, user.RoleType.ToString(), CookieHelper.TimeUtil.H, "1");
                return Json(0);
            }
        }


        public ActionResult Logout()
        {

            CookieHelper.DelCookie(CommonVariables.LoginCookieName);
            CookieHelper.DelCookie(CommonVariables.LoginCookieID);
            CookieHelper.DelCookie(CommonVariables.LoginCookieType);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

       
    }
}