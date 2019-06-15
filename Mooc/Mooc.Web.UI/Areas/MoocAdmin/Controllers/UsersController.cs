using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.Web.UI.Filter;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    //[LoginAuthorize]
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(UsersController));
        // GET: MoocAdmin/Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddUserList(User user)
        {
            try
            {
                if (user == null)
                    return Json(300);

                if (user.Id == 0)
                {
                    user.UserState = 0;
                    user.AddTime = DateTime.Now;
                    db.Users.Add(user);
                }
                else
                {
                    db.Entry(user).State = EntityState.Modified;
                }

                db.SaveChanges();
                return Json(0);           
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(400);
            }



        }
    }
}