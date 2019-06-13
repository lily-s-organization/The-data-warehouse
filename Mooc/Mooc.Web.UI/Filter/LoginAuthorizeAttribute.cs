using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mooc.Common.Utils;

namespace Mooc.Web.UI.Filter
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //跳过免验证的controller
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            //得到前端发来的cookie值
            HttpCookie cookie = filterContext.HttpContext.Request.Cookies.Get(CommonVariables.LoginCookieName);

            if (cookie == null )
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }
            else
            {
                //var cookieUserName = cookie.Value;
                //var localCookie = CookieHelper.GetCookie(CommonVariables.LoginCookieName);
                //if (localCookie != cookieUserName)
                //{
                //    filterContext.Result = new RedirectResult("/Users/Login");
                //}
                //else
                //{
                //    return;
                //}
                return;
            }
            

            
        }

    }
}