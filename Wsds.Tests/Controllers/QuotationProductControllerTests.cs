using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Wsds.Tests.Controllers
{
    public class QuotationProductControllerTests
    {
        private Mock<IQuotationProductRepository> _mockIQuotationProductRepository;
        private QuotationProductController _quotationProductController;

        public QuotationProductControllerTests()
        {
           _mockIQuotationProductRepository = new Mock<IQuotationProductRepository>();
            _quotationProductController = new QuotationProductController(_mockIQuotationProductRepository.Object);
        }

        [Fact]
        public void QuotationProductControllerCreated()
        {
            Assert.NotNull(_quotationProductController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Quotation_Product_DTO>() { new Quotation_Product_DTO() };
            _mockIQuotationProductRepository.Setup(x => x.QuotationProducts).Returns(list);

            var result = _quotationProductController.Get();
            Assert.IsType<OkObjectResult>(result);

            _mockIQuotationProductRepository.Verify(x => x.QuotationProducts, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetByIdTest(long id)
        {
            var quotation = new Quotation_Product_DTO();
            _mockIQuotationProductRepository.Setup(x => x.QuotationProduct(It.IsAny<long>())).Returns(quotation);

            var result = _quotationProductController.Get(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIQuotationProductRepository.Verify(x => x.QuotationProduct(id), Times.Once);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("100")]
        public void GetByIdProductTest(string idProduct)
        {
            var list = new List<Quotation_Product_DTO>() { new Quotation_Product_DTO() };
            _mockIQuotationProductRepository.Setup(x => x.GetQuotProdsByProductID(It.IsAny<long>())).Returns(list);

            var result = _quotationProductController.Get(idProduct);
            Assert.IsType<OkObjectResult>(result);

            _mockIQuotationProductRepository.Verify(x => x.GetQuotProdsByProductID(Int64.Parse(idProduct)), Times.Once);
        }
    }
}
