using AutoMapper;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DataAccess
{
    public class CartRepository : Repository<Cart, DTOCart>, ICartRepo
    {
        public CartRepository(FripShopContext context, ILogger<CartRepository> logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<DTOCart>> GetCartByUserId(long id)
        {
            try
            {
                var UserCart = _context.Carts.Where(cartItem => cartItem.BuyerId == id).ToList();
                foreach(var cart in UserCart)
                {
                    cart.Article = null;
                    cart.Buyer = null;
                }
                return _mapper.Map<IEnumerable<DTOCart>>(UserCart);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY Cart -- GetCartByUserId() -- Error : ", ex.Message);
                return null;
            }
        }



        public async Task<DTOCart> GetCartItemByArticleId(long articleId)
        {
            try
            {
                var test = _context.Carts.Where(a => a.ArticleId == articleId).FirstOrDefault();
                test.Buyer = null;
                test.Article = null;
               return _mapper.Map<DTOCart>(test);
            }
            catch (Exception ex)
            {   
                _logger.LogError($"REPOSITORY Cart -- GetCartItemByArticleId() -- Error : ", ex.Message);
                return null;
            }
        }

        public async Task<bool> UserCartAlreadyContains(long articleId, long userId)
        {
            try
            {
                var test = _context.Carts.Where(a => (a.ArticleId == articleId && a.BuyerId == userId)).FirstOrDefault();
                if (test == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"REPOSITORY Cart -- UseCartAlreadyContains() -- Error : ", ex);
                return true;
            };
        }
    }
}
