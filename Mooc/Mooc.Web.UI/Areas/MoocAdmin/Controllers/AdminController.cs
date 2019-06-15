using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooc.Common.Utils;
using Mooc.DataAccess.Models.Context;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();

        // GET: MoocAdmin/Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            var result = db.Users.Where(x => x.UserName == username && x.PassWord == password && x.UserState == 0 && x.RoleType == 1).SingleOrDefault();
            if (result == null)
            {
                return Json(0);
            }
            else
            {
                //用户登录后 默认存cookie一小时
                CookieHelper.SetCookie(CommonVariables.LoginCookieName, result.UserName, CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieID, result.Id.ToString(), CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieType, result.RoleType.ToString(), CookieHelper.TimeUtil.H, "1");
                return Json(200);
            }
        }

        public ActionResult Logout()
        {

            CookieHelper.DelCookie(CommonVariables.LoginCookieName);
            CookieHelper.DelCookie(CommonVariables.LoginCookieID);
            CookieHelper.DelCookie(CommonVariables.LoginCookieType);
            return RedirectToAction("Index");

        }
    }
}