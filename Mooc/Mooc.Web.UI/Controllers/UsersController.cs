using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.ViewModels;

namespace Mooc.Web.UI.Controllers
{
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(UsersController));
        // GET: Users
        public ActionResult Index()
        {
            try
            {
                return View(db.Users.ToList());
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return View();
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update(long id)
        {
            try
            {
                User user = new User();
                user = db.Users.Where(x => x.Id == id).FirstOrDefault<User>();
                return View("Details", user);//If you return the view that is the same with action, you don't need to use "update"

                //return action
               // return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return View();
            }
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
                else
                {
                    return View("Update", user);
                }
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return View("Update", user);
            }
            

          
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
                    return RedirectToAction("Index");//因为是 form  提交数据  没有回调函数  所以 执行完 直接跳转到列表页
                    //  return Json(new { success = true, message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
              
                }
                else
                {                  
                    return View("Create", viewModel);
                    // return Json(new { success = false, message = "Model state is not valid" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return View("Create",viewModel);
                //用日志记录ex

               // return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,PassWord,Email,NickName,RoleType")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,PassWord,Email,NickName,UserState,RoleType,AddTime")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new { success = true, message = "Deleted Successfully", userid = id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return View("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
