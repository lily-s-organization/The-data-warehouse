using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooc.Common.Utils;
using Mooc.DataAccess.Models.Context;

namespace Mooc.Web.UI.Filter
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
       // private DataContext db = new DataContext();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
   
            //得到前端发来的cookie值
            HttpCookie name = filterContext.HttpContext.Request.Cookies.Get(CommonVariables.LoginCookieName);
            HttpCookie id = filterContext.HttpContext.Request.Cookies.Get(CommonVariables.LoginCookieID);
            HttpCookie roletype = filterContext.HttpContext.Request.Cookies.Get(CommonVariables.LoginCookieType);

            if (roletype == null || name == null || id == null)
            {
                filterContext.Result = new RedirectResult("/MoocAdmin/Admin/Login");
            }
            else
            {
                var userName = name.Value;
                var userId = Convert.ToInt64(id.Value);
                var userRoleType = Convert.ToInt64(roletype.Value);

               // var result = db.Users.Where(x => x.UserName == userName && x.Id == userId && x.UserState == 0 && x.RoleType == userRoleType).SingleOrDefault();

                using (DataContext db = new DataContext())
                {
                    var result = db.Users.Where(x => x.UserName == userName && x.Id == userId && x.UserState == 0 && x.RoleType == userRoleType).SingleOrDefault();

                    if (result == null)
                    {
                        filterContext.Result = new RedirectResult("/MoocAdmin/Admin/Login");
                    }
                    else
                    {
                        return;
                    }

                }

               
                
               
            }
            

            
        }

    }
}