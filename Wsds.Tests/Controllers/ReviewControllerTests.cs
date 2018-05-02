using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class ReviewControllerTests
    {
        private Mock<IReviewRepository> _mockIReviewRepository;
        private ReviewController _reviewController;

        public ReviewControllerTests()
        {
            _mockIReviewRepository = new Mock<IReviewRepository>();
            _reviewController = new ReviewController(_mockIReviewRepository.Object);
        }

        [Fact]
        public void ReviewControllerCreated()
        {
            Assert.NotNull(_reviewController);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetProductReviewsTest(long id)
        {
            var list = new List<ProductReview_DTO>() { new ProductReview_DTO()};
            _mockIReviewRepository.Setup(x => x.GetProductReviews(It.IsAny<long>())).Returns(list);

            var result = _reviewController.GetProductReviews(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIReviewRepository.Verify(x => x.GetProductReviews(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetStoreReviewsByStoreIdTest(long id)
        {
            var list = new List<StoreReview_DTO>() { new StoreReview_DTO() };
            _mockIReviewRepository.Setup(x => x.GetStoreReviewsByStoreId(It.IsAny<long>())).Returns(list);

            var result = _reviewController.GetStoreReviewsByStoreId(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIReviewRepository.Verify(x => x.GetStoreReviewsByStoreId(id), Times.Once);
        }

        [Fact]
        public void GetStoreReviewsTest()
        {
            var list = new List<StoreReview_DTO>() { new StoreReview_DTO() };
            _mockIReviewRepository.Setup(x => x.GetStoreReviews()).Returns(list);

            var result = _reviewController.GetStoreReviews();
            Assert.IsType<OkObjectResult>(result);

            _mockIReviewRepository.Verify(x => x.GetStoreReviews(), Times.Once);
        }
    }
}
