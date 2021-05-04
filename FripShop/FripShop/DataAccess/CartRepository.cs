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

        public async Task<DTOCart> GetCartByEmail(string email)
        {
            try
            {
                return _mapper.Map<DTOCart>(_context.Carts.Find(email));
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(DTOUserPublic)} -- Get() -- Error on db : ", ex);
                return null;
            }
        }

        public async Task<DTOCart> GetCartByUserName(string userName)
        {
            try
            {
                return _mapper.Map<DTOCart>(_context.Carts.Find(userName));
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(DTOUserPublic)} -- Get() -- Error on db : ", ex);
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
                _logger.LogError($"REPOSITORY {typeof(DTOUserPublic)} -- Get() -- Error on db : ", ex);
                return null;
            }
        }
    }
}
