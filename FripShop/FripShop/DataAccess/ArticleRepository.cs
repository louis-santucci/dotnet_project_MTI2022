﻿using System;
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
    /// <summary>
    /// Class for article repository
    /// </summary>
    public class ArticleRepository : Repository<Article, DTOArticle>, IArticleRepo
    {
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public ArticleRepository(FripShopContext context, ILogger<ArticleRepository> logger, IMapper mapper) : base(context, logger, mapper) {}

        /// <summary>
        /// Get user from user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Concerned id</returns>
        public DTOUser GetUserFromId(long id)
        {
            try
            {
                return _mapper.Map<DTOUser>(_context.Users.Find(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY Article -- GetUserFromId() -- Error : ", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets an Article by the ArticleId
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>Concerned Article </returns>
        public async Task<DTOArticle> GetArticleById(long articleId)
        {
            try
            {
                var test =  _context.Articles.Where(a => a.Id == articleId).FirstOrDefault();
                return _mapper.Map<DTOArticle>(test);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY Cart -- GetCartItemByArticleId() -- Error : ", ex.Message);
                return null;
            }
        }

    }
}
