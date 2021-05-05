using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
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
        /// Our Mock User Repository for use in testing
        /// </summary>
        public readonly IUserRepo _mockRepo;

        /// <summary>
        /// Converts a database entity to a dto entity asynchronously
        /// </summary>
        /// <param name="user">The user to convert</param>
        /// <returns>the converted user</returns>
        public static async Task<DTOUser> DBOToDTOAsync(User userModel)
        {
            var user = new DTOUser();

            user.Id = userModel.Id;
            user.Email = userModel.Email;
            user.Address = userModel.Address;
            user.Name = userModel.Name;
            user.Password = userModel.Password;
            user.UserName = userModel.UserName;
            user.Gender = userModel.Gender;
            user.Note = userModel.Note;

            return user;
        }

        /// <summary>
        /// Converts a database entity to a dto entity
        /// </summary>
        /// <param name="user">The user to convert</param>
        /// <returns>the converted user</returns>
        public static DTOUser DBOToDTO(User userModel)
        {
            var user = new DTOUser();

            user.Id = userModel.Id;
            user.Email = userModel.Email;
            user.Address = userModel.Address;
            user.Name = userModel.Name;
            user.Password = userModel.Password;
            user.UserName = userModel.UserName;
            user.Gender = userModel.Gender;
            user.Note = userModel.Note;

            return user;
        }

        /// <summary>
        /// Converts a database entity to a dto entity
        /// </summary>
        /// <param name="user">The user to convert</param>
        /// <returns>the converted user</returns>
        public static User DTOToDBO(DTOUser userModel)
        {
            var user = new User();

            user.Id = userModel.Id;
            user.Email = userModel.Email;
            user.Address = userModel.Address;
            user.Name = userModel.Name;
            user.Password = userModel.Password;
            user.UserName = userModel.UserName;
            user.Gender = userModel.Gender;
            user.Note = userModel.Note;

            return user;
        }

        public static List<DTOUser> DBOTODTOList(IEnumerable<User> userModels)
        {
            List<DTOUser> result = new List<DTOUser>();
            foreach (var userModel in userModels)
            {
                var user = new DTOUser();

                user.Id = userModel.Id;
                user.Email = userModel.Email;
                user.Address = userModel.Address;
                user.Name = userModel.Name;
                user.Password = userModel.Password;
                user.UserName = userModel.UserName;
                user.Gender = userModel.Gender;
                user.Note = userModel.Note;

                result.Add(user);
            }

            return result;
        }

        /// <summary>
        /// Constructor for the user CRUD unit tests
        /// </summary>
        public UserTests()
        {
            IList<User> usersMockList = new List<User>
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

            // Mocks the function Get()
            mockRepo.Setup(user => user.Get("")).ReturnsAsync(DBOTODTOList(usersMockList));

            // Mocks the function Insert()
            mockRepo.Setup(users => users.Insert(It.IsAny<DTOUser>())).ReturnsAsync((DTOUser userModel) =>
            {
                var max = Math.Max(usersMockList.Max(c => c.Id) + 1, usersMockList.Max(c => c.Id));
                var user = DTOToDBO(userModel);
                user.Id = max;
                var users = _mockRepo.Get();
                if (users.Result.Count(c => c.Email == userModel.Email || c.UserName == userModel.UserName) != 0)
                    return null;
                usersMockList.Add(user);
                return DBOToDTO(user);
            });

            // Mocks the function Update()
            mockRepo.Setup(users => users.Update(It.IsAny<DTOUser>())).ReturnsAsync((DTOUser userModel) =>
            {
                var user = usersMockList.Single(c => c.Id == userModel.Id);
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
            mockRepo.Setup(users => users.Delete(It.IsAny<int>())).ReturnsAsync((int i) =>
            {
                var user = usersMockList.ToList().Single(c => c.Id == i);
                if (user != null)
                {
                    usersMockList.ToList().Remove(user);
                    return true;
                }

                return false;
            });

            // Mo0cks the function GetById()
            mockRepo.Setup(users => users.GetById(It.IsAny<int>()))
                .Returns((int i) => DBOToDTOAsync(usersMockList.Single(c => c.Id == i)));

            // Mocks the function Count()
            mockRepo.Setup(users => users.Count()).ReturnsAsync(usersMockList.Count());

            // Mocks the function GetUserByEmail()
            mockRepo.Setup(users => users.GetUserByEmail(It.IsAny<string>())).Returns((string email) =>
                DBOToDTO(usersMockList.Single(c => c.Email == email)));

            // Mocks the function GetUserByUserName()
            mockRepo.Setup(users => users.GetUserByUserName(It.IsAny<string>())).Returns((string userName) =>
                DBOToDTO(usersMockList.Single(c => c.Email == userName)));
            ;


        }

        /// <summary>
        /// Test for counting elements of a repo
        /// </summary>
        [TestMethod]
        public void TestCount()
        {
            var count = this._mockRepo.Count();
            Assert.AreEqual(7, count);
        }

        /// <summary>
        /// Test to get elements of a repo
        /// </summary>
        [TestMethod]
        public void TestGet()
        {
            var users = _mockRepo.Get();
            Assert.AreEqual(7, users.Result.Count());
        }

        /// <summary>
        /// Test to get element with its id
        /// </summary>
        [TestMethod]
        public void TestGetId()
        {
            var user = _mockRepo.GetById(4);
            var userExpect = new DTOUser()
            {
                Id = 4,
                UserName = "test4",
                Email = "test5@valid.fr",
                Password = "pswd4",
                Name = "Test 4",
                Address = "Test 4\nNew York",
                Gender = "woman",
                Note = 10
            };
            Assert.AreEqual(user, userExpect);
        }

        /// <summary>
        /// Test to get wrong id
        /// </summary>
        [TestMethod]
        public void TestGetIdWrong()
        {
            var user = _mockRepo.GetById(10);
            Assert.IsNull(user.Result);
        }

        /// <summary>
        /// Test to insert element
        /// </summary>
        [TestMethod]
        public void TestInsertOk()
        {
            var user8 = new DTOUser()
            {
                Id = 8,
                UserName = "test8",
                Email = "test8@valid.fr",
                Password = "pswd8",
                Name = "Test 8",
                Address = "Test 8\nNew York",
                Gender = "woman",
                Note = 10
            };

            var result = _mockRepo.Insert(user8);
            Assert.IsNotNull(result);
            Assert.AreEqual(user8, result.Result);
            var count = this._mockRepo.Count();
            Assert.AreEqual(8, count);
        }

        /// <summary>
        /// Test to wrong email insert 
        /// </summary>
        [TestMethod]
        public void TestInsertWrongEmail()
        {
            var user8 = new DTOUser()
            {
                Id = 9,
                UserName = "test9",
                Email = "test8@valid.fr",
                Password = "pswd9",
                Name = "Test 9",
                Address = "Test 9\nNew York",
                Gender = "woman",
                Note = 10
            };
            _mockRepo.Insert(user8);
            var count = this._mockRepo.Count();
            Assert.AreEqual(8, count.Result);
        }

        /// <summary>
        /// Test to wrong userName insert
        /// </summary>
        [TestMethod]
        public void TestInsertWrongUserName()
        {
            var user9 = new DTOUser()
            {
                Id = 9,
                UserName = "test8",
                Email = "test9@valid.fr",
                Password = "pswd9",
                Name = "Test 9",
                Address = "Test 9\nNew York",
                Gender = "woman",
                Note = 10
            };
            _mockRepo.Insert(user9);
            var count = this._mockRepo.Count();
            Assert.AreEqual(8, count);
        }

        /// <summary>
        /// Test to update element
        /// </summary>
        [TestMethod]
        public void TestUpdateOk()
        {
            var updateUser = new DTOUser
            {
                Id = 1, UserName = "toto", Email = "toto@valid.fr", Password = "password", Name = "Test 1 Updated",
                Address = "Test 1\nNew York City", Gender = "woman", Note = 10
            };
            var result = _mockRepo.Update(updateUser);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(updateUser, _mockRepo.GetById(updateUser.Id).Result);
        }

        /// <summary>
        /// Wrong ID testing update
        /// </summary>
        [TestMethod]
        public void TestUpdateWrong()
        {
            var updateUser = new DTOUser
            {
                Id = 15,
                UserName = "toto",
                Email = "toto@valid.fr",
                Password = "password",
                Name = "Test 1 Updated",
                Address = "Test 1\nNew York City",
                Gender = "woman",
                Note = 10
            };
            var result = _mockRepo.Update(updateUser);
            Assert.IsNull(result);
            Assert.AreEqual(8, _mockRepo.Count().Result);
        }

        /// <summary>
        /// Delete element
        /// </summary>
        [TestMethod]
        public void TestDeleteOk()
        {
            _mockRepo.Delete(8);
            var search = _mockRepo.GetById(8);
            Assert.IsNull(search);
            var count = this._mockRepo.Count();
            Assert.AreEqual(7, count);
        }

        /// <summary>
        /// Wrong id to delete
        /// </summary>
        [TestMethod]
        public void TestDeleteWrong()
        {
            var result = _mockRepo.Delete(8);
            Assert.IsFalse(result.Result);
            var count = this._mockRepo.Count();
            Assert.AreEqual(7, count);
        }

        /// <summary>
        /// Get user function of its email
        /// </summary>
        [TestMethod]
        public void TestGetUserEmailOk()
        {
            var userTheorical = new DTOUser
            {
                Id = 2,
                UserName = "test2",
                Email = "test3@valid.fr",
                Password = "pswd2",
                Name = "Test 2",
                Address = "Test 2\nParis",
                Gender = "woman",
                Note = 10
            };
            var user = _mockRepo.GetUserByEmail(userTheorical.Email);
            Assert.IsNotNull(user);
            Assert.AreEqual(userTheorical, user);
        }

        /// <summary>
        /// Get user function of its email wrong email
        /// </summary>
        [TestMethod]
        public void TestGetUserEmailWrong()
        {
            var user = _mockRepo.GetUserByEmail("test666@valid.fr");
            Assert.IsNull(user);
        }

        /// <summary>
        /// Get user function of its username
        /// </summary>
        [TestMethod]
        public void TestGetUserUserNameOk()
        {
            var userTheorical = new DTOUser
            {
                Id = 1,
                UserName = "toto",
                Email = "toto@valid.fr",
                Password = "password",
                Name = "Test 1 Updated",
                Address = "Test 1\nNew York City",
                Gender = "woman",
                Note = 10
            };
            var user = _mockRepo.GetUserByUserName(userTheorical.UserName);
            Assert.IsNotNull(user);
            Assert.AreEqual(userTheorical, user);
        }

        /// <summary>
        /// Get user function of its email wrong username
        /// </summary>
        [TestMethod]
        public void TestGetUserUserNameWrong()
        {
            var user = _mockRepo.GetUserByEmail("unknown_username");
            Assert.IsNull(user);
        }
    }
}