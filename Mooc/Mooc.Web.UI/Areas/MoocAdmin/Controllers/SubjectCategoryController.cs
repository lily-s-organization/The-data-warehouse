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

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class SubjectCategoryController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(UsersController));
        // GET: MoocAdmin/SubjectCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSubjectCategoryList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;      
            var list = db.SubjectCategorys.Where(x => x.Id > 0).OrderByDescending(p => p.CategoryName).Skip(currentItems).Take(pageSize).ToList();          
            int iCount = db.SubjectCategorys.Count(x => x.Id > 0);
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
        public JsonResult AddSubjectCategoryList(SubjectCategory subjectCategory)
        {
            try
            {
                if (subjectCategory == null)
                    return Json(300);

                if (subjectCategory.Id == 0)
                {
                    db.SubjectCategorys.Add(subjectCategory);
                }
                else
                {
                    db.Entry(subjectCategory).State = EntityState.Modified;
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
        public JsonResult GetSubjectCategoryDetail(long id)
        {
            return Json(db.SubjectCategorys.Find(id));
        }

       


        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                SubjectCategory subjectCategory = db.SubjectCategorys.Find(id);
                if (subjectCategory == null)
                    return Json(500);

                db.SubjectCategorys.Remove(subjectCategory);
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