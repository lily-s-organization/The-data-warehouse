using AutoMapper;
using Mooc.Web.UI.Models;

namespace Mooc.Web.UI.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
        }
    }
}