using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.Dbo;
using Microsoft.Extensions.Logging;

namespace FripShop.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IArticleRepo _articleRepo;
        private readonly IUserRepo _userRepo;

        public ArticleController(IArticleRepo articleRepo, IUserRepo userRepo)
        {
            _articleRepo = articleRepo;
            _userRepo = userRepo;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            var results = await this._articleRepo.Get();
            return Ok(results);
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult> GetId(long articleId)
        {
            var articles = await this._articleRepo.Get();
            if (articles.Any(c => c.Id == articleId))
                return Ok(articles.Single(c => c.Id == articleId));
            return NotFound();
        }

        [HttpGet("{articleId}/getUser")]
        public async Task<ActionResult> GetUserFromArticle(long articleId)
        {
            var user = await _articleRepo.GetUserFromId(articleId);
            if (user != null)
                return Ok(user);
            return NotFound();
        }
    }
}
