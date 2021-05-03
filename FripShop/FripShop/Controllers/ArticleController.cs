﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

        public async Task<ActionResult> Index(string gender = null, List<string> categories = null,
                                                        string minPrice = null, string maxPrice = null, string conditionMin = null,
                                                        string sortBy = null, string ascending = null,
                                                        string search = null)
        {
            Tuple<float, float> priceTuple = null;
            if (minPrice != null && maxPrice != null)
                priceTuple = Tuple.Create(float.Parse(minPrice), float.Parse(maxPrice));

            int cond = 0;
            if (conditionMin != null)
                cond = int.Parse(conditionMin);

            Comparison comp = Comparison.Date;
            if (sortBy != null)
                switch (sortBy)
                {
                    case "price":
                        comp = Comparison.Price;
                        break;
                    case "condition":
                        comp = Comparison.Condition;
                        break;
                    case "rating":
                        comp = Comparison.SellerRating;
                        break;
                    default:
                        break;
                }

            bool asc = true;
            if (ascending != null && ascending == "false")
                asc = false;

            var resArticles = await GetArticles(gender, categories, priceTuple, cond, comp, asc, search);
            return View(resArticles);
        }

        /// Controller functions

        public enum Comparison
        {
            Date,
            Price,
            Condition,
            SellerRating
        }

        /// <summary>
        /// Gets all the articles in function of the parameters given
        /// </summary>
        /// <param name="gender">Either Male, Female, Unisex or Child</param>
        /// <param name="categories">List of categories of clothes</param>
        /// <param name="price">Tuple containing the price range</param>
        /// <param name="comparison">Type of the sorting parameter</param>
        /// <param name="ascending">Boolean to indicate the direction of the sorting algorithm</param>
        public async Task<IEnumerable<DTOArticle>> GetArticles(string gender = null, List<string> categories = null,
                                                        Tuple<float, float> price = null, int conditionMin = 0,
                                                        Comparison comparison = Comparison.Date, bool ascending = false,
                                                        string search = null) // Filters
        {
            var res = new List<DTOArticle>();

            foreach (var element in await _articleRepo.Get())
            {
                if (element.State.ToLower() != "free")
                    continue;
                if (search != null)
                {
                    if (element.Description.ToLower().Contains(search.ToLower()) ||
                        element.Name.ToLower().Contains(search.ToLower()) ||
                        element.Brand.ToLower().Contains(search.ToLower()))
                        continue;
                }
                if (gender != null)
                {
                    switch (gender)
                    {
                        case "child":
                            if (element.Sex.ToLower() != "child")
                                continue;
                            break;
                        case "man":
                            if (element.Sex.ToLower() != "man" && element.Sex.ToLower() != "unisex")
                                continue;
                            break;
                        case "woman":
                            if (element.Sex.ToLower() != "woman" && element.Sex.ToLower() != "unisex")
                                continue;
                            break;
                        case "unisex":
                            if (element.Sex.ToLower() != "unisex")
                                continue;
                            break;
                        default:
                            continue;
                    }
                }
                if (categories != null && categories.Count > 0)
                {
                    if (!categories.Exists(c => c.Equals(element.Category)))
                        continue;
                }
                if (price != null)
                {
                    if (element.Price > price.Item2 || element.Price < price.Item1)
                        continue;
                }
                res.Add(element);
            }
            // Filter OK
            switch (comparison)
            {
                case Comparison.Date:
                    res = @ascending ? res.OrderBy(x => x.CreatedAt).ToList() : res.OrderByDescending(x => x.CreatedAt).ToList();
                    break;
                case Comparison.Condition:
                    res = @ascending ? res.OrderBy(x => x.Condition).ToList() : res.OrderByDescending(x => x.Condition).ToList();
                    break;
                case Comparison.Price:
                    res = @ascending ? res.OrderBy(x => x.Price).ToList() : res.OrderByDescending(x => x.Price).ToList();
                    break;
                case Comparison.SellerRating:
                    //TODO
                    break;
            }

            return res;
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

        public static DTOArticle DTOArticleEditionToArticle(DTOArticleEdition article, DTOUser user)
        {
            var newArticle = new DTOArticle();
            newArticle.Brand = article.Brand;
            newArticle.Category = article.Category;
            newArticle.Condition = article.Condition;
            newArticle.State = "Free";
            newArticle.CreatedAt = DateTime.Now;
            newArticle.Description = article.Description;
            newArticle.ImageSource = article.ImageSource;
            newArticle.Name = article.Name;
            newArticle.Price = article.Price;
            newArticle.SellerId = user.Id;
            newArticle.Sex = article.Sex;
            newArticle.User = DtoUserToDtoUserPublic(user);
            return newArticle;
        }

        /// API Calls
        [HttpGet("/api/articles/")]
        public async Task<ActionResult> GetAll()
        {
            var results = await this._articleRepo.Get();
            foreach (var result in results)
            {
                var user = await _articleRepo.GetUserFromId(result.Id);
                result.User = DtoUserToDtoUserPublic(user);
            }
            return Ok(results);
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateArticle(DTOArticleEdition article)
        {
            try
            {
                var claimsUserEmail = HttpContext.User.Identity.Name;
                var user = await _userRepo.GetUserByEmail(claimsUserEmail);
                var newArticle = DTOArticleEditionToArticle(article, user);
                var result = await _articleRepo.Insert(newArticle);
                if (result != null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CONTROLLER ARTICLE -- CreateArticle() -- Error : ", ex);
                return BadRequest();
            }

            return View();
        }
    }
}
