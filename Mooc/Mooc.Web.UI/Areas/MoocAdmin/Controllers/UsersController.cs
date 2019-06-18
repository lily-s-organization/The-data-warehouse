﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Utils;
using Mooc.DataAccess.Models.ViewModels;
using Mooc.Web.UI.Filter;
using Mooc.Common.Utils;

namespace Mooc.Web.UI.Areas.MoocAdmin.Controllers
{
    [LoginAuthorize]
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(UsersController));
        // GET: MoocAdmin/Users
        public ActionResult Index()
        {
            var userName = LoginHelper.UserName;
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddUserList(User user)
        {
            try
            {
                if (user == null)
                    return Json(300);

                if (user.Id == 0)
                {
                    user.UserState = 0;
                    user.AddTime = DateTime.Now;
                    db.Users.Add(user);
                }
                else
                {
                    var tmp = db.Users.Find(user.Id);
                    user.UserState = tmp.UserState;
                    user.AddTime = tmp.AddTime;       //在前端获得的日期格式为"/Date(1560579721727)/"，无法在js里转换成c#的datetime格式                 
                    db.Entry(tmp).State = EntityState.Detached;
                    db.Entry(user).State = EntityState.Modified;
                }

                db.SaveChanges();
                return Json(0);
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(400);
            }
        }

        [HttpPost]
        public JsonResult GetUserList(int pageIndex, int pageSize)
        {
            int currentItems = (pageIndex - 1) * pageSize;// the items from which pages-当前页从第几条开始        
            var list = db.Users.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).ToList();//paging in EF
            List<UserViewModel> viewList = AutoMapper.Mapper.Map<List<UserViewModel>>(list);
            int iCount = db.Users.Count(x => x.Id > 0);
            return Json(new { code = 0, data = viewList, iCount = iCount });

        }

        public ActionResult Details(int id)
        {
            try
            {
                ViewBag.Id = id;
                return View();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return RedirectToAction("Index");
            }
        }

        //因为前端不用url+id的字符串拼接形式，所有改为Post请求
        [HttpPost]
        public JsonResult GetUserDetail(long id)
        {
            return Json(db.Users.Find(id));
        }

        [HttpGet]
        public JsonResult GetRoleTypeList()
        {

            return Json(new { data = EnumModels.ToSelectList(typeof(EnumModels.RoleNameEnum)) }, JsonRequestBehavior.AllowGet);

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
                logger.Error(ex.Message);
                return Json(300);
            }
        }



    }
}