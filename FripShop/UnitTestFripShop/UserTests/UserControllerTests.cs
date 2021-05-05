using FripShop.Controllers;
using FripShop.DataAccess.Interfaces;
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
            var userMockRepo = new UserMockRepo()._mockRepo;
            var articleMockRepo = new ArticleMockRepo()._articleRepo;
            var cartMockRepo = new CartMockRepo()._mockRepo;
            var userController = new UserController(_logger, articleMockRepo, userMockRepo, cartMockRepo);
        }
    }
}
