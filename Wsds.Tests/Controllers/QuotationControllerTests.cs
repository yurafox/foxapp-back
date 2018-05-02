using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class QuotationControllerTests
    {
        private Mock<IQuotationRepository> _mockIQuotationRepository;
        private QuotationController _quotationController;

        public QuotationControllerTests()
        {
            _mockIQuotationRepository = new Mock<IQuotationRepository>();
            _quotationController = new QuotationController(_mockIQuotationRepository.Object);
        }

        [Fact]
        public void QuotationControllerCreated()
        {
            Assert.NotNull(_quotationController);
        }

        [Fact]
        public void GetQuotesTest()
        {
            var list = new List<Quotation_DTO>() { new Quotation_DTO() };
            _mockIQuotationRepository.Setup(x => x.Quotations).Returns(list);

            var result = _quotationController.GetQuotes();
            Assert.IsType<OkObjectResult>(result);

            _mockIQuotationRepository.Verify(x => x.Quotations, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetQouteByIdTest(long id)
        {
            var quotation = new Quotation_DTO();
            _mockIQuotationRepository.Setup(x => x.Quotation(It.IsAny<long>())).Returns(quotation);

            var result = _quotationController.GetQouteById(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIQuotationRepository.Verify(x => x.Quotation(id), Times.Once);
        }
    }
}
