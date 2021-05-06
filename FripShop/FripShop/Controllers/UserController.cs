using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FripShop.Controllers
{
    /// <summary>
    /// Controller for users
    /// </summary>
    public class UserController : Controller
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
        public UserController(ILogger<UserController> logger, IArticleRepo articleRepo, IUserRepo userRepo, ICartRepo cartRepo, ITransactionRepo transactionRepo)
        {
            this._userRepo = userRepo;
            this._articleRepo = articleRepo;
            this._logger = logger;
            this._cartRepo = cartRepo;
            this._transactionRepo = transactionRepo;
        }

        /// <summary>
        /// Get user details
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// Get create user view
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Get register page view
        /// </summary>
        /// <returns></returns>
        public IActionResult RegisterPage()
        {
            return View("Register");
        }

        /// <summary>
        /// Get login view
        /// </summary>
        /// <returns></returns>
        public IActionResult LoginPage()
        {
            return View("Login");
        }

        /// <summary>
        /// Get edit profile view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult EditProfile()
        {
            return View("EditProfile");
        }

        /// <summary>
        /// Get second auth view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult SecondAuth()
        {
            return View("SecondAuth");
        }

        /// <summary>
        /// Get profile view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.User.Identity.Name;
            var user = _userRepo.GetUserByEmail(email);
            DTOLoginUser userToReturn = new DTOLoginUser();
            userToReturn.Name = user.Name;
            userToReturn.Email = user.Email;
            userToReturn.UserName = user.UserName;
            userToReturn.Address = user.Address;
            userToReturn.Gender = user.Gender;
            userToReturn.Note = user.Note;
            userToReturn.NbNotes = user.NbNotes;

            var transactionList = new List<DTOArticle>();

            var userTransactions = await _transactionRepo.GetTransactionByUserId(user.Id);
            foreach (var transaction in userTransactions)
            {
                var toInsert = new DTOArticle();
                var article = await _articleRepo.GetById(transaction.ArticleId);
                toInsert.Name = article.Name;
                toInsert.State = article.State;
                toInsert.Id = article.Id;

                if (toInsert.Transaction == null)
                {
                    toInsert.Transaction = new DTOTransaction();
                    toInsert.Transaction.TransactionState = transaction.TransactionState;
                }


                var sellerFromDb = await _userRepo.GetById(article.SellerId);
                var sellerToInsert = new DTOUserPublic();
                sellerToInsert.Name = sellerFromDb.Name;
                sellerToInsert.UserName = sellerFromDb.UserName;
                toInsert.User = sellerToInsert;
                transactionList.Add(toInsert);
            }


            ViewData["transactionlist"] = transactionList;
            return View(userToReturn);
        }

        /// <summary>
        /// Add an article to a user cart
        /// </summary>
        /// <param name="articleID">Article id</param>
        /// <param name="email">User to add article to cart</param>
        /// <returns>Article view or error</returns>
        [Authorize]
        public async Task<IActionResult> AddArticle(int articleID, string email = null)
        {
            if (email == null)
                email = HttpContext.User.Identity.Name;
            try
            {
                DTOCart cart = new DTOCart();
                var user = _userRepo.GetUserByEmail(email);
                var test = await _cartRepo.UserCartAlreadyContains(articleID, user.Id);
                if (test == false)
                {
                    cart.ArticleId = articleID;
                    cart.BuyerId = user.Id;
                    cart.Quantity = 1;
                    cart.Article = null;
                    cart.Buyer = null;
                    var result = await _cartRepo.Insert(cart);
                    if (result != null)
                    {
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- AddArticle() -- Error : ", ex);
                return BadRequest();
            }

            return RedirectToAction("Index", "Article");
        }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static DTOUser DtoUserEditionToDtoUser(DTOUserEdition userModel)
        {
            var user = new DTOUser();

            user.Id = userModel.Id;
            user.Email = userModel.Email;
            user.Address = userModel.Address;
            user.Name = userModel.Name;
            user.Password = userModel.Password;
            user.UserName = userModel.UserName;
            user.Gender = userModel.Gender;
            user.Note = userModel.Note;
            user.NbNotes = userModel.NbNotes;

            return user;
        }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static DTOUserPublic DtoUserToDtoUserPublic(DTOUser userModel)
        {
            DTOUserPublic userPublic = new DTOUserPublic();
            userPublic.Id = userModel.Id;
            userPublic.Name = userModel.Name;
            userPublic.UserName = userModel.UserName;
            userPublic.Note = userModel.Note;
            userPublic.Gender = userModel.Gender;
            userPublic.NbNotes = userModel.NbNotes;
            return userPublic;
        }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register(DTOUserEdition userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_userRepo.GetUserByEmail(userModel.Email) != null)
                        return BadRequest(userModel);
                    if (_userRepo.GetUserByUserName(userModel.UserName) != null)
                        return BadRequest(userModel);
                    userModel.Password = HashPassword(userModel.Password);
                    var newUser = DtoUserEditionToDtoUser(userModel);
                    newUser.NbNotes = 0;
                    var result = await _userRepo.Insert(newUser);
                    if (result != null)
                        return View("Success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Register() -- Error : ", ex);
                return BadRequest();
            }
            return View();
        }

        /// <summary>
        /// API Login
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(DTOLoginUser userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_userRepo.GetUserByEmail(userModel.Email) == null)
                        throw new Exception();
                    string typedPassword = userModel.Password;
                    var user = _userRepo.GetUserByEmail(userModel.Email);
                    if (HashPassword(typedPassword) == user.Password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userModel.Email)
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, "Login");

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return Redirect("/Home");
                    }
                    else
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Login() -- Error : ", ex);
            }
            return View("Error", new ErrorViewModel());
        }

        /// <summary>
        /// API Second login
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SecondLogin(DTOLoginUser userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_userRepo.GetUserByEmail(userModel.Email) == null)
                        throw new Exception();
                    string typedPassword = userModel.Password;
                    var user = _userRepo.GetUserByEmail(userModel.Email);
                    if (HashPassword(typedPassword) == user.Password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userModel.Email)
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, "Login");

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return Redirect("/User/EditProfile");
                    }
                    else
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Login() -- Error : ", ex);
            }
            return View("Error", new ErrorViewModel());
        }

        /// <summary>
        /// Hash a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Hash</returns>
        private string HashPassword(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// Edit user info
        /// </summary>
        /// <param name="newUserInfos"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditUserInfos(DTOUserEdition newUserInfos)
        {
            try
            {
                var userEmail = HttpContext.User.Identity.Name;
                var userToReInsert = _userRepo.GetUserByEmail(userEmail);
                userToReInsert.Name = newUserInfos.Name;
                userToReInsert.Email = newUserInfos.Email;
                userToReInsert.UserName = newUserInfos.UserName;
                userToReInsert.Gender = newUserInfos.Gender;
                userToReInsert.Address = newUserInfos.Address;
                userToReInsert.Password = HashPassword(newUserInfos.Password);

                var userToUpdate = _userRepo.Update(userToReInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- EditUserInfos() -- Error : ", ex);
            }
            await HttpContext.SignOutAsync();
            return Redirect("/Home");
        }

        /// <summary>
        /// API get user details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("/api/users/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Get(int userId)
        {
            try
            {
                var user = await _userRepo.GetById(userId);
                if (user != null)
                    return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Get() -- Error : ", ex);
            }
            return NotFound();
        }

        /// <summary>
        /// API get public user details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("/api/users/{userId}/public")]
        public async Task<ActionResult> GetPublic(int userId)
        {
            try
            {
                var user = await _userRepo.GetById(userId);
                if (user != null)
                    return Ok(DtoUserToDtoUserPublic(user));
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- GetPublic() -- Error : ", ex);
            }
            return NotFound();
        }

        /// <summary>
        /// API Edit user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("/api/users/editUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromBody] DTOUserEdition userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = await _userRepo.GetById(userModel.Id);
                    if (currentUser == null)
                        return NotFound();
                    if (currentUser.Id != userModel.Id)
                        return BadRequest();
                    var current = DtoUserEditionToDtoUser(userModel);
                    if (_userRepo.GetUserByEmail(currentUser.Email) != null
                        && _userRepo.GetUserByUserName(currentUser.UserName) != null)
                    {
                        var result = await _userRepo.Update(currentUser);
                        if (result != null)
                            return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Edit() -- Error : ", ex);
                return BadRequest();
            }

            return BadRequest();
        }

        /// <summary>
        /// API Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("/api/users/delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([FromBody] long id)
        {
            try
            {
                if (await _userRepo.Delete(id) == false)
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Delete() -- Error : ", ex);
                return BadRequest();
            }

            return Ok(id);
        }

        /// <summary>
        /// API Get user articles
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        [HttpGet("/api/users/{userId}/getArticles")]
        public async Task<ActionResult> GetArticlesFromId(long userId)
        {
            try
            {
                var articles = await _articleRepo.Get();
                var results = articles.Where(a => a.SellerId == userId);
                var privateUser = await _userRepo.GetById(userId);
                var publicUser = DtoUserToDtoUserPublic(privateUser);
                foreach (var result in results)
                {
                    result.User = publicUser;
                }
                if (results.Count() != 0)
                    return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- GetArticlesFromId() -- Error : ", ex);
                return BadRequest();
            }

            return NotFound(userId);
        }


        /// <summary>
        /// Note a user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NoteUser()
        {
            var email = HttpContext.User.Identity.Name;
            var Buyer = _userRepo.GetUserByEmail(email);
         

            var transactionList = new List<DTOArticle>();
            var SellerName = Request.Form["id"].ToString();
            var Seller = _userRepo.GetUserByUserName(SellerName);
            var BuyerTransactions = await _transactionRepo.GetTransactionByUserId(Buyer.Id);
            var articleId = (long)Convert.ToDouble(Request.Form["articleId"].ToString());

            foreach (var transaction in BuyerTransactions)
            {
                var toInsert = new DTOArticle();
                var currentArticle = await _articleRepo.GetById(transaction.ArticleId);
                toInsert.Name = currentArticle.Name;
                toInsert.State = currentArticle.State;
                toInsert.Id = currentArticle.Id;

                if (toInsert.Transaction == null)
                {
                    toInsert.Transaction = new DTOTransaction();
                }
                var sellerToInsert = new DTOUserPublic();
                sellerToInsert.Name = Seller.Name;
                sellerToInsert.UserName = Seller.UserName;
                toInsert.User = sellerToInsert;

                if (currentArticle.Id == articleId)
                {

                    toInsert.Transaction = transaction;

                    if (transaction.TransactionState == "sold")
                    {
                        toInsert.Transaction.TransactionState = "Sold and Noted";
                        transaction.TransactionState = "Sold and Noted";
                    }
                    var upt = await _transactionRepo.Update(transaction);

                    var note = Convert.ToDouble(Request.Form["note"]);
                    Seller.NbNotes += 1;
                    Seller.Note = FindNoteAverage(note, Seller.NbNotes);
                    var test = await _userRepo.Update(Seller);
                    if (test == null)
                    {
                        View("Cart", "Failed");
                    }

                }
                else
                {
                    toInsert.Transaction.TransactionState = transaction.TransactionState;
                }


                transactionList.Add(toInsert);
            }

            ViewData["transactionlist"] = transactionList;

            return RedirectToAction("secondPageManager", new { CurrentarticleId = articleId, sellername = SellerName });
        }

        /// <summary>
        /// Show on a View which transaction have been noted
        /// </summary>
        /// <param name="CurrentarticleId"></param>
        /// <param name="sellername"></param>
        /// <returns>View with updated transactions note status</returns>
        public async Task<IActionResult> secondPageManager(long CurrentarticleId, string sellername)
        {
            var email = HttpContext.User.Identity.Name;
            var Buyer = _userRepo.GetUserByEmail(email);

            var transactionList = new List<DTOArticle>();
            var Seller = _userRepo.GetUserByUserName(sellername);
            var BuyerTransactions = await _transactionRepo.GetTransactionByUserId(Buyer.Id);

            foreach (var transaction in BuyerTransactions)
            {
                var toInsert = new DTOArticle();
                var currentArticle = await _articleRepo.GetById(transaction.ArticleId);
                toInsert.Name = currentArticle.Name;
                toInsert.State = currentArticle.State;
                toInsert.Id = currentArticle.Id;
                if (toInsert.Transaction == null)
                {
                    toInsert.Transaction = new DTOTransaction();
                }
                var sellerToInsert = new DTOUserPublic();
                sellerToInsert.Name = Seller.Name;
                sellerToInsert.UserName = Seller.UserName;
                toInsert.User = sellerToInsert;

                if (currentArticle.Id == CurrentarticleId)
                {

                    toInsert.Transaction = transaction;

                    if (transaction.TransactionState == "sold")
                    {
                        toInsert.Transaction.TransactionState = "Sold and Noted";
                        transaction.TransactionState = "Sold and Noted";
                    }
                    var upt = await _transactionRepo.Update(transaction);
                }
                else
                {
                    toInsert.Transaction.TransactionState = transaction.TransactionState;
                }

                transactionList.Add(toInsert);
            }

            ViewData["transactionlist"] = transactionList;

            return View("Profile_secondPage");
        }

        /// <summary>
        /// Compute note average
        /// </summary>
        /// <param name="note"></param>
        /// <param name="nbNote"></param>
        /// <returns></returns>
        public double FindNoteAverage(double note, double nbNote)
        {
            double val;
            var email = HttpContext.User.Identity.Name;
            var user = _userRepo.GetUserByEmail(email);
            if(nbNote == 1)
            {
                return note;
            }
            else
            {
                var currUserNote = user.Note;
                double pond = currUserNote * nbNote;
                val = (pond + note) / (nbNote + 1);

            }
            return val;
        }
    }
}