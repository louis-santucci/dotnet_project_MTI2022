using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace FripShop.Controllers
{
    public class ArticleController : Controller
    {

        private readonly IArticleRepo _articleRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IArticleRepo articleRepo, IUserRepo userRepo, ILogger<ArticleController> logger)
        {
            _articleRepo = articleRepo;
            _userRepo = userRepo;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        /// API Calls
        [HttpGet("/api/articles/")]
        public async Task<ActionResult> GetAll()
        {
            var results = await this._articleRepo.Get();
            return Ok(results);
        }

        [HttpGet("/api/articles/{articleId}")]
        public async Task<ActionResult> GetId(long articleId)
        {
            var articles = await this._articleRepo.Get();
            if (articles.Any(c => c.Id == articleId))
                return Ok(articles.Single(c => c.Id == articleId));
            return NotFound();
        }

        [HttpGet("/api/articles/{articleId}/getUser")]
        public async Task<ActionResult> GetUserFromArticle(long articleId)
        {
            var user = await _articleRepo.GetUserFromId(articleId);
            if (user != null)
                return Ok(user);
            return NotFound();
        }
    }
}
