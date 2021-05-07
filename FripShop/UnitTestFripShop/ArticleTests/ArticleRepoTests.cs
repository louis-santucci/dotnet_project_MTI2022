using System;
using System.Collections.Generic;
using System.Linq;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestFripShop.ArticleTests
{
    /// <summary>
    /// Class for article CRUD unit testing
    /// </summary>
    [TestClass]
    public class ArticleRepoTests
    {

        /// <summary>
        /// Our Mock Repository for use in testing
        /// </summary>
        public readonly IArticleRepo _mockRepo;

        public List<Article> _articles = new List<Article>()
        {
            new Article()
            {
                Id = 1, ImageSource = null, SellerId = 1, State = "free", Name = "Joli haut", Description = "Joli haut tout bleu", Category = "top", Sex = "woman", Brand = "Converse", Condition = 10, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 2, ImageSource = null, SellerId = 1, State = "free", Name = "Joli bas", Description = "Joli bas tout vert T36", Category = "pants", Sex = "man", Brand = "Levis", Condition = 3, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 3, ImageSource = null, SellerId = 1, State = "free", Name = "Joli veste", Description = "", Category = "top", Sex = "woman", Brand = "Adidas", Condition = 2, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 4, ImageSource = null, SellerId = 1, State = "free", Name = "Jolies chaussures", Description = "", Category = "shoes", Sex = "woman", Brand = "Converse", Condition = 10, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 5, ImageSource = null, SellerId = 1, State = "free", Name = "pantalon", Description = "", Category = "pants", Sex = "woman", Brand = "Dickies", Condition = 9, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 6, ImageSource = null, SellerId = 1, State = "free", Name = "pull kaki", Description = "", Category = "top", Sex = "woman", Brand = "Supreme", Condition = 8, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 7, ImageSource = null, SellerId = 1, State = "free", Name = "serviette verte", Description = "", Category = "accessories", Sex = "woman", Brand = "Nike", Condition = 6, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 8, ImageSource = null, SellerId = 1, State = "free", Name = "Lunettes noires", Description = "Ray Ban noires", Category = "accessories", Sex = "woman", Brand = "RayBan", Condition = 10, CreatedAt = DateTime.Now
            },

        };

        /// <summary>
        /// Constructor for the user CRUD unit tests
        /// </summary>
        public ArticleRepoTests()
        {
            var articleMockRepo = new ArticleMockRepo(_articles);
            this._mockRepo = articleMockRepo._articleRepo;
        }


        [TestMethod]
        public void TestCount()
        {
            var count = this._mockRepo.Count().Result;
            Assert.AreEqual(8, count);
        }

        [TestMethod]
        public void TestGet()
        {
            var articles = _mockRepo.Get();
            Assert.AreEqual(8, articles.Result.Count());
        }

        [TestMethod]
        public void TestGetId()
        {
            var result = _mockRepo.GetById(4);
            Assert.IsNotNull(result);
            var article = result.Result;
            var articleExpect = new DTOArticle()
            {
                Id = 4,
                ImageSource = null,
                SellerId = 1,
                State = "free",
                Name = "Jolies chaussures",
                Description = "",
                Category = "shoes",
                Sex = "woman",
                Brand = "Converse",
                Condition = 10,
            };
            Assert.AreEqual(articleExpect.Id, article.Id);
            Assert.AreEqual(articleExpect.ImageSource, article.ImageSource);
            Assert.AreEqual(articleExpect.SellerId, article.SellerId);
            Assert.AreEqual(articleExpect.State, article.State);
            Assert.AreEqual(articleExpect.Name, article.Name);
            Assert.AreEqual(articleExpect.Description, article.Description);
            Assert.AreEqual(articleExpect.Category, article.Category);
            Assert.AreEqual(articleExpect.Sex, article.Sex);
            Assert.AreEqual(articleExpect.Brand, article.Brand);
        }

        /// <summary>
        /// Test to get wrong id
        /// </summary>
        [TestMethod]
        public void TestGetIdWrong()
        {
            var user = _mockRepo.GetById(10);
            Assert.IsNull(user);
        }

        /// <summary>
        /// Test to insert element
        /// </summary>
        [TestMethod]
        public void TestInsertOk()
        {
            var articleExpect = new DTOArticle()
            {
                Id = 9,
                ImageSource = null,
                SellerId = 1,
                State = "free",
                Name = "Joli pantalon",
                Description = "joli pantalon militaire",
                Category = "pants",
                Sex = "man",
                Brand = null,
                Condition = 6
            };

            var result = _mockRepo.Insert(articleExpect).Result;
            var article = _mockRepo.GetById(9).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(articleExpect.Id, article.Id);
            Assert.AreEqual(articleExpect.ImageSource, article.ImageSource);
            Assert.AreEqual(articleExpect.SellerId, article.SellerId);
            Assert.AreEqual(articleExpect.State, article.State);
            Assert.AreEqual(articleExpect.Name, article.Name);
            Assert.AreEqual(articleExpect.Description, article.Description);
            Assert.AreEqual(articleExpect.Category, article.Category);
            Assert.AreEqual(articleExpect.Sex, article.Sex);
            Assert.AreEqual(articleExpect.Brand, article.Brand);
            var count = this._mockRepo.Count();
            Assert.AreEqual(9, _articles.Count);
        }

        /// <summary>
        /// Test to wrong email insert 
        /// </summary>
        [TestMethod]
        public void TestInsertWrongId()
        {
            var article = new DTOArticle()
            {
                Id = 15,
                ImageSource = null,
                SellerId = 1,
                State = "free",
                Name = "Joli pantalon",
                Description = "joli pantalon militaire",
                Category = "pants",
                Sex = "man",
                Brand = null,
                Condition = 6
            };
            var result = _mockRepo.Insert(article).Result;
            Assert.IsNull(result);
            var count = this._mockRepo.Count();
            Assert.AreEqual(8, count.Result);
        }

        /// <summary>
        /// Test to wrong userName insert
        /// </summary>
        [TestMethod]
        public void TestInsertWrongUserId()
        {
            var article = new DTOArticle()
            {
                Id = 6,
                ImageSource = null,
                SellerId = 2,
                State = "free",
                Name = null,
                Description = "joli pantalon militaire",
                Category = "pants",
                Sex = "man",
                Brand = null,
                Condition = 6
            };
            var result = _mockRepo.Insert(article).Result;
            Assert.IsNull(result);
            var count = this._mockRepo.Count();
            Assert.AreEqual(8, count.Result);
        }

        /// <summary>
        /// Test to update element
        /// </summary>
        [TestMethod]
        public void TestUpdateOk()
        {
            var article = new DTOArticle()
            {
                Id = 8,
                ImageSource = "path-to-photo",
                SellerId = 1,
                State = "free",
                Name = "Lunettes noires",
                Description = "Ray Ban noires",
                Category = "accessories",
                Sex = "woman",
                Brand = "RayBan",
                Condition = 10,
                CreatedAt = DateTime.Now
            };

            var result = _mockRepo.Update(article).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(article.Id, result.Id);
            Assert.AreEqual(article.Description, result.Description);
        }

        /// <summary>
        /// Wrong ID testing update
        /// </summary>
        [TestMethod]
        public void TestUpdateWrongSellerId()
        {
            var updateUser = new DTOArticle
            {
                Id = 9,
                ImageSource = "path-to-photo",
                SellerId = 1,
                State = "free",
                Name = "Lunettes noires",
                Description = "Ray Ban noires",
                Category = "accessories",
                Sex = "woman",
                Brand = "RayBan",
                Condition = 10
            };
            var result = _mockRepo.Update(updateUser).Result;
            Assert.IsNull(result);
            Assert.AreEqual(8, _mockRepo.Count().Result);
        }

        [TestMethod]
        public void TestUpdateWrongId()
        {
            var article = new DTOArticle
            {
                Id = 10,
                ImageSource = "path-to-photo",
                SellerId = 1,
                State = "free",
                Name = "Lunettes noires",
                Description = "Ray Ban noires",
                Category = "accessories",
                Sex = "woman",
                Brand = "RayBan",
                Condition = 10,
                CreatedAt = DateTime.Now
            };
            var result = _mockRepo.Update(article).Result;
            Assert.IsNull(result);
            Assert.AreEqual(8, _mockRepo.Count().Result);
        }

        /// <summary>
        /// Delete element
        /// </summary>
        [TestMethod]
        public void TestDeleteOk()
        {
            var result = _mockRepo.Delete(7);
            Assert.IsTrue(result.Result);
            var search = _mockRepo.GetById(7);
            Assert.IsNull(search);
            var count = _articles.Count();
            Assert.AreEqual(7, count);
        }

        /// <summary>
        /// Wrong id to delete
        /// </summary>
        [TestMethod]
        public void TestDeleteWrong()
        {
            var result = _mockRepo.Delete(9);
            Assert.IsFalse(result.Result);
            var count = this._mockRepo.Count();
            Assert.AreEqual(8, count.Result);
        }

        [TestMethod]
        public void TestGetUserFromId()
        {
            var userTheorical = new DTOUser();
            userTheorical.Id = 1;
            userTheorical.Email = "test@GetUserFromId.fr";
            userTheorical.Address = "3B Rue de La Poste 69110 FRANCHEVILLE";
            userTheorical.Gender = "man";
            userTheorical.Name = "Louis SANTOS";
            userTheorical.Note = 10;
            userTheorical.UserName = "santoss";

            var user = _mockRepo.GetUserFromId(1);
            Assert.AreEqual(userTheorical.Id, user.Id);
            Assert.AreEqual(userTheorical.UserName, user.UserName);
            Assert.AreEqual(userTheorical.Email, user.Email);
            Assert.AreEqual(userTheorical.Password, user.Password);
            Assert.AreEqual(userTheorical.Name, user.Name);
            Assert.AreEqual(userTheorical.Note, user.Note);
            Assert.AreEqual(userTheorical.Gender, user.Gender);
            Assert.AreEqual(userTheorical.Address, user.Address);
        }
    }
}
