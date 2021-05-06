using FripShop.Controllers;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
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
        private readonly ILogger<UserController> _logger;

        [TestMethod]
        public void RegisterFirstValidUser()
        { 
            var userMockRepo = new UserMockRepo();
            var articleMockRepo = new ArticleMockRepo();
            var cartMockRepo = new CartMockRepo();
            var userController = new UserController(_logger, articleMockRepo._articleRepo, userMockRepo._mockRepo, cartMockRepo._mockRepo);

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
    }
}
