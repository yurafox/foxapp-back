using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities.DTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class StockControllerTests
    {
        private Mock<IActionRepository> _mockIActionRepository;
        private StockController _stockController;

        public StockControllerTests()
        {
            _mockIActionRepository = new Mock<IActionRepository>();
            _stockController = new StockController(_mockIActionRepository.Object);
        }

        [Fact]
        public void StockControllerCreated()
        {
            Assert.NotNull(_stockController);
        }

        [Fact]
        public void GetActionTests()
        {
            var list = new List<Action_DTO>() { new Action_DTO() };
            _mockIActionRepository.Setup(x => x.GetActions()).Returns(list);

            var result = _stockController.GetActions();
            Assert.IsType<OkObjectResult>(result);

            _mockIActionRepository.Verify(x => x.GetActions(), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetActionById(long id)
        {
            var action = new Action_DTO();
            _mockIActionRepository.Setup(x => x.GetActionById(It.IsAny<long>())).Returns(action);

            var result = _stockController.GetActionById(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIActionRepository.Verify(x => x.GetActionById(id), Times.Once);
        }
    }
}