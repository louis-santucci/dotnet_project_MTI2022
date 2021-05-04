using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IArticleRepo _articleRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILogger<UserController> _logger;
        private readonly ICartRepo _cartRepo;

        public CartController(ILogger<UserController> logger, IArticleRepo articleRepo, IUserRepo userRepo, ICartRepo cartRepo)
        {
            this._userRepo = userRepo;
            this._articleRepo = articleRepo;
            this._logger = logger;
            this._cartRepo = cartRepo;
        }

        public IUserRepo UserRepo => _userRepo;

        /// Recupere le cart de la personne connecte
        /// recherche chaque article grace a leurs id
        /// retourne la liste des articles
        /// les view data permettent de transferer les infos a la view
        public async Task<IActionResult> Index()
        {
            var cart = await GetCurrentUserCart();
            var list = new List<DTOArticle>();
            foreach(var elem in cart)
            {
                var article = await _articleRepo.GetArticleFromId(elem.ArticleId);
                list.Add(article);
            }
            ViewData["cartlist"] = list;
            ViewData["PageTitle"] = "cart details";
            return View("ShowCart");
        }

        //Recupere le Cart associe a l'Id de la personne connecte
        public async Task<IEnumerable<DTOCart>> GetCurrentUserCart()
        {
                var res = new List<DTOArticle>();
                var email = HttpContext.User.Identity.Name;
                var user = _userRepo.GetUserByEmail(email);
                var cart = await _cartRepo.Get();
                var results = cart.Where(a => a.BuyerId == user.Id);

                return results;
        }

        public async Task<IActionResult> DeleteArticleFromCart(long articleId)
        {
            var cart = await GetCurrentUserCart();
            var list = new List<DTOArticle>();
            foreach (var elem in cart)
            {
                var curr = await _articleRepo.GetArticleFromId(elem.ArticleId);
                if (curr.Id != articleId)
                {
                    list.Add(curr);
                }
            }
            var toDelete = await _cartRepo.GetCartItemByArticleId(articleId);
            var res = await _cartRepo.Delete(toDelete.Id);
            if (res == false)
            {
                return View("Failed");
            }
           
            ViewData["cartlist"] = list;
            ViewData["PageTitle"] = "cart details";
            return View("ShowCart");
        }
    }
}
