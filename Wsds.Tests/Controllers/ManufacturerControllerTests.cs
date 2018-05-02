using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class ManufacturerControllerTests
    {
        private Mock<IManufacturerRepository> _mockIManufacturerRepository;
        private ManufacturerController _manufacturerController;

        public ManufacturerControllerTests()
        {
            _mockIManufacturerRepository = new Mock<IManufacturerRepository>();
            _manufacturerController = new ManufacturerController(_mockIManufacturerRepository.Object);
        }

        [Fact]
        public void ManufacturerControllerCreated()
        {
            Assert.NotNull(_manufacturerController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Manufacturer_DTO>() { new Manufacturer_DTO()};
            _mockIManufacturerRepository.Setup(x => x.Manufacturers).Returns(list);

            var result = _manufacturerController.Get();
            Assert.IsType<OkObjectResult>(result);

            _mockIManufacturerRepository.Verify(x => x.Manufacturers, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetByIdTest(long id)
        {
            var manufacturer = new Manufacturer_DTO();
            _mockIManufacturerRepository.Setup(x => x.Manufacturer(It.IsAny<long>())).Returns(manufacturer);

            var result = _manufacturerController.Get(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIManufacturerRepository.Verify(x => x.Manufacturer(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetBrandByCategoryIdTest(long categoryId)
        {
            var result = _manufacturerController.GetBrandByCategoryId(categoryId);

            Assert.Null(result);
        }
    }
}
