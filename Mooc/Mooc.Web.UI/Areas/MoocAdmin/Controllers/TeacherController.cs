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
    [LoginAuthorize]
    public class TeacherController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(TeacherController));
        // GET: MoocAdmin/Teacher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTeacherList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;
            var list = db.Teachers.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).ToList();
            int iCount = db.Teachers.Count(x => x.Id > 0);
            return Json(new { code = 0, data = list, iCount = iCount });

        }

        public ActionResult Details(int id)
        {
            try
            {
                ViewBag.Id = id;
                return View();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult AddTeacherList(Teacher teacher)
        {
            try
            {
                if (teacher == null)
                    return Json(300);

                if (teacher.Id == 0)
                {
                    teacher.State = 0;
                    teacher.AddTime = DateTime.Now;
                    db.Teachers.Add(teacher);
                }
                else
                {
                    var tmp = db.Teachers.Find(teacher.Id);
                    teacher.State = tmp.State;
                    teacher.AddTime = tmp.AddTime;                     
                    db.Entry(tmp).State = EntityState.Detached;
                    db.Entry(teacher).State = EntityState.Modified;
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

        [HttpPost]
        public JsonResult GetTeacherDetail(long id)
        {
            return Json(db.Teachers.Find(id));
        }




        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Teacher teacher = db.Teachers.Find(id);
                if (teacher == null)
                    return Json(500);

                db.Teachers.Remove(teacher);
                db.SaveChanges();
                return Json(0);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Json(300);
            }
        }
    }
}