using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class CurrencyControllerTests
    {
        private Mock<ICurrencyRepository> _mockICurrencyRepository;
        private CurrencyController _currencyController;

        public CurrencyControllerTests()
        {
            _mockICurrencyRepository = new Mock<ICurrencyRepository>();
            _currencyController = new CurrencyController(_mockICurrencyRepository.Object);
        }

        [Fact]
        public void CurrencyControllerCreated()
        {
            Assert.NotNull(_currencyController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Currency_DTO>() { new Currency_DTO()};
            _mockICurrencyRepository.Setup(x => x.Currencies).Returns(list);

            var result = _currencyController.Get();
            Assert.IsType<OkObjectResult>(result);

            _mockICurrencyRepository.Verify(x => x.Currencies, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetParamsTest(long id)
        {
            var currencyDto = new Currency_DTO();
            _mockICurrencyRepository.Setup(x => x.Currency(It.IsAny<long>())).Returns(currencyDto);

            var result = _currencyController.Get(id);
            Assert.IsType<OkObjectResult>(result);

            _mockICurrencyRepository.Verify(x => x.Currency(id), Times.Once);
        }

        [Fact]
        public void GetCurAscTest()
        {
            var result = _currencyController.GetCurAsc();
            Assert.Null(result);
        }
    }
}
