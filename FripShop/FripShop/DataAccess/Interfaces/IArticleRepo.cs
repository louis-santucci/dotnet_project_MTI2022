using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.DTO;

namespace FripShop.DataAccess.Interfaces
{
    public interface IArticleRepo : IRepo<Article, DTOArticle>
    {
        public Task<DTOUserInfo> GetUserFromId(long id);
    }
}
