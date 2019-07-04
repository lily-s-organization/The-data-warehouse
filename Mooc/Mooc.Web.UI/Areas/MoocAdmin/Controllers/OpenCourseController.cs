using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class OpenCourseController : Controller
    {
        // GET: MoocAdmin/OpenCourse
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(string startDate,string closeDate)
        {
            DateTime startDateTime = Convert.ToDateTime(startDate);
            DateTime closeDateTime = Convert.ToDateTime(closeDate);
            return Json(100);
        }
    }
}