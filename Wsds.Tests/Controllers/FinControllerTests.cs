using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class FinControllerTests
    {
        private Mock<IFinRepository> _mockIFinRepository;
        private FinController _finController;

        public FinControllerTests()
        {
            _mockIFinRepository = new Mock<IFinRepository>();
            _finController = new FinController(_mockIFinRepository.Object);
        }

        [Fact]
        public void FinControllerCreated()
        {
            Assert.NotNull(_finController);
        }

        [Fact]
        public void GetPmtMethods()
        {
            var list = new List<Enum_Pmt_Method_DTO>() { new Enum_Pmt_Method_DTO()};
            _mockIFinRepository.Setup(x => x.PaymentMethods).Returns(list);

            var result = _finController.GetPmtMethods();
            Assert.IsType<OkObjectResult>(result);

            _mockIFinRepository.Verify(x => x.PaymentMethods, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetPmtMethodByIdTest(long id)
        {
            var pmt = new Enum_Pmt_Method_DTO();
            _mockIFinRepository.Setup(x => x.PaymentMethod(It.IsAny<long>())).Returns(pmt);

            var result = _finController.GetPmtMethodById(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIFinRepository.Verify(x => x.PaymentMethod(id), Times.Once);
        }
    }
}
