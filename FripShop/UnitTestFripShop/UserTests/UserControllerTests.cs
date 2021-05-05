using FripShop.Controllers;
using FripShop.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestFripShop.UserTests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly ILogger _logger;

        [TestMethod]
        public void RegisterFirstValidUser()
        { 
            var userMockRepo = new UserMockRepo()._mockRepo;
            //var userController = new UserController(_logger, userMockRepo);
        }
    }
}
