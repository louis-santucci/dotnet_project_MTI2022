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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([FromBody] DTOUser userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_userRepo.GetUserByEmail(userModel.Email) != null)
                        return BadRequest(userModel);
                    if (_userRepo.GetUserByUserName(userModel.UserName) != null)
                        return BadRequest(userModel);
                    var result = await _userRepo.Insert(userModel);
                    if (result != null)
                        return Created(result.Id.ToString(), result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Register() -- Error on db : ", ex);
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var user = await _userRepo.GetById(id);
                if (user != null)
                    return Ok(User);
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER USER -- Get() -- Error on db : ", ex);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromBody] DTOUser userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = await _userRepo.GetById(userModel.Id);
                    if (currentUser == null)
                        return NotFound();
                    currentUser.Gender = userModel.Gender;
                    currentUser.Name = userModel.Name;
                    currentUser.UserName = userModel.UserName;
                    currentUser.Password = userModel.Password;
                    currentUser.Address = userModel.Address;
                    currentUser.Email = userModel.Email;

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

        [HttpPost]
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
    }
}
