﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.Common.Utils;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.Utils;
using Mooc.DataAccess.Models.ViewModels;

namespace Mooc.Web.UI.Controllers
{

    [AllowAnonymous]
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(UsersController));


        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Logout()
        {
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieName);
            return Json(200);
        }

        public ActionResult ToRegister()
        {
            IList<SelectListItem> RoleNamelistItem = EnumModels.ToSelectList(typeof(EnumModels.RoleNameEnum));
            ViewData["RoleType"] = new SelectList(RoleNamelistItem, "Value", "Text");
            return View();
        }

        [HttpPost]
        public JsonResult Register(User user)
        {
            try
            {
                if (user.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(user.ImageUpload.FileName);
                    string extension = Path.GetExtension(user.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    user.PhotoUrl = "~/App_Data/Images/" + fileName;
                    user.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/App_Data/Images/"), fileName));
                }
                db.Users.Add(user);
                db.SaveChanges();
                //用户注册后，默认为登录状态，用cookie存储用户状态
                CookieHelper.SetCookie(CommonVariables.LoginCookieName, user.UserName, CookieHelper.TimeUtil.H, "1");
                
                return Json(0);
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(400);
            }
        }

        public ActionResult ToSignIn()
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
            try
            {
                if (user == null)
                    return Json(300);
                else
                {
                    if (user.Id == 0)
                    {
                        user.UserState = 0;
                        user.AddTime = DateTime.Now;
                        db.Users.Add(user);
                    }
                    else
                    {
                        db.Entry(user).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    return Json(0);
                }
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return Json(400);
            }
               
            

        }

        
        public ActionResult Details(int id)
        {
            try
            {
                IList<SelectListItem> RoleNamelistItem = EnumModels.ToSelectList(typeof(EnumModels.RoleNameEnum));
                ViewData["RoleType"] = new SelectList(RoleNamelistItem, "Value", "Text");
                User user = new User();
                user = db.Users.Where(x => x.Id == id).FirstOrDefault<User>();

                return View(user);
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public JsonResult GetUserList(int pageIndex, int pageSize)
        {
            try
            {
                int currentItems = (pageIndex - 1) * pageSize;// the items from which pages-当前页从第几条开始

                var list = db.Users.Where(x => x.Id > 0).OrderByDescending(p => p.AddTime).Skip(currentItems).Take(pageSize).ToList();//paging in EF

                List<UserViewModel> viewList = AutoMapper.Mapper.Map<List<UserViewModel>>(list);

                return Json(new { code = 0, data = viewList });
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Json(new { code = 1});
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
                logger.Error(ex.Message);
                return Json(300);
            }
        }
           
        
    }
}
