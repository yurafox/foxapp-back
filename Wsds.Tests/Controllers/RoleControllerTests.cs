using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wsds.WebApp.Models;

namespace Wsds.Tests.Controllers
{
    public class RoleControllerTests
    {
        private Mock<IRoleRepository> _mockIRoleRepository;
        private RoleController _roleController;

        public RoleControllerTests()
        {
            _mockIRoleRepository = new Mock<IRoleRepository>();
            _roleController = new RoleController(_mockIRoleRepository.Object);
        }

        [Fact]
        public void RoleControllerCreated()
        {
            Assert.NotNull(_roleController);
        }

        [Fact]
        public void AllRolesTest()
        {
            var role = new IdentityRole();
            var list = new List<IdentityRole>() { role};
            _mockIRoleRepository.Setup(x => x.AllRoles(It.IsAny<Func<IdentityRole, bool>>())).Returns(list);

            var result = _roleController.AllRoles();
            Assert.IsType<OkObjectResult>(result);
        }


        [Theory]
        [InlineData("admin")]
        [InlineData("user")]
        public void CreateRoleSuccessTest(string roleName)
        {
            _mockIRoleRepository.Setup(x => x.CreateRole(It.IsAny<string>())).Returns(CreateRole(roleName));
            var result = _roleController.CreateRole(roleName);

            Assert.IsType<Task<IActionResult>>(result);

            Task<IActionResult> task = _roleController.CreateRole(roleName);
            Assert.NotNull(task);

            var taskIsCompleted = task.IsCompleted;
            Assert.Equal(true, taskIsCompleted);

            _mockIRoleRepository.Verify(x => x.CreateRole(roleName), Times.AtLeastOnce);
        }

        [Fact]
        public void CreateRoleFailTest()
        {
            _mockIRoleRepository.Setup(x => x.CreateRole(It.IsAny<string>())).Returns(CreateRole(""));
            var result = _roleController.CreateRole(null);

            Assert.IsType<Task<IActionResult>>(result);
            Assert.Equal(true,result.IsCompleted);
        }

        [Theory]
        [InlineData("10")]
        [InlineData("11")]
        public void DeleteRoleSuccessTest(string id)
        {
            _mockIRoleRepository.Setup(x => x.DeleteRole(It.IsAny<string>())).Returns(DeleteRole(id));
            var result = _roleController.DeleteRole(id);

            Assert.IsType<Task<IActionResult>>(result);

            Task<IActionResult> task = _roleController.DeleteRole(id);
            Assert.NotNull(task);

            var taskIsCompleted = task.IsCompleted;
            Assert.Equal(true, taskIsCompleted);

            _mockIRoleRepository.Verify(x => x.DeleteRole(id), Times.AtLeastOnce);
        }

        [Fact]
        public void DeleteRoleFailTest()
        {
            _mockIRoleRepository.Setup(x => x.DeleteRole(It.IsAny<string>())).Returns(DeleteRole(""));
            var result = _roleController.DeleteRole(null);

            Assert.IsType<Task<IActionResult>>(result);
            Assert.Equal(true, result.IsCompleted);
        }

        [Fact]
        public void UpdateRoleSuccessTest()
        {
            var roleModel = new RoleShortModel() { Id="1",Name="name" };
            _mockIRoleRepository.Setup(x => x.EditRole(It.IsAny<string>(), It.IsAny<string>())).Returns(EditRole(roleModel.Id,roleModel.Name));

            var result = _roleController.UpdateRole(roleModel);

            Assert.IsType<Task<IActionResult>>(result);

            Task<IActionResult> task = _roleController.UpdateRole(roleModel);
            Assert.NotNull(task);

            var taskIsCompleted = task.IsCompleted;
            Assert.Equal(true, taskIsCompleted);

            _mockIRoleRepository.Verify(x => x.EditRole(roleModel.Id,roleModel.Name), Times.AtLeastOnce);
        }

        [Fact]
        public void UpdateRoleFailTest()
        {
            RoleShortModel model = null;
            _roleController.ModelState.AddModelError("Id", "Required");

            var result = _roleController.UpdateRole(model);

            Assert.IsType<Task<IActionResult>>(result);
            Assert.Equal(true, result.IsCompleted);
        }

        private Task<IdentityRole> CreateRole(string roleName)
        {
            return Task.FromResult<IdentityRole>(new IdentityRole(roleName));
        }

        private Task<BadRequestObjectResult> CreateRoleFailTest(string roleName)
        {
            return Task.FromResult<BadRequestObjectResult>(new BadRequestObjectResult("error"));
        }

        private Task<IdentityRole> DeleteRole(string id)
        {
            return Task.FromResult<IdentityRole>(new IdentityRole(id));
        }

        private Task<IdentityRole> EditRole(string id, string name)
        {
            return Task.FromResult<IdentityRole>(new IdentityRole(id));
        }
    }
}
