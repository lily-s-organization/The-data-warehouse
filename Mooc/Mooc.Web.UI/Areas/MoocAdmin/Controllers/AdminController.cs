using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    public class AdminController : Controller
    {
        // GET: MoocAdmin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}