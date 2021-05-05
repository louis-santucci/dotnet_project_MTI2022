using System;
using System.Collections.Generic;
using FripShop.DataAccess;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestFripShop.ArticleTests
{
    /// <summary>
    /// Class for article CRUD unit testing
    /// </summary>
    [TestClass]
    class ArticleRepoTests
    {

        /// <summary>
        /// Our Mock Article Repository for use in testing
        /// </summary>
        public IArticleRepo _articleRepo;

        public List<Article> _articleMockList;


        /// <summary>
        /// Constructor for the article CRUD unit tests
        /// </summary>
        public ArticleRepoTests()
        {
            _articleMockList = new List<Article>
            {
                
            };

            Mock<IArticleRepo> mockRepo = new Mock<IArticleRepo>();

            // Mocks the function Get()
            mockRepo.Setup(articleRepo => articleRepo.Get("")).ReturnsAsync();

            // Mocks the function Insert()
            mockRepo.Setup(articleRepo => articleRepo.Insert(It.IsAny<DTOUser>())).ReturnsAsync((DTOUser userModel) =>
            {
                var max = Math.Max(_usersMockList.Max(c => c.Id) + 1, _usersMockList.Max(c => c.Id));
                var user = DTOToDBO(userModel);
                user.Id = max;
                var users = _mockRepo.Get();
                if (users.Result.Count(c => c.Email == userModel.Email || c.UserName == userModel.UserName) != 0)
                    return null;
                this._usersMockList.Add(user);
                return DBOToDTO(user);
            });

            // Mocks the function Update()
            mockRepo.Setup(articleRepo => articleRepo.Update(It.IsAny<DTOUser>())).ReturnsAsync((DTOUser userModel) =>
            {
                var user = _usersMockList.Single(c => c.Id == userModel.Id);
                if (user == null)
                    return null;
                user.Address = userModel.Address;
                user.Name = userModel.Name;
                user.Gender = userModel.Gender;
                user.Note = userModel.Note;
                user.Password = userModel.Password;
                user.UserName = userModel.UserName;
                user.Email = userModel.Email;
                return DBOToDTO(user);
            });

            // Mocks the function Delete()
            mockRepo.Setup(articleRepo => articleRepo.Delete(It.IsAny<long>())).ReturnsAsync((long i) =>
            {
                var count = _usersMockList.Count(c => c.Id == i);
                if (count == 1)
                {
                    _usersMockList.Remove(_usersMockList.Single(c => c.Id == i));
                    return true;
                }

                return false;
            });

            // Mocks the function GetById()
            mockRepo.Setup(articleRepo => articleRepo.GetById(It.IsAny<long>())).Returns((long i) =>
            {
                var count = _usersMockList.Count(c => c.Id == i);
                if (count != 1)
                {
                    return null;
                }
                return DBOToDTOAsync(_usersMockList.Single(c => c.Id == i));
            });

            // Mocks the function Count()
            mockRepo.Setup(articleRepo => articleRepo.Count()).ReturnsAsync(_usersMockList.Count());

            // Mocks the function GetUserByEmail()
            mockRepo.Setup(articleRepo => articleRepo.GetUserByEmail(It.IsAny<string>())).Returns((string email) =>
            {
                if (_usersMockList.Count(c => c.Email == email) == 0)
                {
                    return null;
                }

                return DBOToDTO(_usersMockList.Single(c => c.Email == email));
            });

            // Mocks the function GetUserByUserName()
            mockRepo.Setup(articleRepo => articleRepo.GetUserByUserName(It.IsAny<string>())).Returns((string userName) =>
            {
                if (_usersMockList.Count(c => c.UserName == userName) == 0)
                {
                    return null;
                }

                return DBOToDTO(_usersMockList.Single(c => c.UserName == userName));
            });

            this._articleRepo = mockRepo.Object;
        }
    }



        [TestMethod]
        public void Test()
        {

        }
    }
}
