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
    public class UserController : Controller
    {
        private readonly IArticleRepo _articleRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IArticleRepo articleRepo, IUserRepo userRepo)
        {
            this._userRepo = userRepo;
            this._articleRepo = articleRepo;
            this._logger = logger;
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public IActionResult RegisterPage()
        {
            return View("Register");
        }

        public IActionResult LoginPage()
        {
            return View("Login");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _userRepo.GetUserByEmail(email);
            DTOLoginUser userToReturn = new DTOLoginUser();
            userToReturn.Name = user.Name;
            userToReturn.Email = user.Email;
            userToReturn.UserName = user.UserName;
            userToReturn.Address = user.Address;
            userToReturn.Gender = user.Gender;
            return View(userToReturn);
        }

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
            user.Note = 10;

            return user;
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

        [HttpPost]
        public async Task<ActionResult> Register(DTOUserEdition userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userRepo.GetUserByEmail(userModel.Email) != null)
                        return BadRequest(userModel);
                    if (await _userRepo.GetUserByUserName(userModel.UserName) != null)
                        return BadRequest(userModel);
                    userModel.Password = HashPassword(userModel.Password);
                    var result = await _userRepo.Insert(DtoUserEditionToDtoUser(userModel));
                    if (result != null)
                        return View("Success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Register() -- Error on db : ", ex);
                return BadRequest();
            }
            return View();
        }

        /// API Calls

        [HttpPost]
        public async Task<ActionResult> Login(DTOLoginUser userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userRepo.GetUserByEmail(userModel.Email) == null)
                        return BadRequest(userModel);
                    string typedPassword = userModel.Password;
                    var user = await _userRepo.GetUserByEmail(userModel.Email);
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
                _logger.LogError("CONTROLLER USER -- Login() -- Error on db : ", ex);
                return BadRequest();
            }
            return BadRequest();
        }

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
                _logger.LogError("CONTROLLER USER -- Get() -- Error on db : ", ex);
            }
            return NotFound();
        }

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
                _logger.LogError("CONTROLLER USER -- GetPublic() -- Error on db : ", ex);
            }
            return NotFound();
        }

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
                _logger.LogError("CONTROLLER USER -- Edit() -- Error on db : ", ex);
                return BadRequest();
            }

            return BadRequest();
        }

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
                _logger.LogError("CONTROLLER USER -- Delete() -- Error on db : ", ex);
                return BadRequest();
            }

            return Ok(id);
        }

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
                _logger.LogError("CONTROLLER USER -- GetArticlesFromId() -- Error on db : ", ex);
                return BadRequest();
            }

            return NotFound(userId);
        }

    }
}
