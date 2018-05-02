using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Wsds.DAL.Entities.DTO;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Xunit;

namespace Wsds.Tests.Controllers
{
    public class CatalogControllerTests
    {
        [Fact]
        public void CatalogControllerCreated()
        {
            var mockIProductGroupRepository = new Mock<IProductGroupRepository>();
            var catalogController = new CatalogController(mockIProductGroupRepository.Object);

            Assert.NotNull(catalogController);
        }

        [Fact]
        public void SuccessGetTest()
        {
            var mockIProductGroupRepository = new Mock<IProductGroupRepository>();
            var catalogController = new CatalogController(mockIProductGroupRepository.Object);

            mockIProductGroupRepository.Setup(x=>x.ProductGroups).Returns(GetProductGroups());
            var result = catalogController.Get();

            Assert.IsType<OkObjectResult>(result);

            mockIProductGroupRepository.Verify(x => x.ProductGroups, Times.Once);
        }

        [Fact]
        public void FailGetTest()
        {
            var mockIProductGroupRepository = new Mock<IProductGroupRepository>();
            var catalogController = new CatalogController(mockIProductGroupRepository.Object);

            mockIProductGroupRepository.Setup(x => x.ProductGroups).Returns(GetEmptyCollaction());

            var result = catalogController.Get();

            Assert.Equal("category list is empty", ((BadRequestObjectResult)result).Value);

            Assert.IsType<BadRequestObjectResult>(result);

            mockIProductGroupRepository.Verify(x => x.ProductGroups, Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-700)]
        public void FailGetByIdTest(int value)
        {
            var mockIProductGroupRepository = new Mock<IProductGroupRepository>();
            var catalogController = new CatalogController(mockIProductGroupRepository.Object);

            mockIProductGroupRepository.Setup(x => x.GetProductGroupById(value)).Returns(GetProductGroupById(value));

            var result = catalogController.Get(value);

            Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"can not find category by id={value}", ((BadRequestObjectResult)result).Value);

            mockIProductGroupRepository.Verify(x => x.GetProductGroupById(value), Times.Once);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(700)]
        public void SuccessGetByIdTest(int value)
        {
            var mockIProductGroupRepository = new Mock<IProductGroupRepository>();
            var catalogController = new CatalogController(mockIProductGroupRepository.Object);

            mockIProductGroupRepository.Setup(x => x.GetProductGroupById(value)).Returns(GetProductGroupById(value));

            var result = catalogController.Get(value);

            Assert.IsType<OkObjectResult>(result);

            mockIProductGroupRepository.Verify(x => x.GetProductGroupById(value), Times.Once);

        }

        private Product_Groups_DTO GetProductGroupById(long _id)
        {
            if (_id > 0)
                return new Product_Groups_DTO() { id = _id};

            return null;
        }

        private IEnumerable<Product_Groups_DTO> GetProductGroups()
        {
            return new List<Product_Groups_DTO>()
            {
                new Product_Groups_DTO(){ id=1},
                new Product_Groups_DTO(){ id=2}
            };
        }

        private IEnumerable<Product_Groups_DTO> GetEmptyCollaction()
        {
            return new List<Product_Groups_DTO>();
        }
    }
}
