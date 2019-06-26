using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class SectionController : Controller
    {
        // GET: MoocAdmin/Section
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}