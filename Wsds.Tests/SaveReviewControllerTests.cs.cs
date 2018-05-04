using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wsds.WebApp.Auth;
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

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };
            var storeReview = new StoreReview_DTO() { id = 1, idClient = 1, user = "user" };
            var client = new Client_DTO() { id = 1 };

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);
            _mockIReviewRepository.Setup(x => x.SaveStoreReview(It.IsAny<StoreReview_DTO>(), It.IsAny<Client_DTO>())).Returns(storeReview);
            _saveReviewController.ControllerContext.HttpContext.Items["token"] = token;


            var result = _saveReviewController.SaveStoreReview(storeReview);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.NotNull(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIReviewRepository.Verify(x => x.SaveStoreReview(It.IsAny<StoreReview_DTO>(), It.IsAny<Client_DTO>()), Times.Once);
        }

        [Fact]
        public void SaveStoreReviewBadRequestTest()
        {
            _saveReviewController.ControllerContext = new ControllerContext();
            _saveReviewController.ControllerContext.HttpContext = new DefaultHttpContext();

            var storeReview = new StoreReview_DTO() { id = 1, idClient = 1, user = "user" };
            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };

            var result = _saveReviewController.SaveStoreReview(storeReview);

            Assert.IsType<BadRequestResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Never);
            _mockIReviewRepository.Verify(x => x.SaveStoreReview(It.IsAny<StoreReview_DTO>(), It.IsAny<Client_DTO>()), Times.Never);
        }

        [Fact]
        public void SaveProductReviewTest()
        {
            var client = new Client_DTO() { id = 1 };
            var product = new ProductReview_DTO() { id = 1 };

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);
            _mockIReviewRepository.Setup(x => x.SaveProductReview(It.IsAny<ProductReview_DTO>(), It.IsAny<Client_DTO>())).Returns(product);

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };
            _saveReviewController.ControllerContext = new ControllerContext();
            _saveReviewController.ControllerContext.HttpContext = new DefaultHttpContext();
            _saveReviewController.ControllerContext.HttpContext.Items["token"] = token;

            var result = _saveReviewController.SaveProductReview(product);
            Assert.IsType<CreatedAtRouteResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIReviewRepository.Verify(x => x.SaveProductReview(It.IsAny<ProductReview_DTO>(), It.IsAny<Client_DTO>()), Times.Once);
        }

        [Fact]
        public void SaveProductReviewBadRequestTest()
        {
            _saveReviewController.ControllerContext = new ControllerContext();
            _saveReviewController.ControllerContext.HttpContext = new DefaultHttpContext();

            var product = new ProductReview_DTO() { id = 1 };
            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };

            var result = _saveReviewController.SaveProductReview(product);

            Assert.IsType<BadRequestResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Never);
            _mockIReviewRepository.Verify(x => x.SaveProductReview(It.IsAny<ProductReview_DTO>(), It.IsAny<Client_DTO>()), Times.Never);
        }
    }
}