using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Infrastructure.Facade;
using System;
using System.Net.Http;

namespace Wsds.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewResultNotNull()
        {
            //var appUser = new AppUser();
            //var fsAuthSender = new FSAuthSender();
            //var fsSmsService = new FSSmsService();

            //var fsUserRepository = new FSUserRepository(new UserManager<AppUser>,)
            //var AccountUserFacade = new AccountUserFacade();
            //var controller = new HomeController();

            // Arrange            
            var mock = new Mock<AccountUserFacade>(null, null, null);
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void IndexReturnsViewResult()
        {
            // Arrange
            var mock = new Mock<AccountUserFacade>(null, null, null);
            HomeController controller = new HomeController(mock.Object);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
