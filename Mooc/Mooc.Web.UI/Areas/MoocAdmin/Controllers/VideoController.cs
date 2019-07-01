using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class VideoController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(SubjectController));
        // GET: MoocAdmin/Video
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddVideoList(Video video,int SectionId)
        {

            video.Section = db.Sections.Find(SectionId);


            try
            {
                if (video == null)
                    return Json(300);

                if (video.Id == 0)
                {
                    db.Videos.Add(video);
                }
                else
                {
                    //var tmp = db.Subjects.Find(subject.Id);
                    //tmp.Subjectgory = db.SubjectCategorys.Find(categoryId);
                    //tmp.Teacher = db.Teachers.Find(teacherId);
                    //subject.AddTime = DateTime.Now;
                    //db.Entry(tmp).CurrentValues.SetValues(subject);
                    //使用db.Entry(subject).State = EntityState.Modified; subject表中两个外键并没有被改变 只能使用CuurentValues.SetValues
                }
                db.SaveChanges();
                return Json(new { code = 200, currentId = video.Id });     //添加完成后 返回最新的自增长id
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(new { code = 500});
            }
           
        }

        [HttpPost]
        public JsonResult UploadVideo(HttpPostedFileBase video)
        {
            if (video == null)
                return Json(new { code = 300, msg = "请上传视频" });

            string fileName = Path.GetFileName(video.FileName);
            string fileExtension = Path.GetExtension(video.FileName).ToLower();

            string[] filetype = { ".mp4", ".avi", ".flv", ".3gp", ". rmvb" }; //文件允许格式    
            bool checkType = Array.IndexOf(filetype, fileExtension) == -1;
            if (checkType)
                return Json(new { code = 301, msg = "格式错误" });

            if (video.ContentLength >= 1024 * 1024 * 800)//1gb
                return Json(new { code = 302, msg = "超过800MB了" });

            string saveName = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), fileExtension);
            string savePath = Server.MapPath("~/Images/Video/");
            if (!System.IO.Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            var filePath = Path.Combine(savePath, saveName);
            video.SaveAs(filePath);



            return Json(new { code = 0, filename = saveName, originalFileName = fileName, url = Url.Content("~/api/GetContent/Video?id=" + saveName) });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Video video = db.Videos.Find(id);
                if (video == null)
                    return Json(500);

                //删除视频文件
                string savePath = Server.MapPath("~/Images/Video/");
                string deleteFile = savePath + video.FileId;
                if (System.IO.File.Exists(deleteFile))
                {
                    System.IO.File.Delete(deleteFile);
                }

                db.Videos.Remove(video);
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