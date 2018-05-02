using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Xunit;

namespace Wsds.Tests.Controllers
{
    public class CreditControllerTests
    {
        private Mock<ICreditRepository> _mockICreditRepository;
        private CreditController _creditController;

        public CreditControllerTests()
        {
            _mockICreditRepository = new Mock<ICreditRepository>();
            _creditController = new CreditController(_mockICreditRepository.Object);
        }

        [Fact]
        public void CreditControllerCreated()
        {
            Assert.NotNull(_creditController);
        }

        [Fact]
        public void GetCreditProductsTest()
        {
            var list = new List<CreditProduct_DTO>() { new CreditProduct_DTO()};
            _mockICreditRepository.Setup(x => x.CreditProducts).Returns(list);

            var result = _creditController.GetCreditProducts();
            Assert.IsType<OkObjectResult>(result);

            _mockICreditRepository.Verify(x => x.CreditProducts, Times.Once);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(100,100)]
        [InlineData(200,500)]
        public void GetProductCreditSizeTest(long idProduct, long idSupplier)
        {
            //ProductSupplCreditGrade_DTO
            var list = new List<ProductSupplCreditGrade_DTO>() { new ProductSupplCreditGrade_DTO() };
            _mockICreditRepository.Setup(x => x.GetProductCreditSize(It.IsAny<long>(), It.IsAny<long>())).Returns(list);

            var result = _creditController.GetProductCreditSize(idProduct,idSupplier);
            Assert.IsType<OkObjectResult>(result);

            _mockICreditRepository.Verify(x => x.GetProductCreditSize(idProduct, idSupplier), Times.Once);

        }
    }
}
