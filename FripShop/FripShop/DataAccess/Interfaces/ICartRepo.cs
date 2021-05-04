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
        public Task<DTOCart> GetCartByEmail(string email);
        public Task<DTOCart> GetCartByUserName(string userName);

        public Task<DTOCart> GetCartItemByArticleId(long articleId);
    }
}
