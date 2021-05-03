using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.DTO;

namespace FripShop.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for Article repository
    /// </summary>
    public interface IArticleRepo : IRepo<Article, DTOArticle>
    {
        public Task<DTOUser> GetUserFromId(long id);
    }
}
