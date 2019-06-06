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
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

          

            //判断登录情况
            if (CookieHelper.GetCookie(CommonVariables.LoginCookieName) == "")
            {
                //cookie里没有值 用户没有登录  没有权限访问

                filterContext.Result = new RedirectResult("/Users/Login");
            }
        }

    }
}