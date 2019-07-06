using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.ViewModels;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class OpenCourseController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(SubjectController));
        // GET: MoocAdmin/OpenCourse
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(string startDate,string closeDate,int CourseId)
        {
            DateTime startDateTime = Convert.ToDateTime(startDate);
            DateTime closeDateTime = Convert.ToDateTime(closeDate);
            DateTime currentTime = DateTime.Now.Date;

            var openCourseList = db.OpenCourses.Where(x => x.CourseId == CourseId).ToList();
            foreach (var item in openCourseList)
            {
                if ((startDateTime >= item.StartDate && startDateTime <= item.CloseDate) || (closeDateTime >= item.StartDate && closeDateTime <= item.CloseDate))
                {
                    return Json(400);          //该时间段已经存在开课
                }
            }

            if (DateTime.Compare(startDateTime, currentTime) <= 0)
            {
                return Json(300);    //开课日期必须比当前时间晚一天
            }

            if (DateTime.Compare(startDateTime, closeDateTime) >= 0)  //开课日期不能大于结课日期
            {
                return Json(0);
            }

            try
            {
                OpenCourse openCourse = new OpenCourse
                {
                    StartDate = startDateTime,
                    CloseDate = closeDateTime,
                    CourseId = CourseId,
                };

                db.OpenCourses.Add(openCourse);
                db.SaveChanges();

                return Json(100);
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(200);
            }
        }

        [HttpPost]
        public JsonResult GetOpenCourseList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;

            var courseList = db.OpenCourses.Where(x => x.Id > 0).OrderByDescending(x => x.Id).Skip(currentItems).Take(pageSize).Join(db.Subjects,
                 openCourse => openCourse.CourseId,
                 subject => subject.Id,
                 (openCourse, subject) => new
                 {
                     startDate = openCourse.StartDate,
                     closeDate = openCourse.CloseDate,
                     Id = openCourse.Id,
                     subjectName = subject.SubjectName
                 }).ToList();

          
            //List<OpenCourseViewModel> openCourseList = AutoMapper.Mapper.Map<List<OpenCourseViewModel>>(courseList);

            var iCount = courseList.Count();
            return Json(new { data = courseList, iCount = iCount });
 
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                OpenCourse openCourse = db.OpenCourses.Find(id);
                if (openCourse == null)
                    return Json(500);

                var currentTime = DateTime.Now.Date;
                if (currentTime>=openCourse.StartDate && currentTime<= openCourse.CloseDate)
                {
                    return Json(new { code = 200});           //开课期间不能删除
                }

                db.OpenCourses.Remove(openCourse);
                db.SaveChanges();
                return Json(new { code = 0 });
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Json(300);
            }
        }
    }
}