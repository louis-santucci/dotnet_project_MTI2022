using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.Extensions.Logging;

namespace FripShop.DataAccess
{
    public class ArticleRepository : Repository<Article, DTOArticle>, IArticleRepo
    {
        public ArticleRepository(FripShopContext context, ILogger<ArticleRepository> logger, IMapper mapper) : base(context, logger, mapper) {}
        public async Task<DTOUserInfo> GetUserFromId(long id)
        {
            try
            {
                return _mapper.Map<DTOUserInfo>(_context.Users.Find(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(DTOUserInfo)} -- Get() -- Error on db : ", ex);
                return null;
            }
        }
    }
}
