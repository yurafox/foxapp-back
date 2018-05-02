using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.Tests.Fixtures;
using Wsds.WebApp.Controllers;
using Xunit;

namespace Wsds.Tests.Controllers
{
    public class BannerSlideControllerTests
    {
        [Fact]
        public void BannerSlideControllerCreated()
        {
            var mockIBannerSlideRepository = Mock.Of<IBannerSlideRepository>();
            var bannerSlideController = new BannerSlideController(mockIBannerSlideRepository);

            Assert.NotNull(bannerSlideController);
        }

        [Fact]
        public void GetBannerSlidesType()
        {
            var mockIBannerSlideRepository = Mock.Of<IBannerSlideRepository>();
            var bannerSlideController = new BannerSlideController(mockIBannerSlideRepository);

            var result = bannerSlideController.GetBannerSlides();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
