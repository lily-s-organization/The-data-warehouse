using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = from t in actionContext.Request.Headers where t.Key == "jwt" select t.Value.FirstOrDefault();
            if (authHeader != null)
            {
                string token = authHeader.FirstOrDefault();
                if (!string.IsNullOrEmpty(token))
                {
                    if (token == "admin")
                        return true;
                }
                return false;
            }
            return false;
        }


    }
}