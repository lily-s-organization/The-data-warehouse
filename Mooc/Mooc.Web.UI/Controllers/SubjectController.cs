using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;

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
    }
}