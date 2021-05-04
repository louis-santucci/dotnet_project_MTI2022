using FripShop.DataAccess.EFModels;
using FripShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DataAccess.Interfaces
{
    public interface ICartRepo: IRepo<Cart, DTOCart>
    {
        public Task<IEnumerable<DTOCart>> GetCartByUserId(long id);

        public Task<DTOCart> GetCartItemByArticleId(long articleId);

        public Task<Boolean> UserCartAlreadyContains(long articleId, long userId);

    }
}
