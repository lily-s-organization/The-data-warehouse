using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;

namespace Mooc.Web.UI.Controllers
{
    public class SubjectController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(SubjectController));
        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id)
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
        public JsonResult GetListByCategoryId(int id)      //通过课程类别的ID,获得这一类别下的课程 只取前五个课程
        {
            var result = db.Subjects.Where(x => x.SubjectCategoryId == id).OrderByDescending(x => x.Id).Take(5).ToList();

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetList()      //只取课程表里前十个课程 用于首页展示
        {
            var result = db.Subjects.Where(x => x.Id > 10).OrderByDescending(x => x.Id).Take(10);

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetDetail(int id)      //根据课程Id 返回课程下的章节 以及章节下的视频
        {
           
            var sectionList = db.Sections.Where(x => x.SubjectId == id).ToList();
            ArrayList resultList = new ArrayList();
            var videoCount = 0;
            foreach (var item in sectionList)
            {
                var videoList = db.Videos.Where(x => x.SectionId == item.Id).ToList();
                var tmp = new { sectionName = item.SectionName, list = videoList};
                resultList.Add(tmp);                                                       //组装数据 得到所有章节下的对应视频

                videoCount += videoList.Count();                                            //计算该老师所相关的视频数 用于前台展示
            }

            var teacherId = db.Subjects.Find(id).TeacherId;
          
            var teacherResult = db.Teachers.Find(teacherId);
            var courseCount = db.Subjects.Where(x => x.TeacherId == teacherId).ToList().Count();             //得到该老师所教的课的总数 用于前台展示
            
            var teacherInfo = new { Name = teacherResult.TeacherName, Title = teacherResult.Level, department = teacherResult.Department,photoUrl = teacherResult.PhotoUrl ,courseCount = courseCount, videoCount = videoCount };
            var subjectInfo = db.Subjects.Find(id);
           
            return Json(new { resultList  = resultList , teacherInfo = teacherInfo, subjectInfo = subjectInfo });
        }

        //Refactory
        //拆分成四个函数 
        #region 
        [HttpPost]
        public JsonResult GetTeacherDetail(int id)         //subjectId
        {
            int teacherId = db.Subjects.Find(id).TeacherId;
            Teacher teacherResult = db.Teachers.Find(teacherId);

            return Json(teacherResult);

        }
        [HttpPost]
        public JsonResult GetSubjectDetail(int id)             //subjectId
        {
            Subject subjectResult = db.Subjects.Find(id);
            int teacherId = db.Subjects.Find(id).TeacherId;
            int courseCount = db.Subjects.Where(x => x.TeacherId == teacherId).ToList().Count();             //得到该老师所教的课的总数 用于前台展示
            return Json(new { subjectResult= subjectResult, courseCount= courseCount });
        }
        [HttpPost]
        public JsonResult GetSectionList(int id)              //subjectId
        {
          
            List<Section> sectionList = db.Sections.Where(x => x.SubjectId == id).ToList();
            var videoCount = 0;
            foreach (var item in sectionList)
            {
                List<Video> videoList = db.Videos.Where(x => x.SectionId == item.Id).ToList();                                                                
                videoCount += videoList.Count();          //得到每个章节下的视频数 累加  用于前台展示
            }
            return Json(new { sectionList = sectionList , videoCount = videoCount });
        }
        [HttpPost]
        public JsonResult GetVideoList(int id)                 //sectionId
        {
            IList videoList = db.Videos.Where(x => x.SectionId == id).ToList();
            return Json(videoList);
        }
        #endregion

    }
}