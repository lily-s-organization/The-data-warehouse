using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.ViewModels;

namespace Mooc.Web.UI.Controllers
{
    public class Demo1Controller : Controller
    {
        // 使用 razor 绑定数据实现 User 的增删改查  EF + Automap
        // implement add delete update select functions in Demo1--using razor

        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(Demo1Controller));

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserViewModel viewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    User user = AutoMapper.Mapper.Map<User>(viewModel);//AutoMapper

                    user.AddTime = DateTime.Now.ToLocalTime();
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");


                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
               
            }
            return View("Create", viewModel);

        }

        public ActionResult Update(long id)
        {
            User user = db.Users.Where(x => x.Id == id).FirstOrDefault<User>();
            if (user == null)
                return HttpNotFound();

            return View(user);

            /*
            try
            {
                User user = new User();
                user = db.Users.Where(x => x.Id == id).FirstOrDefault<User>();
                return View(user);//If you return the view that is the same with action, you don't need to use "update"

                //return action
                // return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return View();
            }
            */
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(User user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }               
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                
            }

            return View("Update", user);

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return RedirectToAction("Index");
        }

    }
}