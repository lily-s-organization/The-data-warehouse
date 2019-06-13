using System.Web;
using System.Web.Mvc;
using Mooc.Web.UI.Filter;

namespace Mooc.Web.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // filters.Add(new LoginAuthorizeAttribute());//相当于过滤器对所有的控制器检验-[LoginAuthorize]



        }
    }
}
 