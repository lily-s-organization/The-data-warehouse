using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Utils;
using Mooc.DataAccess.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooc.Web.UI.Controllers
{
    
    public class Demo2Controller : Controller
    {
        // 使用 jquey / vue.js /angular.js 绑定数据 EF+automap+autofac (不要急)
        public ActionResult Index()
        {
            return View();
        }

        //[ActionName("adddeom")]
       //[Route("Demo/add")]

        public ActionResult Add()
        {
            IList<SelectListItem> RoleNamelistItem = EnumModels.ToSelectList(typeof(EnumModels.RoleNameEnum));
            ViewData["RoleType"] = new SelectList(RoleNamelistItem, "Value", "Text");
           // ViewBag.roletype = JobNamelistItem;

            return View();
        }

        [HttpPost]
        public JsonResult AddUserList(User user)
        {
            if (user == null)
                return Json(300);

            using (DataContext db = new DataContext())
            {
                user.UserState = 0;
                user.AddTime = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return Json(0);
            }

        }


        [HttpPost]
        public JsonResult GetUserList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;// the items from which pages-当前页从第几条开始
            using (DataContext db = new DataContext())
            {
                var list = db.Users.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).ToList();//paging in EF

                List<UserViewModel> viewList = AutoMapper.Mapper.Map<List<UserViewModel>>(list);

                return Json(new { code = 0, data = viewList });
            }
        }


        [HttpGet]
        public JsonResult GetHttpUserList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;// the items from which pages-当前页从第几条开始
            using (DataContext db = new DataContext())
            {
                var list = db.Users.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).ToList();//paging in EF

                List<UserViewModel> viewList = AutoMapper.Mapper.Map<List<UserViewModel>>(list);

                return Json(new { code = 0, data = viewList },JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Angular()
        {
            return View();
        }


        public ActionResult Video()
        {
            return View();
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

            return Json(new { code = 0, filename = saveName, url = Url.Content("~/api/GetContentApi/Video?id=" + saveName) });
        }



    }
}