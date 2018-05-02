using Xunit;
using Moq;
using System.Collections.Generic;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class PageControllerTests
    {
        private Mock<IPageRepository> _mockIPageRepository;
        private PageController _pageController;

        public PageControllerTests()
        {
            _mockIPageRepository = new Mock<IPageRepository>();
            _pageController = new PageController(_mockIPageRepository.Object);
        }

        [Fact]
        public void PageControllerCreated()
        {
            Assert.NotNull(_pageController);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetPageByIdTest(long id)
        {
            var page = new Page_DTO();
            _mockIPageRepository.Setup(x => x.GetPageById(It.IsAny<long>())).Returns(page);

            var result = _pageController.GetPageById(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIPageRepository.Verify(x => x.GetPageById(id), Times.Once);
        }
    }
}
