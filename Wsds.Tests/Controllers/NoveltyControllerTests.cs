using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class NoveltyControllerTests
    {
        private Mock<INoveltyRepository> _mockINoveltyRepository;
        private NoveltyController _noveltyController;

        public NoveltyControllerTests()
        {
            _mockINoveltyRepository = new Mock<INoveltyRepository>();
            _noveltyController = new NoveltyController(_mockINoveltyRepository.Object);
        }

        [Fact]
        public void NoveltyControllerCreated()
        {
            Assert.NotNull(_noveltyController);
        }

        [Fact]
        public void GetNoveltiesTest()
        {
            var list = new List<Novelty_DTO>() { new Novelty_DTO() };
            _mockINoveltyRepository.Setup(x => x.GetNovelties()).Returns(list);

            var result = _noveltyController.GetNovelties();
            Assert.IsType<OkObjectResult>(result);

            _mockINoveltyRepository.Verify(x => x.GetNovelties(), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetNoveltyByIdTest(long id)
        {
            var novelty = new Novelty_DTO();
            _mockINoveltyRepository.Setup(x => x.GetNoveltyById(It.IsAny<long>())).Returns(novelty);

            var result = _noveltyController.GetNoveltyById(id);

            Assert.IsType<OkObjectResult>(result);
            _mockINoveltyRepository.Verify(x => x.GetNoveltyById(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetNoveltyDetailsByNoveltyIdTest(long id)
        {
            var list = new List<NoveltyDetails_DTO>() { new NoveltyDetails_DTO() };
            _mockINoveltyRepository.Setup(x => x.GetNoveltyDetailsByNoveltyId(It.IsAny<long>())).Returns(list);

            var result = _noveltyController.GetNoveltyDetailsByNoveltyId(id);
            Assert.IsType<OkObjectResult>(result);

            _mockINoveltyRepository.Verify(x => x.GetNoveltyDetailsByNoveltyId(id), Times.Once);
        }
    }
}
