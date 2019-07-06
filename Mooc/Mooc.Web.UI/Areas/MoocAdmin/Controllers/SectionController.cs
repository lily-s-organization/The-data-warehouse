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
    public class SectionController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(SectionController));
        // GET: MoocAdmin/Section
        public ActionResult Index(int id)
        {
            try
            {
                ViewBag.subjectId = id;
                return View();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return RedirectToAction("Index","Subject");
             
              
            }
        }

        public ActionResult Add(int id)
        {
            try
            {
                ViewBag.subjectId = id;
                return View();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.sectionId = id;
                return View();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult GetSectionList(int pageIndex, int pageSize,int Id)
        {
            int currentItems = (pageIndex - 1) * pageSize;
            var list = db.Sections.Where(x => x.Id > 0).OrderByDescending(p => p.Id).Skip(currentItems).Take(pageSize).Join(db.Subjects,
                section=>section.SubjectId,
                subject=>subject.Id,
                (section, subject) => new
                {
                    Id = section.Id,
                    SectionName = section.SectionName,
                    Description = section.Description,
                    SubjectId = subject.Id
                }
                ).Where(x=>x.SubjectId == Id).ToList();


            int iCount = list.Count();
            return Json(new { code = 0, data = list, iCount = iCount });

        }

        [HttpPost]
        public JsonResult AddSectionList(Section section,int subjectId)
        {
            try
            {
                if (section == null)
                    return Json(300);

                section.SubjectId = subjectId;//daidong

                if (section.Id == 0)
                {
                    //  section.Subject = db.Subjects.Find(subjectId);
                   
                    db.Sections.Add(section);
                }
                else                         //未引用到对象的实例
                {
                    db.Entry(section).State = EntityState.Modified;
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

        [HttpPost]
        public JsonResult GetSectionAndVideoDetail(int id)
        {
            var sectionDetail = db.Sections.Where(x => x.Id == id).SingleOrDefault();
            var videoList = db.Videos.Join(db.Sections,
                video => video.SectionId,
                section => section.Id,
                (video, section) => new
                {
                    videoId =video.Id,
                    originalFileName = video.OriginalFileName,
                    saveFileName = video.FileId,
                    sectionId = section.Id,
                    videoTitle = video.VideoTitle,
                    videoDescription = video.Description
                }
                ).Where(x=>x.sectionId == id).ToList();
            var subjectId = db.Sections.Join(db.Subjects,
                section=>section.SubjectId,
                subject=>subject.Id,
                (section,subject)=>new
                {
                    subjectId = subject.Id,
                    section = section.Id
                }
                ).Where(x=>x.section == id).SingleOrDefault();

            return Json(new { code = 200, sectionDetail = sectionDetail, videoList = videoList , subjectId  = subjectId });
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Section section = db.Sections.Find(id);
                if (section == null)
                    return Json(500);

                

                db.Sections.Remove(section);
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