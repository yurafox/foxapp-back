using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class LocalizationControllerTest
    {
        private Mock<ILocalizationRepository> _mockILocalizationRepository;
        private LocalizationController _localizationController;

        public LocalizationControllerTest()
        {
            _mockILocalizationRepository = new Mock<ILocalizationRepository>();
            _localizationController = new LocalizationController(_mockILocalizationRepository.Object);
        }

        [Fact]
        public void LocalizationControllerCreated()
        {
            Assert.NotNull(_localizationController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Lang_DTO>() { new Lang_DTO()};
            _mockILocalizationRepository.Setup(x => x.Langs).Returns(list);

            var result = _localizationController.Get();
            Assert.IsType<OkObjectResult>(result);

            _mockILocalizationRepository.Verify(x => x.Langs, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetByIdTest(long id)
        {
            var lang = new Lang_DTO();
            _mockILocalizationRepository.Setup(x => x.Lang(It.IsAny<long>())).Returns(lang);

            var result = _localizationController.Get(id);
            Assert.IsType<OkObjectResult>(result);

            _mockILocalizationRepository.Verify(x => x.Lang(id), Times.Once);
        }
    }
}
