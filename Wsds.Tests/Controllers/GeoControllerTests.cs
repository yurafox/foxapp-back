using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Wsds.Tests.Controllers
{
    public class GeoControllerTests
    {
        private Mock<IGeoRepository> _mockIGeoRepository;
        private GeoController _geoController;

        public GeoControllerTests()
        {
            _mockIGeoRepository = new Mock<IGeoRepository>();
            _geoController = new GeoController(_mockIGeoRepository.Object);
        }

        [Fact]
        public void GeoControllerCreated()
        {
            Assert.NotNull(_geoController);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetCityTest(long id)
        {
            var city = new City_DTO();
            _mockIGeoRepository.Setup(x=>x.City(It.IsAny<long>())).Returns(city);

            var result = _geoController.GetCity(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.City(id), Times.Once);
        }

        [Fact]
        public void GetCitiesTest()
        {
            var list = new List<City_DTO>() { new City_DTO() };
            _mockIGeoRepository.Setup(x => x.Cities).Returns(list);

            var result = _geoController.GetCities();
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.Cities, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetCountryTest(long id)
        {
            var country = new Country_DTO();
            _mockIGeoRepository.Setup(x => x.Country(It.IsAny<long>())).Returns(country);

            var result = _geoController.GetCountry(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.Country(id), Times.Once);
        }

        [Fact]
        public void GetCountriesTest()
        {
            var list = new List<Country_DTO>() { new Country_DTO()};
            _mockIGeoRepository.Setup(x => x.Countries).Returns(list);

            var result = _geoController.GetCountries();
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.Countries, Times.Once);
        }

        [Fact]
        public void GetCitiesWithStoresTest()
        {
            var list = new List<City_DTO>() { new City_DTO()};
            _mockIGeoRepository.Setup(x => x.CitiesWithStores()).Returns(list);

            var result = _geoController.GetCitiesWithStores();
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.CitiesWithStores(), Times.Once);
        }

        [Theory]
        [InlineData("phrase")]
        [InlineData("phrase1!@")]
        [InlineData("phrase 2")]
        public void SearchCitiesTest(string search)
        {
            var list = new List<City_DTO>() { new City_DTO() };
            _mockIGeoRepository.Setup(x => x.SearchCities(It.IsAny<string>())).Returns(list);

            var result = _geoController.SearchCities(search);
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.SearchCities(search), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(1000)]
        public void GetRegionTest(long id)
        {
            var region = new Region_DTO();
            _mockIGeoRepository.Setup(x => x.Region(It.IsAny<long>())).Returns(region);

            var result = _geoController.GetRegion(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.Region(id), Times.Once);
        }

        [Fact]
        public void GetRegionsTest()
        {
            var list = new List<Region_DTO>() { new Region_DTO() };
            _mockIGeoRepository.Setup(x => x.Regions).Returns(list);

            var result = _geoController.GetRegions();
            Assert.IsType<OkObjectResult>(result);

            _mockIGeoRepository.Verify(x => x.Regions, Times.Once);
        }
    }
}
