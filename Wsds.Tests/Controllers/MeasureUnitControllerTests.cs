using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class MeasureUnitControllerTests
    {
        private Mock<IMeasureUnitRepository> _mockIMeasureUnitRepository;
        private MeasureUnitController _measureUnitController;

        public MeasureUnitControllerTests()
        {
            _mockIMeasureUnitRepository = new Mock<IMeasureUnitRepository>();
            _measureUnitController = new MeasureUnitController(_mockIMeasureUnitRepository.Object);
        }

        [Fact]
        public void MeasureUnitControllerCreated()
        {
            Assert.NotNull(_measureUnitController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Measure_Unit_DTO>() { new Measure_Unit_DTO()};
            _mockIMeasureUnitRepository.Setup(x => x.MeasureUnits).Returns(list);

            var result = _measureUnitController.Get();
            Assert.IsType<OkObjectResult>(result);

            _mockIMeasureUnitRepository.Verify(x => x.MeasureUnits, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetById(long id)
        {
            var measureUnit = new Measure_Unit_DTO();
            _mockIMeasureUnitRepository.Setup(x => x.MeasureUnit(It.IsAny<long>())).Returns(measureUnit);

            var result = _measureUnitController.Get(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIMeasureUnitRepository.Verify(x => x.MeasureUnit(id), Times.Once);
        }
    }
}
