using AutoMapper;
using FripShop.Dbo;

namespace FripShop.DataAccess
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DboArticle, EFModels.Article>();
            CreateMap<EFModels.Article, DboArticle>();

            CreateMap<DboCart, EFModels.Cart>();
            CreateMap<EFModels.Cart, DboCart>();

            CreateMap<DboRating, EFModels.Rating>();
            CreateMap<EFModels.Rating, DboRating>();

            CreateMap<DboTransaction, EFModels.Transaction>();
            CreateMap<EFModels.Transaction, DboTransaction>();

            CreateMap<DboUser, EFModels.User>();
            CreateMap<EFModels.User, DboUser>();
        }
    }
}
