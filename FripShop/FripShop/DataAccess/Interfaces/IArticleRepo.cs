using System.Collections.Generic;
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
        /// <summary>
        /// Get DTO user in function of its ID
        /// </summary>
        /// <param name="id">the ID of the wanted DTO user</param>
        /// <returns>The wanted DTO user with all its information</returns>
        public DTOUser GetUserFromId(long id);
    }
}
