using System.Collections.Generic;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestFripShop
{
    /// <summary>
    /// Class for user CRUD unit testing
    /// </summary>
    [TestClass]
    public class UserTests
    {

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Our Mock Products Repository for use in testing
        /// </summary>
        public readonly IUserRepo _mockRepo;

        /// <summary>
        /// Constructor for the user CRUD unit tests
        /// </summary>
        public UserTests()
        {
            IEnumerable<User> users = new List<User>
            {
                new User
                {
                    Id = 1, UserName = "test1", Email = "test2@valid.fr", Password = "pswd1", Name = "Test 1",
                    Address = "Test 1\nNew York", Gender = "man", Note = 10
                },
                new User
                {
                    Id = 2, UserName = "test2", Email = "test3@valid.fr", Password = "pswd2", Name = "Test 2",
                    Address = "Test 2\nParis", Gender = "woman", Note = 10
                },
                new User
                {
                    Id = 3, UserName = "test3", Email = "test4@valid.fr", Password = "pswd3", Name = "Test 3",
                    Address = "Test 3\nMiami", Gender = "child", Note = 10
                },
                new User
                {
                    Id = 4, UserName = "test4", Email = "test5@valid.fr", Password = "pswd4", Name = "Test 4",
                    Address = "Test 4\nNew York", Gender = "woman", Note = 10
                },
                new User
                {
                    Id = 5, UserName = "test5", Email = "test6@valid.fr", Password = "pswd5", Name = "Test 5",
                    Address = "Test 5\nNew York", Gender = "man", Note = 10
                },
                new User
                {
                    Id = 6, UserName = "test6", Email = "test7@valid.fr", Password = "pswd6", Name = "Test 6",
                    Address = "Test 6\nNew York", Gender = "woman", Note = 10
                },
                new User
                {
                    Id = 7, UserName = "test7", Email = "test8@valid.fr", Password = "pswd7", Name = "Test 7",
                    Address = "Test 7\nNew York", Gender = "man", Note = 10
                }
            };

            Mock<IUserRepo> mockRepo = new Mock<IUserRepo>();
        }
        
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
