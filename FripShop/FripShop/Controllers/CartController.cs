using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

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
            double total = 0;
            foreach(var elem in cart)
            {
                var article = await _articleRepo.GetArticleFromId(elem.ArticleId);
                list.Add(article);
                total += article.Price;
            }
            ViewData["cartTotalBill"] = total;
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

        public async Task<IActionResult> DeleteCart(IEnumerable<DTOCart> cartList)
        {
            var list = new List<DTOArticle>();
            foreach (var item in cartList)
            {
                var res = await _cartRepo.Delete(item.Id);
                if (res == false)
                {
                    return View("Failed");
                }
            }

            ViewData["cartlist"] = null;
            ViewData["PageTitle"] = "cart details";
            return View("ShowCart");
        }


        public async Task<IActionResult> DeleteUserCart(long userId)
        {
            try
            {
                var cartList = await _cartRepo.GetCartByUserId(userId);
                foreach (var item in cartList)
                {
                    var test = DeleteCart(cartList);
                }
                return View();
            }
            catch(Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- DeleteUserCart() -- Error on Controller: ", ex);
                return BadRequest();
            }
        }

        public string build_string(IEnumerable<DTOArticle> articleList, DTOUser user)
        {
            string buy = "\n\n Les Articles acheté sont : \n";
            double total = 0;

            foreach(var item in articleList)
            {
                buy += "_____________________________\n\n";
                buy += item.Name + " | " + item.Price + "€\n\n";
                total += item.Price;
                
            }

            string res = "FACTURE FripShop \n\n"
                + "Courriel client: " + user.Email + "\n"
                + "Facturé à:       " + user.Name
                + buy
                + "_____________________\n"
                + "Total facturé: "
                + total.ToString() + "€";
         

            return res;
        }

        public async Task<IActionResult> ConfirmCart()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = _userRepo.GetUserByEmail(email);

                var cart = await GetCurrentUserCart();
                var list = new List<DTOArticle>();
                foreach (var elem in cart)
                {
                    var curr = await _articleRepo.GetArticleFromId(elem.ArticleId);
                    list.Add(curr);
                }


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("fripshop.dotnet@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Facture Fripshop";
                message.IsBodyHtml = false;  
                message.Body =build_string(list,user);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("fripshop.dotnet@gmail.com", "fripshopdotnet");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);


                var del = DeleteUserCart(user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Confirm() -- Error on Controller : ", ex);
                return BadRequest();
            }
            return View("ShowCart");
        }
    }
}
