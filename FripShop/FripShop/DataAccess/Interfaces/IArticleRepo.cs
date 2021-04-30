using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.Dbo;

namespace FripShop.DataAccess.Interfaces
{
    public interface IArticleRepo : IRepo<Article, DboArticle>
    {
        public Task<DboUser> GetUserFromId(long id);
    }
}
