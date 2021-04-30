using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Authorization;
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

        public ActionResult Index()
        {
            return View();
        }



        public static DTOUserPublic DtoUserToDtoUserPublic(DTOUser userModel)
        {
            DTOUserPublic userPublic = new DTOUserPublic();
            userPublic.Id = userModel.Id;
            userPublic.Name = userModel.Name;
            userPublic.UserName = userModel.UserName;
            userPublic.Note = userModel.Note;
            userPublic.Gender = userModel.Gender;
            return userPublic;
        }

        /// API Calls
        [HttpGet("/api/articles/")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var results = await this._articleRepo.Get();
                foreach (var result in results)
                {
                    var user = await _articleRepo.GetUserFromId(result.Id);
                    result.User = DtoUserToDtoUserPublic(user);
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER ARTICLE -- GetAll() -- Error on db : ", ex);
                return BadRequest();
            }
        }
    
        [HttpGet("/api/articles/{articleId}")]
        public async Task<ActionResult> GetId(long articleId)
        {
            var articles = await this._articleRepo.Get();
            if (articles.Any(c => c.Id == articleId))
            {
                var article = articles.Single(c => c.Id == articleId);
                var user = await _articleRepo.GetUserFromId(article.Id);
                article.User = DtoUserToDtoUserPublic(user);
                return Ok(article);
            }

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

        [HttpPost("/api/articles/create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateArticle([FromBody] DTOArticle article)
        {
            throw new NotImplementedException();
            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER ARTICLE -- CreateArticle() -- Error on db : ", ex);
            }

        }
    }
}
