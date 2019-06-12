using System.Web.Mvc;

namespace Mooc.Web.UI.Areas.MoocAdmin
{
    public class MoocAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MoocAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MoocAdmin_default",
                "MoocAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}