using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;

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

            //_mockIRoleRepository.Verify(x => x.AllRoles((role) => true),Times.Once);
        }
    }
}
