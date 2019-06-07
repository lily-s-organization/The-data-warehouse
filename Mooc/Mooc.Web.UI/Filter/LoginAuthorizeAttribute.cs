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
            var cookieUserName = cookie.Value;

            //判断登录情况
            if ((cookieUserName == null) ||(CookieHelper.GetCookie(CommonVariables.LoginCookieName) != cookieUserName))
            {
                //cookie里没有值 或者值不对  没有权限访问

                filterContext.Result = new RedirectResult("/Users/Login");
            }
            else
            {
                return;
            }
        }

    }
}