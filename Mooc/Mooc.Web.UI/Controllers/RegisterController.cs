using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.Common.Utils;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Utils;

namespace Mooc.Web.UI.Controllers
{
    public class RegisterController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(UsersController));

        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            IList<SelectListItem> RoleNamelistItem = EnumModels.ToSelectList(typeof(EnumModels.RoleNameEnum));
            ViewData["RoleType"] = new SelectList(RoleNamelistItem, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user, HttpPostedFileBase ImageUpload)
        {
            try
            {
                if (ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyymmssfff") + extension;
                    string savePath = Server.MapPath("~/Images/Upload/");
                    if (!System.IO.Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    string saveFile = savePath + fileName;

                    ImageUpload.SaveAs(saveFile);
                    user.PhotoUrl = fileName;//编辑时要写全路径--迁移的时候比较容易
                }
                user.AddTime = DateTime.Now;
                user.UserState = 0;
                db.Users.Add(user);
                db.SaveChanges();

                CookieHelper.SetCookie(CommonVariables.LoginCookieName, user.UserName, CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieID, user.Id.ToString(), CookieHelper.TimeUtil.H, "1");
                CookieHelper.SetCookie(CommonVariables.LoginCookieType, user.RoleType.ToString(), CookieHelper.TimeUtil.H, "1");
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return RedirectToAction("Register");
            }
        }

    }
}