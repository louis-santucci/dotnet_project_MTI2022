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

        public async Task<IActionResult> Index()
        {
            var cart = await GetCurrentUserCart();
            var list = new List<DTOArticle>();
            foreach(var elem in cart)
            {
                var user = await _articleRepo.GetArticleFromId(elem.ArticleId);
                list.Add(user);
            }
            ViewData["cartlist"] = list;
            ViewData["PageTitle"] = "cart details";
            return View("ShowCart");
        }

        public async Task<IEnumerable<DTOCart>> GetCurrentUserCart()
        {
                var res = new List<DTOArticle>();
                var email = HttpContext.User.Identity.Name;
                var user = _userRepo.GetUserByEmail(email);
                var cart = await _cartRepo.Get();
                var results = cart.Where(a => a.BuyerId == user.Id);

                return results;
        }
    }
}
