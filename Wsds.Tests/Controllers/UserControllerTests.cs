using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Wsds.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Wsds.Tests.Controllers
{
    public class UserControllerTests
    {
        private Mock<IUserRepository> _mockIUserRepository;
        private Mock<IRoleRepository> _mockIRoleRepository;
        private UserController _userController;

        public UserControllerTests()
        {
            _mockIUserRepository = new Mock<IUserRepository>();
            _mockIRoleRepository = new Mock<IRoleRepository>();
            _userController = new UserController(_mockIUserRepository.Object, _mockIRoleRepository.Object);
        }

        [Fact]
        public void DeviceDataControllerCreated()
        {
            Assert.NotNull(_userController);
        }

        [Fact]
        public void AllUsersTest()
        {
            var list = new List<AppUser>() { new AppUser() };
            _mockIUserRepository.Setup(x => x.Users).Returns(list);

            var result = _userController.AllUsers();
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.Users, Times.Once);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("100")]
        public void GetUserByIdTest(string id)
        {
            var appUser = new AppUser();
            _mockIUserRepository.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(Task.FromResult(appUser));

            var result = _userController.GetUserById(id);

            Assert.IsType<Task<IActionResult>>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.GetUserById(id), Times.Once);
        }

        [Theory]
        [InlineData("login 1")]
        [InlineData("login 2")]
        public void GetUserByUserNameTest(string login)
        {
            var appUser = new AppUser();
            _mockIUserRepository.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(Task.FromResult(appUser));

            var result = _userController.GetUserByUserName(login);

            Assert.IsType<Task<IActionResult>>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.GetUserByName(login), Times.Once);
        }

        [Theory]
        [InlineData("email@gmail.com2")]
        [InlineData("email@gmail.com100")]
        public void GetUserByEmailTest(string email)
        {
            var appUser = new AppUser();
            _mockIUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult(appUser));

            var result = _userController.GetUserByEmail(email);

            Assert.IsType<Task<IActionResult>>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.GetUserByEmail(email), Times.Once);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("100")]
        public void GetRolesByUserTest(string id)
        {
            var list = new List<string>() { "1", "100" };

            var listOfRoles = new List<IdentityRole>() {
                new IdentityRole("role1") { Id = "1", Name = "role1"},
                new IdentityRole("role2") { Id = "2", Name = "role2"}
            };

            var listOfRoleShortModel = new List<RoleShortModel>() { new RoleShortModel() { Id = "1", Name = "role1" }, new RoleShortModel() { Id = "2", Name = "role2" } };

            _mockIUserRepository.Setup(x => x.UserRoles(It.IsAny<string>())).Returns(list);
            _mockIRoleRepository.Setup(x => x.AllRoles(It.IsAny<Func<IdentityRole, bool>>())).Returns(listOfRoles);

            var result = _userController.GetRolesByUser(id);

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.UserRoles(id), Times.Once);
            _mockIRoleRepository.Verify(x => x.AllRoles(It.IsAny<Func<IdentityRole, bool>>()), Times.Once);
        }

        [Fact]
        public void GetRolesByUserBadRequestTest()
        {
            string id = ""; List<string> a = new List<string>() { "", "", "" };

            var result = _userController.GetRolesByUser(id);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal("id is empty", ((BadRequestObjectResult)result).Value);

            _mockIUserRepository.Verify(x => x.UserRoles(id), Times.Never);
            _mockIRoleRepository.Verify(x => x.AllRoles(It.IsAny<Func<IdentityRole, bool>>()), Times.Never);
        }

        [Fact]
        public void CreateUserTest()
        {
            RegisterModel auth = new RegisterModel() {Id="1",Email="12345@gmail.com2",Phone="380990000001",Password="password"};
            var appUser = new AppUser() { UserName = auth.Phone.ToLower(), Email = auth.Email.ToLower() };
            _mockIUserRepository.Setup(x => x.CreateUser(It.IsAny<AppUser>(), It.IsAny<string>())).Returns(Task.FromResult(appUser));

            var result =_userController.CreateUser(auth);

            Assert.IsType<Task<IActionResult>>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.CreateUser(It.IsAny<AppUser>(),It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CreateUserBadRequestTest()
        {
            RegisterModel auth = new RegisterModel() { Id = "1", Phone = "380990000001", Password = "password" };
            _userController.ModelState.AddModelError("Email", "Required");

            var result =_userController.CreateUser(auth);
            var taskResult = result.Result;

            Assert.IsType<Task<IActionResult>>(result);
            Assert.Equal("Model is not valid", ((BadRequestObjectResult)taskResult).Value);
        }

        [Theory]
        [InlineData("1", new string[] { "role one", "role two", "role three" })]
        public void EditRolesForUserTest(string id, string[] roles)
        {
            var appUser = new AppUser() { Id = "1", Email = "12345@gmail.com2" };
            var listOfRoles = new List<string>() { roles[0], roles[1], roles[2]};
            var identityResult = new Microsoft.AspNetCore.Identity.IdentityResult();
            
            _mockIUserRepository.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(Task.FromResult(appUser));
            _mockIUserRepository.Setup(x => x.UserRoles(It.IsAny<string>())).Returns(listOfRoles);

            var result = _userController.EditRolesForUser(id, roles);
            Assert.IsType<Task<IActionResult>>(result);
            Assert.NotNull(result);

            var mock = new Mock<UserManager<AppUser>>();
            mock.Setup(x => x.RemoveFromRolesAsync(It.IsAny<AppUser>(), It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(identityResult));
            //_mockIUserRepository.Setup(x => x.UserEngine.RemoveFromRolesAsync(It.IsAny<AppUser>(), It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(new IdentityResult()));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("100")]
        public void DeleteUserTest(string id)
        {
            var appUser = new AppUser() { Id = "1", Email = "12345@gmail.com2" };
            _mockIUserRepository.Setup(x => x.DeleteUser(It.IsAny<string>())).Returns(Task.FromResult(appUser));

            var result = _userController.DeleteUser(id);
            Assert.IsType<Task<IActionResult>>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.DeleteUser(id), Times.Once);
        }

        [Fact]
        public void DeleteUserBadRequestTest()
        {
            string id = "";

            var taskResult = _userController.DeleteUser(id);
            var result = taskResult.Result;

            Assert.IsType<Task<IActionResult>>(taskResult);
            Assert.NotNull(result);
            Assert.Equal("current id is not valid", ((BadRequestObjectResult)result).Value);
        }

        [Fact]
        public void UpdateUserTest()
        {
            var appUser = new AppUser();
            var registerModel = new RegisterModel();
            _mockIUserRepository.Setup(x => x.UpdateUser(It.IsAny<AppUser>())).Returns(Task.FromResult(appUser));

            var result = _userController.UpdateUser(registerModel);
            Assert.IsType<Task<IActionResult>>(result);
            Assert.NotNull(result);

            _mockIUserRepository.Verify(x => x.UpdateUser(It.IsAny<AppUser>()), Times.Once);
        }

        [Fact]
        public void UpdateUserBadRequestTest()
        {
            RegisterModel registerModel = new RegisterModel();
            _userController.ModelState.AddModelError("Email", "Required");

            var result =_userController.UpdateUser(registerModel);

            var taskResult = result.Result;

            Assert.IsType<Task<IActionResult>>(result);
            Assert.Equal("current model of new user is not valid", ((BadRequestObjectResult)result.Result).Value);
        }
    }
}
