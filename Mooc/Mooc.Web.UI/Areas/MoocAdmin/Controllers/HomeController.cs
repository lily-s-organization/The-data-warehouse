using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: MoocAdmin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}