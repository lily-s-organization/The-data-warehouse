using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooc.Web.UI.Controllers
{
    public class Demo1Controller : Controller
    {
        // 使用 razor 绑定数据实现 User 的增删改查  EF + Automap
        public ActionResult Index()
        {
            return View();
        }
    }
}