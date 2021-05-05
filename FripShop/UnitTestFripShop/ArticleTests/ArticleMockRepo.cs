using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Moq;

namespace UnitTestFripShop.ArticleTests
{
    /// <summary>
    /// Our Mock Article Repository for use in testing
    /// </summary>
    class ArticleMockRepo
    {
        public readonly IArticleRepo _articleRepo;

        public List<Article> _articlesMockList;

        /// <summary>
        /// Converts a database entity to a dto entity asynchronously
        /// </summary>
        /// <param name="user">The user to convert</param>
        /// <returns>the converted user</returns>
        public static async Task<DTOArticle> DBOToDTOAsync(Article articleModel)
        {
            var article = new DTOArticle();

            article.Id = articleModel.Id;
            article.ImageSource = articleModel.ImageSource;
            article.SellerId = articleModel.SellerId;

            article.Name = articleModel.Name;
            article.Price = articleModel.Price;
            article.Description = articleModel.Description;
            article.Category = articleModel.Category;
            article.Sex = articleModel.Sex;
            article.Brand = articleModel.Brand;
            article.Condition = articleModel.Condition;
            article.CreatedAt = articleModel.CreatedAt;

            return article;
        }

        /// <summary>
        /// Converts a database entity to a dto entity
        /// </summary>
        /// <param name="user">The article to convert</param>
        /// <returns>the converted article</returns>
        public static DTOArticle DBOToDTO(Article articleModel)
        {
            var article = new DTOArticle();

            article.Id = articleModel.Id;
            article.ImageSource = articleModel.ImageSource;
            article.SellerId = articleModel.SellerId;

            article.Name = articleModel.Name;
            article.Price = articleModel.Price;
            article.Description = articleModel.Description;
            article.Category = articleModel.Category;
            article.Sex = articleModel.Sex;
            article.Brand = articleModel.Brand;
            article.Condition = articleModel.Condition;
            article.CreatedAt = articleModel.CreatedAt;

            return article;
        }

        /// <summary>
        /// Converts a database entity to a dto entity
        /// </summary>
        /// <param name="user">The user to convert</param>
        /// <returns>the converted user</returns>
        public static Article DTOToDBO(DTOArticle articleModel)
        {
            var article = new Article();

            article.Id = articleModel.Id;
            article.ImageSource = articleModel.ImageSource;
            article.SellerId = articleModel.SellerId;

            article.Name = articleModel.Name;
            article.Price = articleModel.Price;
            article.Description = articleModel.Description;
            article.Category = articleModel.Category;
            article.Sex = articleModel.Sex;
            article.Brand = articleModel.Brand;
            article.Condition = articleModel.Condition;
            article.CreatedAt = articleModel.CreatedAt;

            return article;
        }

        /// <summary>
        /// Converts a database entity to a dto entity list
        /// </summary>
        /// <param name="userModels">The user list to convert</param>
        /// <returns>the converted user list</returns>
        public static List<DTOArticle> DBOTODTOList(IEnumerable<Article> articleModels)
        {
            List<DTOArticle> result = new List<DTOArticle>();
            foreach (var articleModel in articleModels)
            {
                var article = new DTOArticle();

                article.Id = articleModel.Id;
                article.ImageSource = articleModel.ImageSource;
                article.SellerId = articleModel.SellerId;

                article.Name = articleModel.Name;
                article.Price = articleModel.Price;
                article.Description = articleModel.Description;
                article.Category = articleModel.Category;
                article.Sex = articleModel.Sex;
                article.Brand = articleModel.Brand;
                article.Condition = articleModel.Condition;
                article.CreatedAt = articleModel.CreatedAt;

                result.Add(article);
            }

            return result;
        }

        /// <summary>
        /// Public constructor for the mocked repository
        /// </summary>
        public ArticleMockRepo(List<Article> articles)
        {

            _articlesMockList = articles;

            Mock<IArticleRepo> mockRepo = new Mock<IArticleRepo>();

            // Mocks the function Get()
            mockRepo.Setup(articleRepo => articleRepo.Get("")).ReturnsAsync(DBOTODTOList(_articlesMockList));

            // Mocks the function Insert()
            mockRepo.Setup(articleRepo => articleRepo.Insert(It.IsAny<DTOArticle>())).ReturnsAsync((DTOArticle articleModel) =>
            {
                var max = Math.Max(_articlesMockList.Max(c => c.Id) + 1, _articlesMockList.Max(c => c.Id));
                var user = DTOToDBO(articleModel);
                user.Id = max;
                if (articleModel.Name == null)
                    return null;
                this._articlesMockList.Add(user);
                return DBOToDTO(user);
            });

            // Mocks the function Update()
            mockRepo.Setup(articleRepo => articleRepo.Update(It.IsAny<DTOArticle>())).ReturnsAsync((DTOArticle articleModel) =>
            {
                var result = _articlesMockList.Where(c => c.Id == articleModel.Id);
                if (result.Count() != 1)
                    return null;
                var article = result.First();
                if (article.SellerId != articleModel.SellerId)
                    return null;
                article.ImageSource = articleModel.ImageSource;
                article.Name = articleModel.Name;
                article.Price = articleModel.Price;
                article.Description = articleModel.Description;
                article.Category = articleModel.Category;
                article.Sex = articleModel.Sex;
                article.Brand = articleModel.Brand;
                article.Condition = articleModel.Condition;
                return DBOToDTO(article);
            });

            // Mocks the function Delete()
            mockRepo.Setup(articleRepo => articleRepo.Delete(It.IsAny<long>())).ReturnsAsync((long i) =>
            {
                var articles = _articlesMockList.Where(c => c.Id == i);
                if (articles.Count() == 1)
                {
                    _articlesMockList.Remove(articles.First());
                    return true;
                }

                return false;
            });

            // Mocks the function GetById()
            mockRepo.Setup(articleRepo => articleRepo.GetById(It.IsAny<long>())).Returns((long i) =>
            {
                var count = _articlesMockList.Count(c => c.Id == i);
                if (count != 1)
                {
                    return null;
                }
                return DBOToDTOAsync(_articlesMockList.Single(c => c.Id == i));
            });

            // Mocks the function Count()
            mockRepo.Setup(articleRepo => articleRepo.Count()).ReturnsAsync(_articlesMockList.Count());

            // Mocks the function GetUserByEmail()
            mockRepo.Setup(articleRepo => articleRepo.GetArticleFromId(It.IsAny<long>())).Returns((long i) =>
            {
                if (_articlesMockList.Count(c => c.Id == i) == 0)
                {
                    return null;
                }

                return DBOToDTOAsync(_articlesMockList.Single(c => c.Id == i));
            });

            // Mocks the function GetUserByUserName()
            mockRepo.Setup(articleRepo => articleRepo.GetUserFromId(It.IsAny<long>())).Returns((long i) =>
            {
                if (_articlesMockList.Count(c => c.Id == i) == 0)
                {
                    return null;
                }

                var user = new DTOUser();
                user.Id = 1;
                user.Email = "test@GetUserFromId.fr";
                user.Address = "3B Rue de La Poste 69110 FRANCHEVILLE";
                user.Gender = "man";
                user.Name = "Louis SANTOS";
                user.Note = 10;
                user.UserName = "santoss";

                return user;
            });

            this._articleRepo = mockRepo.Object;
        }
    }
}
