using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;

namespace FripShop.Controllers
{
    /// <summary>
    /// Controller for the articles
    /// </summary>
    public class ArticleController : Controller
    {
        private readonly IArticleRepo _articleRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILogger<ArticleController> _logger;

        /// <summary>
        /// Public constructor for article controller
        /// </summary>
        /// <param name="articleRepo">Article repository for dependancy injection</param>
        /// <param name="userRepo">User repository for dependancy injection</param>
        /// <param name="logger">Logger for dependancy injection</param>
        public ArticleController(IArticleRepo articleRepo, IUserRepo userRepo, ILogger<ArticleController> logger)
        {
            _articleRepo = articleRepo;
            _userRepo = userRepo;
            _logger = logger;
        }

        public async Task<ActionResult> Index(string gender = null, string category = null,
                                                string minPrice = null, string maxPrice = null, string conditionMin = null,
                                                string sortBy = null, string ascending = null, string search = null,
                                                string page = null, string pageSize= null)
        {
            IEnumerable<DTOArticle> resArticles = null;

            try
            {
                Tuple<float, float> priceTuple = null;
                if (minPrice != null || maxPrice != null)
                {
                    float min = 0f;
                    float max = float.MaxValue;
                    if (minPrice != null)
                        min = float.Parse(minPrice);
                    if (maxPrice != null)
                        max = float.Parse(maxPrice);
                    priceTuple = Tuple.Create(min, max);
                }

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

                int pageSizeRes = 12;
                if (pageSize != null)
                    pageSizeRes = int.Parse(pageSize);

                int pageNumber = 1;
                if (page != null)
                    pageNumber = int.Parse(page);



                resArticles = await GetArticles(gender, category, priceTuple, cond, comp, asc, search, pageNumber, pageSizeRes);
            }
            catch
            {
                resArticles = await GetArticles();
            }

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
        /// <returns>List of articles in fonction of the different filters</returns>
        public async Task<IEnumerable<DTOArticle>> GetArticles(string gender = null, string category = null,
                                                        Tuple<float, float> price = null, int conditionMin = 0,
                                                        Comparison comparison = Comparison.Date, bool ascending = false,
                                                        string search = null, int page = 1, int pageSize = 12) // Filters
        {
            var res = new List<DTOArticle>();

            // Pagination var init
            var articleCount = await _articleRepo.Count();
            var pageStartIndex = (page - 1) * pageSize;
            var pageCurrentIndex = 0;

            foreach (var element in await _articleRepo.Get())
            {
                        if ((pageCurrentIndex < pageStartIndex) ||
                            (pageCurrentIndex >= page * pageSize))
                {
                    pageCurrentIndex++;
                    continue;
                }
                    
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
                if (category != null)
                {
                    if (element.Category != category)
                        continue;
                }
                if (price != null)
                {
                    if (element.Price > price.Item2 || element.Price < price.Item1)
                        continue;
                }
                if (conditionMin > element.Condition)
                    continue;
                res.Add(element);
                pageCurrentIndex++;
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
                    res = @ascending
                        ? res.OrderBy(x => x.User.Note).ToList()
                        : res.OrderByDescending(x => x.User.Note).ToList();
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> CreateArticleView()
        {
            return View("Create");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditArticleView(DTOArticleEdition article)
        {
            var email = HttpContext.User.Identity.Name;
            var user = _userRepo.GetUserByEmail(email);

            var userArticleList = await _articleRepo.GetAllArticlesFromUserId(user.Id);

            ViewData["UserArticleList"] = userArticleList;
        

            return View("Edit");
        }

        [Authorize]
        public async Task<ActionResult> ModifyArticle(long articleId)
        {
            var articleToModify = await _articleRepo.GetArticleFromId(articleId);

            ViewData["ArticleToModify"] = articleToModify;
            ViewData["imageSource"] = articleToModify.ImageSource;
            ViewData["state"] = articleToModify.State;
            ViewData["name"] = articleToModify.Name;
            ViewData["price"] = articleToModify.Price;
            ViewData["description"] = articleToModify.Description;
            ViewData["category"] = articleToModify.Category;
            ViewData["sex"] = articleToModify.Sex;
            ViewData["brand"] = articleToModify.Brand;
            ViewData["condition"] = articleToModify.Condition;
            ViewData["id"] = articleToModify.Id;

            return View("Edit_secondPage");
        }

        [Authorize]
        public async Task<ActionResult> EditArticle()
        {
            var articleId = Request.Form["id"].ToString();
            var articleToModify = await _articleRepo.GetArticleFromId((long)Convert.ToDouble(articleId));
            ViewData["ArticleToModify"] = articleToModify;

            var name = Request.Form["name"].ToString();
        //    var imageSource = Request.Form["imageSource"].ToString();
            var sex = Request.Form["sex"].ToString();
            var price = Request.Form["price"].ToString();
            var category = Request.Form["category"].ToString();
            var condition = Request.Form["condition"].ToString();
            var brand = Request.Form["brand"].ToString();
            var description = Request.Form["description"].ToString();

            articleToModify.Name = name;
    //        articleToModify.ImageSource = articleToModify.ImageSource;
            articleToModify.Sex = sex;
            articleToModify.Price = Convert.ToDouble(price);
            articleToModify.Category = category;
            articleToModify.Condition = Convert.ToInt32(condition);
            articleToModify.Brand = brand;
            articleToModify.Description = description;

            var test = _articleRepo.Update(articleToModify);
            if (test == null)
            {
                View();
            }

            return View("Edit_thirdpage");
        }

        public string GetImage(string path)
        {
            var full_path = Path.Combine("~/wwwroot/ArticleImages/", path);

            if (System.IO.File.Exists(full_path))
            {
                return full_path;
            }

            return "";
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
                    var user = _articleRepo.GetUserFromId(result.Id);
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
                var user = _articleRepo.GetUserFromId(article.Id);
                article.User = DtoUserToDtoUserPublic(user);
                return Ok(article);
            }

            return NotFound();
        }

        [HttpGet("/api/articles/{articleId}/getUser")]
        public ActionResult GetUserFromArticle(long articleId)
        {
            var user = _articleRepo.GetUserFromId(articleId);
            if (user != null)
                return Ok(user);
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateArticle(DTOArticleEdition article)
        {
            try
            {
                var claimsUserEmail = HttpContext.User.Identity.Name;

                var user = _userRepo.GetUserByEmail(claimsUserEmail);
                var imagePath = "wwwroot/ArticleImages/";

                var newArticle = DTOArticleEditionToArticle(article, user);
                string filename = null;
                string extension = null;
                if (article.ImageFile != null)
                {
                    filename = Path.GetFileName(article.ImageFile.FileName);
                    extension = Path.GetExtension(article.ImageFile.FileName);
                }
                
                if (filename == null || extension == null)
                    newArticle.ImageSource = null;
                else
                {
                    var newFilename = Path.GetRandomFileName() + extension;
                    string path = Path.Combine(imagePath, newFilename);
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }
                    while (System.IO.File.Exists(path))
                    {
                        newFilename = Path.Combine(Path.GetRandomFileName(), extension);
                        path = Path.Combine(imagePath, newFilename);
                    }

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await article.ImageFile.CopyToAsync(fileStream);
                    }

                    newArticle.ImageSource = "./ArticleImages/" + newFilename;

                }
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

            return View("Create");
                }
    }
}
