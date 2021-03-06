using AutoMapper;
using FripShop.DTO;

namespace FripShop.DataAccess
{
    /// <summary>
    /// Class of mappings for AutoMapper
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoMapperProfiles()
        {
            CreateMap<DTOArticle, EFModels.Article>();
            CreateMap<EFModels.Article, DTOArticle>();

            CreateMap<DTOCart, EFModels.Cart>();
            CreateMap<EFModels.Cart, DTOCart>();

            CreateMap<DTOTransaction, EFModels.Transaction>();
            CreateMap<EFModels.Transaction, DTOTransaction>();

            CreateMap<DTOUser, EFModels.User>();
            CreateMap<EFModels.User, DTOUser>();

            CreateMap<DTOUser, DTOUserPublic>();
            CreateMap<DTOUserPublic, DTOUser>();
        }
    }
}
