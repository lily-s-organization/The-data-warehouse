using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class SectionController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(SectionController));
        // GET: MoocAdmin/Section
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int id)
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
        public JsonResult AddSectionList(Section section,int subjectId)
        {
            try
            {
                if (section == null)
                    return Json(300);

                if (section.Id == 0)
                {
                    section.Subject = db.Subjects.Find(subjectId);
                    db.Sections.Add(section);
                }
                else
                {
                 //   var tmp = db.Teachers.Find(teacher.Id);
                 //   teacher.State = tmp.State;
                 //   teacher.AddTime = tmp.AddTime;
                    //teacher.PhotoUrl = tmp.PhotoUrl;
                  //  db.Entry(tmp).State = EntityState.Detached;
                 //   db.Entry(teacher).State = EntityState.Modified;
                }

                db.SaveChanges();
                return Json(new { code = 200 , currentId = section.Id });
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(new { code = 0 });
            }
        }
    }
}