using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Utils;
using Mooc.DataAccess.Models.ViewModels;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class SubjectController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(SubjectController));
        // GET: MoocAdmin/Subject
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSubjectList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;

            var list = db.Subjects.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).Join(db.Teachers,
                subject => subject.Teacher.Id,
                teacher => teacher.Id,
                (subject, teacher) => new
                {
                    subjectId = subject.Id,
                    subjectName = subject.SubjectName,
                    teacherId = teacher.Id,
                    teacherName = teacher.TeacherName,
                    subjectStatus = subject.Status,
                    linkId = subject.Subjectgory.Id
                })
                .Join(db.SubjectCategorys, subject => subject.linkId, category => category.Id, (subject, category) => new
                {

                    CategoryName = category.CategoryName,
                    Id = subject.subjectId,
                    SubjectName = subject.subjectName,
                    TeacherId = subject.teacherId,
                    TeacherName = subject.teacherName,
                    Status = subject.subjectStatus

                }

                ).ToList();
                
            List<SubjectViewModel> viewList = AutoMapper.Mapper.Map<List<SubjectViewModel>>(list);
            int iCount = db.Subjects.Count(x => x.Id > 0);
            return Json(new { code = 0, data = viewList, iCount = iCount });

        }

        [HttpGet]
        public JsonResult InitSelectList()
        {
            var teacherList = db.Teachers.Select(t => new { name = t.TeacherName,id = t.Id });
            var subjectCategory = db.SubjectCategorys.Select(t => new { name = t.CategoryName, id = t.Id });
            return Json(new { teacherList = teacherList, subjectCategory = subjectCategory }, JsonRequestBehavior.AllowGet);

           
        }

        [HttpPost]
        public JsonResult AddSubjectList(Subject subject,int teacherId,int categoryId)        //是否用viewModel比较好
        {
           
            subject.Teacher = db.Teachers.Find(teacherId);
            subject.Subjectgory = db.SubjectCategorys.Find(categoryId);
           
            try
            {
                if (subject == null)
                    return Json(300);

                if (subject.Id == 0)
                {
                    subject.Status = 0;
                    subject.AddTime = DateTime.Now;
                    db.Subjects.Add(subject);
                }
                else
                {
                    db.Entry(subject).State = EntityState.Modified;
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
        public JsonResult Delete(int id)
        {
            try
            {
                Subject subject = db.Subjects.Find(id);
                if (subject == null)
                    return Json(500);

                if (subject.PhotoUrl != null)      //如果用户有头像照片的话 删除照片
                {
                    string savePath = Server.MapPath("~/Images/Upload/");
                    string deleteFile = savePath + subject.PhotoUrl;
                    if (System.IO.File.Exists(deleteFile))
                    {
                        System.IO.File.Delete(deleteFile);
                    }
                }

                db.Subjects.Remove(subject);
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