using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class UpdateReviewControllerTests
    {
        private Mock<IClientRepository> _mockIClientRepository;
        private Mock<IReviewRepository> _mockIReviewRepository;
        private UpdateReviewController _updateReviewController;

        public UpdateReviewControllerTests()
        {
            _mockIClientRepository = new Mock<IClientRepository>();
            _mockIReviewRepository = new Mock<IReviewRepository>();
            _updateReviewController = new UpdateReviewController(_mockIClientRepository.Object, _mockIReviewRepository.Object);
        }

        [Fact]
        public void UpdateReviewControllerCreated()
        {
            Assert.NotNull(_updateReviewController);
        }

        [Fact]
        public void UpdateProductReviewSuccessTest()
        {
            var productReview = new ProductReview_DTO();
            _mockIReviewRepository.Setup(x => x.UpdateProductReview(It.IsAny<ProductReview_DTO>())).Returns(productReview);

            var result = _updateReviewController.UpdateProductReview(productReview);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.NotNull(result);

            _mockIReviewRepository.Verify(x => x.UpdateProductReview(It.IsAny<ProductReview_DTO>()), Times.Once);
        }

        [Fact]
        public void UpdateProductReviewFailTest()
        {
            ProductReview_DTO productReview = null;
            _mockIReviewRepository.Setup(x => x.UpdateProductReview(It.IsAny<ProductReview_DTO>())).Returns(productReview);

            var result = _updateReviewController.UpdateProductReview(productReview);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UpdateStoreReviewSuccessTest()
        {
            var storeReview = new StoreReview_DTO();
            _mockIReviewRepository.Setup(x => x.UpdateStoreReview(It.IsAny<StoreReview_DTO>())).Returns(storeReview);

            var result = _updateReviewController.UpdateStoreReview(storeReview);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.NotNull(result);

            _mockIReviewRepository.Verify(x => x.UpdateStoreReview(It.IsAny<StoreReview_DTO>()), Times.Once);
        }

        [Fact]
        public void UpdateStoreReviewFailTest()
        {
            StoreReview_DTO storeReview = null;
            _mockIReviewRepository.Setup(x => x.UpdateStoreReview(It.IsAny<StoreReview_DTO>())).Returns(storeReview);

            var result = _updateReviewController.UpdateStoreReview(storeReview);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
