using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FripShop.Views.Home
{
    public class ArticleListModel : PageModel
    {

        private readonly ILogger<ArticleListModel> _logger;
        private readonly DataAccess.Interfaces.IArticleRepo _articleRepo;
        private readonly DataAccess.Interfaces.IUserRepo _userRepo;
        private string _sessionId;

        public enum Comparison
        {
            Date,
            Price,
            Condition,
            SellerRate
        }


        public ArticleListModel(ILogger<ArticleListModel> logger, DataAccess.Interfaces.IArticleRepo articleRepo, IUserRepo userRepo)
        {
            _logger = logger;
            _articleRepo = articleRepo;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Gets all the articles in function of the parameters given
        /// </summary>
        /// <param name="gender">Either Male, Female, Unisex or Child</param>
        /// <param name="categories">List of categories of clothes</param>
        /// <param name="price">Tuple containing the price range</param>
        /// <param name="comparison">Type of the sorting parameter</param>
        /// <param name="ascending">Boolean to indicate the direction of the sorting algorithm</param>
        public async Task<List<DboArticle>> OnGetAsync(string gender = null, List<string> categories = null,
                                                        Tuple<float, float> price = null, int conditionMin = 0,
                                                        Comparison comparison = Comparison.Date, bool ascending = false,
                                                        string search = null) // Filters
        {
            var res = new List<DboArticle>();
            foreach (var element in _articleRepo.Get().Result)
            {
                if (search != null)
                {
                    if (element.Description.ToLower().Contains(search.ToLower()) ||
                        element.Name.ToLower().Contains(search.ToLower()) ||
                        element.Brand.ToLower().Contains(search.ToLower()))
                        continue;
                }
                if (gender != null)
                {
                    if (gender != element.Sex)
                        continue;
                }
                if (categories != null)
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
                case Comparison.SellerRate:
                    //TODO
                    break;
            }
            return res;
        }
    }
}
