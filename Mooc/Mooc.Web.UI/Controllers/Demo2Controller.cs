using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Utils;
using Mooc.DataAccess.Models.ViewModels;
using System;
using System.Collections.Generic;
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

    }
}