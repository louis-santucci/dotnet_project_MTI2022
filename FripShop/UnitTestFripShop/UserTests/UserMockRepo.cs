using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFripShop.UserTests
{
    class UserMockRepo
    {
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

        /// <summary>
        /// Converts a database entity to a dto entity list
        /// </summary>
        /// <param name="userModels">The user list to convert</param>
        /// <returns>the converted user list</returns>
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

        public readonly List<User> _usersMockList = new List<User> 
        {
            new User
            {
                Id = 1, UserName = "test1", Email = "test1@valid.fr", Password = "pswd1", Name = "Test 1",
                Address = "Test 1\nNew York", Gender = "man", Note = 10
            },
            new User
            {
                Id = 2, UserName = "test2", Email = "test2@valid.fr", Password = "pswd2", Name = "Test 2",
                Address = "Test 2\nParis", Gender = "woman", Note = 10
            },
            new User
            {
                Id = 3,
                UserName = "test3",
                Email = "test3@valid.fr",
                Password = "pswd3",
                Name = "Test 3",
                Address = "Test 3\nMiami",
                Gender = "child",
                Note = 10
            },
            new User
            {
                Id = 4,
                UserName = "test4",
                Email = "test4@valid.fr",
                Password = "pswd4",
                Name = "Test 4",
                Address = "Test 4\nNew York",
                Gender = "woman",
                Note = 10
            },
            new User
            {
                Id = 5,
                UserName = "test5",
                Email = "test5@valid.fr",
                Password = "pswd5",
                Name = "Test 5",
                Address = "Test 5\nNew York",
                Gender = "man",
                Note = 10
            },
            new User
            {
                Id = 6,
                UserName = "test6",
                Email = "test6@valid.fr",
                Password = "pswd6",
                Name = "Test 6",
                Address = "Test 6\nNew York",
                Gender = "woman",
                Note = 10
            },
            new User
            {
                Id = 7,
                UserName = "test7",
                Email = "test7@valid.fr",
                Password = "pswd7",
                Name = "Test 7",
                Address = "Test 7\nNew York",
                Gender = "man",
                Note = 10
            }
        };

        public readonly IUserRepo _mockRepo;

        public UserMockRepo(List<User> users = null)
        {
            if (users != null)
                _usersMockList = users;

            var mockRepo = new Mock<IUserRepo>();

            // Mocks the function Get()
            mockRepo.Setup(userRepo => userRepo.Get("")).ReturnsAsync(DBOTODTOList(_usersMockList));

            // Mocks the function Insert()
            mockRepo.Setup(userRepo => userRepo.Insert(It.IsAny<DTOUser>())).ReturnsAsync((DTOUser userModel) =>
            {
                long max = 1;
                if (_usersMockList.Count != 0)
                    max = Math.Max(_usersMockList.Max(c => c.Id) + 1, _usersMockList.Max(c => c.Id));
                var user = DTOToDBO(userModel);
                user.Id = max;
                var users = _mockRepo.Get();
                if (users.Result.Count(c => c.Email == userModel.Email || c.UserName == userModel.UserName) != 0)
                    return null;
                this._usersMockList.Add(user);
                return DBOToDTO(user);
            });

            // Mocks the function Update()
            mockRepo.Setup(userRepo => userRepo.Update(It.IsAny<DTOUser>())).ReturnsAsync((DTOUser userModel) =>
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
            mockRepo.Setup(userRepo => userRepo.Delete(It.IsAny<long>())).ReturnsAsync((long i) =>
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
            mockRepo.Setup(userRepo => userRepo.GetById(It.IsAny<long>())).Returns((long i) =>
            {
                var count = _usersMockList.Count(c => c.Id == i);
                if (count != 1)
                {
                    return null;
                }
                return DBOToDTOAsync(_usersMockList.Single(c => c.Id == i));
            });

            // Mocks the function Count()
            mockRepo.Setup(userRepo => userRepo.Count()).ReturnsAsync(_usersMockList.Count());

            // Mocks the function GetUserByEmail()
            mockRepo.Setup(userRepo => userRepo.GetUserByEmail(It.IsAny<string>())).Returns((string email) =>
            {
                if (_usersMockList.Count(c => c.Email == email) == 0)
                {
                    return null;
                }

                return DBOToDTO(_usersMockList.Single(c => c.Email == email));
            });

            // Mocks the function GetUserByUserName()
            mockRepo.Setup(userRepo => userRepo.GetUserByUserName(It.IsAny<string>())).Returns((string userName) =>
            {
                if (_usersMockList.Count(c => c.UserName == userName) == 0)
                {
                    return null;
                }

                return DBOToDTO(_usersMockList.Single(c => c.UserName == userName));
            });

            this._mockRepo = mockRepo.Object;
        }
    }
}
