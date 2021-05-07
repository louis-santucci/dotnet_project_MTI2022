using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Controller for the cart
    /// </summary>
    public class CartController : Controller
    {
        private readonly IArticleRepo _articleRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILogger<UserController> _logger;
        private readonly ICartRepo _cartRepo;
        private readonly ITransactionRepo _transactionRepo;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="articleRepo"></param>
        /// <param name="userRepo"></param>
        /// <param name="cartRepo"></param>
        /// <param name="transactionRepo"></param>
        public CartController(ILogger<UserController> logger, IArticleRepo articleRepo, IUserRepo userRepo, ICartRepo cartRepo, ITransactionRepo transactionRepo)
        {
            this._userRepo = userRepo;
            this._articleRepo = articleRepo;
            this._logger = logger;
            this._cartRepo = cartRepo;
            this._transactionRepo = transactionRepo;
        }

        public IUserRepo UserRepo => _userRepo;


        /// <summary>
        /// Get the Cart of the connected account
        /// Search each article by their id
        /// ViewData[] transfers infos to the .cshtml
        /// </summary>
        /// <returns> View du Cart </returns>
        /// 
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cart = await GetCurrentUserCart();
            var list = new List<DTOArticle>();
            double total = 0;
            foreach(var elem in cart)
            {
                var article = await _articleRepo.GetById(elem.ArticleId);
                list.Add(article);
                total += article.Price;
            }
            ViewData["cartTotalBill"] = total;
            ViewData["cartlist"] = list;
            ViewData["PageTitle"] = "cart details";
            return View("ShowCart");
        }

        /// <summary>
        /// Get the cart of the connected account
        /// </summary>
        /// <returns> Liste de DTOCart</returns>
        [Authorize]
        public async Task<IEnumerable<DTOCart>> GetCurrentUserCart()
        {
                var res = new List<DTOArticle>();
                var email = HttpContext.User.Identity.Name;
                var user = _userRepo.GetUserByEmail(email);
                var cart = await _cartRepo.Get();
                var results = cart.Where(a => a.BuyerId == user.Id);

                return results;
        }

        /// <summary>
        /// Delete one article of the connected account by the article id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>View du Cart</returns>
        [Authorize]
        public async Task<IActionResult> DeleteArticleFromCart(long articleId)
        {
            var cart = await GetCurrentUserCart();
            var list = new List<DTOArticle>();
            foreach (var elem in cart)
            {
                var curr = await _articleRepo.GetById(elem.ArticleId);
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


        /// <summary>
        /// Delete all the cart of cartList
        /// </summary>
        /// <param name="cartList"></param>
        /// <returns></returns>
        [Authorize]
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

        /// <summary>
        /// Delete user Cart by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> current View</returns>
        [Authorize]
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
                _logger.LogError("CONTROLLER CART -- DeleteUserCart() -- Error on Controller: ", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Build the mail string
        /// </summary>
        /// <param name="articleList"></param>
        /// <param name="user"></param>
        /// <returns>string</returns>
        [Authorize]
        public string build_string(IEnumerable<DTOArticle> articleList, DTOUser user)
        {
            string buy = "\n\n Les Articles acheté sont : \n";
            double total = 0;

            foreach(var item in articleList)
            {
                if (item != null)
                {
                    buy += "_____________________________\n\n";
                    buy += item.Name + " | " + item.Price + "€\n\n";
                    total += item.Price;
                }
                
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

        /// <summary>
        /// Delete Cart content
        /// Send a mail with the cart content 
        /// Acts like when you buy something
        /// </summary>
        /// <returns></returns>
        [Authorize]
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
                    var curr= await _articleRepo.GetById(elem.ArticleId);
                    list.Add(curr);
                    if (curr != null)
                    {
                        curr.State = "sold";
                        var test =  await _articleRepo.Update(curr);
                        DTOTransaction CurrTrans = new DTOTransaction();
                        CurrTrans.ArticleId = curr.Id;
                        CurrTrans.BuyerId = user.Id;
                        CurrTrans.TransactionState = "sold";
                        CurrTrans.LastUpdateAt = DateTime.Today;
                        var adding = await _transactionRepo.Insert(CurrTrans);
                    }
                    
                }

                if (cart.Count() != 0)
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("fripshop.dotnet@gmail.com");
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Facture Fripshop";
                    message.IsBodyHtml = false;
                    message.Body = build_string(list, user);
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("fripshop.dotnet@gmail.com", "fripshopdotnet");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    var del = DeleteUserCart(user.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER CART -- Confirm() -- Error on Controller : ", ex);
                return BadRequest();
            }
            return View("ShowCart");
        }
    }
}
