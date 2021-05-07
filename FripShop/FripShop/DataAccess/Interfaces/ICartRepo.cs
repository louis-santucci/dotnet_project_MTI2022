using FripShop.DataAccess.EFModels;
using FripShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for Cart repository
    /// </summary>
    public interface ICartRepo: IRepo<Cart, DTOCart>
    {
        /// <summary>
        /// Get all DTO articles in cart in function of the ID of the DTO user
        /// </summary>
        /// <param name="id">The ID of the wanted user</param>
        /// <returns>The list of wanted DTO articles</returns>
        public Task<IEnumerable<DTOCart>> GetCartByUserId(long id);

        /// <summary>
        /// Get corresponding DTO cart in function of an item ID
        /// </summary>
        /// <param name="articleId">The ID of the wanted DTO article</param>
        /// <returns>The corresponding DTO cart</returns>
        public Task<DTOCart> GetCartItemByArticleId(long articleId);

        /// <summary>
        /// Checks wether a DTO article is already present or not in the cart of a DTO user
        /// </summary>
        /// <param name="articleId">The ID of the wanted DTO article</param>
        /// <param name="userId">The ID of the wanted DTO user</param>
        /// <returns>Boolean to know if the DTO article is already present or not</returns>
        public Task<Boolean> UserCartAlreadyContains(long articleId, long userId);

    }
}
