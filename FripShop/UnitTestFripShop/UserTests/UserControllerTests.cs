using FripShop.Controllers;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTestFripShop.ArticleTests;
using UnitTestFripShop.CartTests;

namespace UnitTestFripShop.UserTests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UserController>> _loggerMock = new Mock<ILogger<UserController>>();

        [TestMethod]
        public void RegisterFirstValidUser()
        { 
            var userMockRepo = new UserMockRepo(new List<User>());
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var user = new DTOUserEdition
            {
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man"
            };

            var preCount = userMockRepo._usersMockList.Count;

            userController.Register(user).Wait();

            var postCount = userMockRepo._usersMockList.Count;

            Assert.AreEqual(preCount + 1, postCount);
        }

        [TestMethod]
        public void RegisterFirstInvalidUser()
        {
            var userMockRepo = new UserMockRepo(new List<User>());
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var user = new DTOUserEdition
            {
                UserName = "username",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man"
            };

            var preCount = userMockRepo._usersMockList.Count;

            userController.Register(user).Wait();

            var postCount = userMockRepo._usersMockList.Count;

            Assert.AreEqual(preCount, postCount);
        }

        [TestMethod]
        public void RegisterExistingdUser()
        {
            var user = new User
            {
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man"
            };
            var userMockRepo = new UserMockRepo(new List<User> { user });
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var newUser = new DTOUserEdition
            {
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man"
            };

            var preCount = userMockRepo._usersMockList.Count;

            userController.Register(newUser).Wait();

            var postCount = userMockRepo._usersMockList.Count;

            Assert.AreEqual(preCount, postCount);
        }

        [TestMethod]
        public void RegisterValidUser()
        {
            var userMockRepo = new UserMockRepo();
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var user = new DTOUserEdition
            {
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man"
            };

            var preCount = userMockRepo._usersMockList.Count;

            userController.Register(user).Wait();

            var postCount = userMockRepo._usersMockList.Count;

            Assert.AreEqual(preCount + 1, postCount);
        }

        [TestMethod]
        public void AddValidArticle()
        {
            User user = new User {
                Id = 1, UserName = "test1", Email = "test1@valid.fr", Password = "pswd1", Name = "Test 1",
                Address = "Test 1\nNew York", Gender = "man", Note = 10
            };
            var userMockRepo = new UserMockRepo(new List<User> { user });

            Article article = new Article
            {
                Id = 1,
                Brand = "brand",
                Category = "top",
                Condition = 10,
                CreatedAt = DateTime.Now,
                Description = "description",
                ImageSource = "path",
                Name = "name",
                Price = 69,
                SellerId = 1,
                Sex = "man",
                State = "free"
            };
            var articleMockRepo = new ArticleMockRepo(new List<Article> { article });
            var cartMockRepo = new CartMockRepo(new List<Cart>());
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            userController.AddArticle(1, "test1@valid.fr").Wait();

            var cartCount = cartMockRepo._cartsMockList.Count;

            var cartItem = cartMockRepo._cartsMockList.Find(c => (c.ArticleId == 1 && c.BuyerId == 1));

            Assert.AreEqual(cartCount, 1);
            Assert.IsNotNull(cartItem);
        }

        [TestMethod]
        public void AddExistingArticle()
        {
            User user = new User
            {
                Id = 1,
                UserName = "test1",
                Email = "test1@valid.fr",
                Password = "pswd1",
                Name = "Test 1",
                Address = "Test 1\nNew York",
                Gender = "man",
                Note = 10
            };
            var userMockRepo = new UserMockRepo(new List<User> { user });

            Article article = new Article
            {
                Id = 1,
                Brand = "brand",
                Category = "top",
                Condition = 10,
                CreatedAt = DateTime.Now,
                Description = "description",
                ImageSource = "path",
                Name = "name",
                Price = 69,
                SellerId = 1,
                Sex = "man",
                State = "free"
            };
            var articleMockRepo = new ArticleMockRepo(new List<Article> { article });
            var cartMockRepo = new CartMockRepo(new List<Cart>());
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            userController.AddArticle(1, "test1@valid.fr").Wait();

            var cartCount = cartMockRepo._cartsMockList.Count;

            userController.AddArticle(1, "test1@valid.fr").Wait();

            var postCount = cartMockRepo._cartsMockList.Count;

            var cartItem = cartMockRepo._cartsMockList.Find(c => (c.ArticleId == 1 && c.BuyerId == 1));

            Assert.AreEqual(cartCount, 1);
            Assert.AreEqual(cartCount, postCount);
            Assert.IsNotNull(cartItem);
        }

        [TestMethod]
        public void GetValidUser()
        {
            var user = new User
            {
                Id = 1,
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man",
                Note = 10
            };

            var userMockRepo = new UserMockRepo(new List<User> { user });
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var userGet = userController.Get(1).Result;

            Assert.IsNotNull(userGet);
        }

        [TestMethod]
        public void GetInexistantUser()
        {
            var user = new User
            {
                Id = 1,
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man",
                Note = 10
            };

            var userMockRepo = new UserMockRepo(new List<User> { user });
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var userGet = userController.Get(2).Result;
            try
            {
                NotFoundResult res = (NotFoundResult)userGet;
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetUserNoUser()
        {
            var userMockRepo = new UserMockRepo(new List<User>());
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var userGet = userController.Get(1).Result;
            try
            {
                NotFoundResult res = (NotFoundResult)userGet;
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteNoUser()
        {
            var userMockRepo = new UserMockRepo(new List<User>());
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var preCount = userMockRepo._usersMockList.Count;

            var userGet = userController.Delete(1).Result;

            var postCount = userMockRepo._usersMockList.Count;

            Assert.AreEqual(preCount, postCount);

            try
            {
                NotFoundResult res = (NotFoundResult)userGet;
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteInexistantUser()
        {
            var user = new User
            {
                Id = 1,
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man",
                Note = 10
            };

            var userMockRepo = new UserMockRepo(new List<User> { user });
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var preCount = userMockRepo._usersMockList.Count;

            var userGet = userController.Delete(2).Result;

            var postCount = userMockRepo._usersMockList.Count;

            Assert.AreEqual(preCount, postCount);

            try
            {
                NotFoundResult res = (NotFoundResult)userGet;
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            var user = new User
            {
                Id = 1,
                UserName = "username",
                Email = "user.name@mail.com",
                Password = "password",
                Name = "User Name",
                Address = "1 Address St.",
                Gender = "man",
                Note = 10
            };

            var userMockRepo = new UserMockRepo(new List<User> { user });
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_loggerMock.Object, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo, null);

            var preCount = userMockRepo._usersMockList.Count;

            var userGet = userController.Delete(1).Result;

            var postCount = userMockRepo._usersMockList.Count;

            Assert.AreEqual(preCount - 1, postCount);

            try
            {
                OkObjectResult res = (OkObjectResult)userGet;
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
