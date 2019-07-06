using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public JsonResult Sell(int id)  //课程上架
        {
            try
            {
                var subject = db.Subjects.Find(id);
                if (subject == null)
                {
                    return Json(500);
                }

                //遍历该课程下的video

                var sectionList = db.Subjects.Where(x => x.Id == id).Join(db.Sections,
                    selectedSubject => selectedSubject.Id,
                    section => section.SubjectId,
                    (selectedSubject, section) => new
                    {
                        sectionId = section.Id,
                        sectionName = section.SectionName
                    }
                    ).ToList();

                if (sectionList.Count()==0)
                {
                    return Json(700);  //告诉前端该课程下没有章节
                }

                foreach (var section in sectionList)
                {
                    var videoList = db.Sections.Where(x => x.Id == section.sectionId).Join(db.Videos,
                        selectedSection => selectedSection.Id,
                        video => video.SectionId,
                        (selectedSection, video) => new
                        {
                            sectionId = selectedSection.Id,
                            videId = video.Id,
                            sectionName = selectedSection.SectionName
                        }

                        );

                    if (videoList.Count() == 0)
                    {
                        return Json(new { code = 600, sectionName = section.sectionName });  //告诉前端第几章没有视频
                    }
                }

                subject.Status = 1;
                db.SaveChanges();
                return Json(200);
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(100);
            }
        }

        [HttpPost]
        public JsonResult NotSell(int id)    //课程下架
        {
            try
            {
                var subject = db.Subjects.Find(id);
                if (subject == null)
                {
                    return Json(500);
                }

                var keyDateList = db.OpenCourses.Where(x => x.CourseId == id).Select(x=>new { StartDate= x.StartDate, CloseDate = x.CloseDate}).ToList();

                var currentTime =  DateTime.Now;
                foreach (var keyDate in keyDateList)
                {
                    var res1 = DateTime.Compare(currentTime, keyDate.StartDate);
                    var res2 = DateTime.Compare(currentTime, keyDate.CloseDate);

                    if (res1*res2 < 0)          //如果res1,res2为一正一负 则说明当前时间落在开课结课的区间内 ,正在开课 则不允许下架
                    {
                        return Json(600);            
                    }
                }


                subject.Status = 2;
                db.SaveChanges();
                return Json(200);
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(100);
            }
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
        public JsonResult GetSubjectDetail(long id)
        {
            var result = db.Subjects.Where(x=>x.Id == id).Join(db.Teachers,
                subject => subject.SubjectCategoryId,
                teacher => teacher.Id,
                (subject, teacher) => new
                {
                    subjectId = subject.Id,
                    subjectName = subject.SubjectName,
                    teacherId = teacher.Id,
                    teacherName = teacher.TeacherName,
                    subjectStatus = subject.Status,
                    subjectDescription = subject.Description,
                    subjectPhotoUrl = subject.PhotoUrl,
                    linkId = subject.SubjectCategoryId
                })
                .Join(db.SubjectCategorys, subject => subject.linkId, category => category.Id, (subject, category) => new
                {

                    CategoryId =category.Id,
                    CategoryName = category.CategoryName,                  
                    Id = subject.subjectId,
                    SubjectName = subject.subjectName,
                    TeacherId = subject.teacherId,
                    TeacherName = subject.teacherName,
                    Status = subject.subjectStatus,
                    PhotoUrl = subject.subjectPhotoUrl,
                    Description = subject.subjectDescription

                }

                ).SingleOrDefault();

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetSubjectList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;

            var list = db.Subjects.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).Join(db.Teachers,
                subject => subject.TeacherId,
                teacher => teacher.Id,
                (subject, teacher) => new
                {
                    subjectId = subject.Id,
                    subjectName = subject.SubjectName,
                    teacherId = teacher.Id,
                    teacherName = teacher.TeacherName,
                    subjectStatus = subject.Status,
                    subjectPhotoUrl = subject.PhotoUrl,
                    linkId = subject.SubjectCategoryId
                })
                .Join(db.SubjectCategorys, subject => subject.linkId, category => category.Id, (subject, category) => new
                {

                    CategoryName = category.CategoryName,
                    Id = subject.subjectId,
                    SubjectName = subject.subjectName,
                    TeacherId = subject.teacherId,
                    TeacherName = subject.teacherName,
                    Status = subject.subjectStatus,
                    PhotoUrl = subject.subjectPhotoUrl

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
        public JsonResult AddSubjectList(Subject subject,int teacherId,int categoryId)        
        {
           
           // subject.Teacher = db.Teachers.Find(teacherId);
          //  subject.Subjectgory = db.SubjectCategorys.Find(categoryId);
            

            try
            {
                if (subject == null)
                    return Json(300);

                subject.TeacherId = teacherId;
                subject.SubjectCategoryId = categoryId;

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
                var a = db.SaveChanges();
                return Json(new { code = 200,currentId = subject.Id });     //添加完成后 返回最新的自增长id
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(400);
            }
        }

        [HttpPost]
        public JsonResult AddPhoto(HttpPostedFileBase ImageUpload, string PhotoUrl)
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

            if (PhotoUrl != "NewPhoto")      //如果旧头像存在 则先删除旧的头像
            {
                string deleteFile = savePath + PhotoUrl;
                if (System.IO.File.Exists(deleteFile))
                {
                    System.IO.File.Delete(deleteFile);
                }
            }


            ImageUpload.SaveAs(saveFile);
            return Json(new { url = fileName });
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

        [HttpPost]
        public JsonResult GetSubjectId(int id)       //传入sectionId 获得对应的subjectId
        {
            var tmpSubject = db.Sections.Where(x => x.Id > 0).Join(db.Subjects,
                section=>section.SubjectId,
                subject=>subject.Id,
                (section,subject) => new
                {
                   subjectId = subject.Id
                }

                );


            return Json(new { subjectId = tmpSubject });
        }
    }
}