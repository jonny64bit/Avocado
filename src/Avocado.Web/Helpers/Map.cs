using AutoMapper;
using Avocado.Database.Models;
using Avocado.Web.Models.Accounts;

namespace Avocado.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountGridItem>();
            CreateMap<Account, AccountEditItem>().ReverseMap();
        }
    }
}