using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Utils;
using Mooc.DataAccess.Models.ViewModels;

namespace Mooc.Web.UI.Controllers
{
    public class AngularJSDemoController : Controller
    {
        private DataContext db = new DataContext();
        // GET: AngularJSDemo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;// the items from which pages-当前页从第几条开始        
            var list = db.Users.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).ToList();//paging in EF
            List<UserViewModel> viewList = AutoMapper.Mapper.Map<List<UserViewModel>>(list);
            return Json(new { code = 0, data = viewList });
            
        }


        public ActionResult Details(int id)
        {
            try
            {
                //IList<SelectListItem> RoleNamelistItem = EnumModels.ToSelectList(typeof(EnumModels.RoleNameEnum));
                // ViewData["RoleType"] = new SelectList(RoleNamelistItem, "Value", "Text");
               // var list = RoleNamelistItem.ToList();
                User user = new User();
                user = db.Users.Where(x => x.Id == id).FirstOrDefault<User>();

                return View(user);
            }
            catch (Exception ex)
            {

              //  logger.Error(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                if (user == null)
                    return Json(500);

                db.Users.Remove(user);
                db.SaveChanges();
                return Json(0);
            }
            catch (Exception ex)
            {
                
                return Json(300);
            }
        }


        [HttpPost]
        public JsonResult UpdateUser(User user)
        {

            try
            {
               
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                return Json(0);
                
            }
            catch (Exception ex)
            {
               

            }

            return Json(200);

        }

        [HttpGet]
        public JsonResult GetRoleTypeList()
        {
           // List<string> list1 = new List<string> {"a","b","c" };
            return Json(new{ data = EnumModels.ToSelectList(typeof(EnumModels.RoleNameEnum)) }, JsonRequestBehavior.AllowGet);
            //return Json(new { data = list1 }, JsonRequestBehavior.AllowGet);
        }
    }
}