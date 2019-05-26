using AutoMapper;
using Mooc.DataAccess.Models.Entities;
using Mooc.DataAccess.Models.ViewModels;

namespace Mooc.Web.UI.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>();
        }

       

    }

}