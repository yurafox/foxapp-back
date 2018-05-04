using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wsds.WebApp.Auth;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Wsds.WebApp.WebExtensions;
using Wsds.DAL.Entities;

namespace Wsds.Tests.Controllers
{
    public class SaveReviewControllerTests
    {
        private Mock<IClientRepository> _mockIClientRepository;
        private Mock<IReviewRepository> _mockIReviewRepository;
        private SaveReviewController _saveReviewController;

        public SaveReviewControllerTests()
        {
            _mockIClientRepository = new Mock<IClientRepository>();
            _mockIReviewRepository = new Mock<IReviewRepository>();
            _saveReviewController = new SaveReviewController(_mockIClientRepository.Object, _mockIReviewRepository.Object);
        }

        [Fact]
        public void SaveReviewControllerCreated()
        {
            Assert.NotNull(_saveReviewController);
        }

        [Fact]
        public void SaveStoreReviewSuccessTest()
        {
            _saveReviewController.ControllerContext = new ControllerContext();
            _saveReviewController.ControllerContext.HttpContext = new DefaultHttpContext();

            var token = new TokenModel() { Phone="380120000000",Card=1,ClientId=1};
            var storeReview = new StoreReview_DTO() { id = 1, idClient = 1, user = "user" };
            var client = new Client_DTO() { id = 1};

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);
            _mockIReviewRepository.Setup(x => x.SaveStoreReview(It.IsAny<StoreReview_DTO>(), It.IsAny<Client_DTO>())).Returns(storeReview);
            _saveReviewController.ControllerContext.HttpContext.Items["token"] = token;
            
            
            var result = _saveReviewController.SaveStoreReview(storeReview);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.NotNull(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()),Times.Once);
            _mockIReviewRepository.Verify(x => x.SaveStoreReview(It.IsAny<StoreReview_DTO>(), It.IsAny<Client_DTO>()), Times.Once);    
        }

        [Fact]
        public void SaveStoreReviewBadRequestTest()
        {
            _saveReviewController.ControllerContext = new ControllerContext();
            _saveReviewController.ControllerContext.HttpContext = new DefaultHttpContext();

            var storeReview = new StoreReview_DTO() { id = 1, idClient = 1, user = "user" };

            var result = _saveReviewController.SaveStoreReview(storeReview);

            Assert.IsType<BadRequestResult>(result);    
        }
    } 
}
