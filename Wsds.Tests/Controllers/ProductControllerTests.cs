using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class ProductControllerTests
    {
        private Mock<IProductRepository> _mockIProductRepository;
        private ProductController _productController;

        public ProductControllerTests()
        {
            _mockIProductRepository = new Mock<IProductRepository>();
            _productController = new ProductController(_mockIProductRepository.Object);
        }

        [Fact]
        public void ProductControllerCreated()
        {
            Assert.NotNull(_productController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Product_DTO>() { new Product_DTO() };
            _mockIProductRepository.Setup(x => x.Products).Returns(list);

            var result = _productController.Get();
            Assert.IsType<OkObjectResult>(result);

            _mockIProductRepository.Verify(x => x.Products, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetByIdTest(long id)
        {
            var product = new Product_DTO();
            _mockIProductRepository.Setup(x => x.Product(It.IsAny<long>())).Returns(product);

            var result = _productController.Get(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIProductRepository.Verify(x => x.Product(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetByCategoryTest(long id)
        {
            var result = _productController.GetByCategory(id);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetProductDescriptionTest(long id)
        {
            var description = "description";
            _mockIProductRepository.Setup(x => x.GetProductDescription(It.IsAny<long>())).Returns(description);

            var result = _productController.GetProductDescription(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIProductRepository.Verify(x => x.GetProductDescription(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetProductImagesTest(long id)
        {
            var list = new List<string>() { "image1","image2","image3","image4"};
            _mockIProductRepository.Setup(x => x.GetProductImages(It.IsAny<long>())).Returns(list);

            var result = _productController.GetProductImages(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIProductRepository.Verify(x => x.GetProductImages(id), Times.Once);
        }

        [Theory]
        [InlineData("search string")]
        [InlineData("search string &Hee&YE*!(!@")]
        public void SearchProductsTest(string srch)
        {
            var list = new List<Product_DTO>() { new Product_DTO() };
            _mockIProductRepository.Setup(x => x.SearchProducts(It.IsAny<string>())).Returns(list);

            var result = _productController.SearchProducts(srch);
            Assert.IsType<OkObjectResult>(result);

            _mockIProductRepository.Verify(x => x.SearchProducts(srch), Times.Once);
        }
    }
}
