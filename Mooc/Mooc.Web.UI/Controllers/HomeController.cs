using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooc.DataAccess.Models.ViewModels;
using Mooc.Common.Utils;

namespace Mooc.Web.UI.Controllers
{
    
    public class HomeController : Controller
    {
        private ILog logger = LogManager.GetLogger(typeof(HomeController));

        private readonly IUsersService _usersService;

       
        public HomeController(IUsersService usersService)
        {
            this._usersService = usersService;
        }
        public ActionResult Index()
        {
            //CookieHelper.SetCookie(CommonVariables.LoginCookieName, "test", CookieHelper.TimeUtil.H, "2");//2h

           // string name = CookieHelper.GetCookie(CommonVariables.LoginCookieName);//get cookie
           // CookieHelper.SetCookie(CommonVariables.LoginCookieName, "", CookieHelper.TimeUtil.H, "2");//2h

            using (DataContext db = new DataContext())
            {
                var list = db.Users.Where(u => u.Id > 0).ToList();

                List<UserViewModel> models = AutoMapper.Mapper.Map<List<UserViewModel>>(list);//AutoMapper
            }

            var list2 = this._usersService.UserList();

            logger.Info("测试1");
            logger.Warn("测试");
            logger.Debug("测试");
            //  logger.Error("测试2ERROR");

            return View();
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
    }
}