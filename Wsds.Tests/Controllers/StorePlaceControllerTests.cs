using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Wsds.WebApp.Auth;

namespace Wsds.Tests.Controllers
{
    public class StorePlaceControllerTests
    {
        private Mock<IStorePlaceRepository> _mockIStorePlaceRepository;
        private Mock<IClientRepository> _mockIClientRepository;
        private StorePlaceController _storePlaceController;

        public StorePlaceControllerTests()
        {
            _mockIStorePlaceRepository = new Mock<IStorePlaceRepository>();
            _mockIClientRepository = new Mock<IClientRepository>();
            _storePlaceController = new StorePlaceController(_mockIClientRepository.Object, _mockIStorePlaceRepository.Object);
        }

        [Fact]
        public void StorePlaceControllerCreated()
        {
            Assert.NotNull(_storePlaceController);
        }

        [Fact]
        public void GetStoresTest()
        {
            var list = new List<Store_DTO>() { new Store_DTO() };
            _mockIStorePlaceRepository.Setup(x => x.Stores).Returns(list);

            var result = _storePlaceController.GetStores();
            Assert.IsType<OkObjectResult>(result);

            _mockIStorePlaceRepository.Verify(x => x.Stores, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetStoreTest(long id)
        {
            var store = new Store_DTO();
            _mockIStorePlaceRepository.Setup(x => x.GetStore(It.IsAny<long>())).Returns(store);

            var result = _storePlaceController.GetStore(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIStorePlaceRepository.Verify(x => x.GetStore(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetStorePlaceByIdTest(long id)
        {
            var storePlace = new StorePlace_DTO();
            _mockIStorePlaceRepository.Setup(x => x.StorePlace(It.IsAny<long>())).Returns(storePlace);

            var result = _storePlaceController.GetStorePlaceById(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIStorePlaceRepository.Verify(x => x.StorePlace(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetProductStorePlacesByQuotIdTest(long id)
        {
            var list = new List<ProductStorePlace_DTO>() { new ProductStorePlace_DTO() };
            _mockIStorePlaceRepository.Setup(x => x.GetProductSPByQuotId(It.IsAny<long>())).Returns(list);

            var result = _storePlaceController.GetProductStorePlacesByQuotId(id);
            Assert.IsType<OkObjectResult>(result);

            _mockIStorePlaceRepository.Verify(x => x.GetProductSPByQuotId(id), Times.Once);
        }

        [Fact]
        public void GetStorePlacesTest()
        {
            var list = new List<StorePlace_DTO>() { new StorePlace_DTO() };
            _mockIStorePlaceRepository.Setup(x => x.StorePlaces).Returns(list);

            var result = _storePlaceController.GetStorePlaces();
            Assert.IsType<OkObjectResult>(result);

            _mockIStorePlaceRepository.Verify(x => x.StorePlaces, Times.Once);
        }

        [Fact]
        public void GetFavoriteStoresBadRequestOneTest()
        {
            _storePlaceController.ControllerContext = new ControllerContext();
            _storePlaceController.ControllerContext.HttpContext = new DefaultHttpContext();

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };
            var client = new Client_DTO();
            _storePlaceController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);

            var result = _storePlaceController.GetFavoriteStores();
            Assert.IsType<BadRequestResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone("380120000000"), Times.Once);
            _mockIStorePlaceRepository.Verify(x => x.GetFavoriteStores(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void GetFavoriteStoresBadRequestTwoTest()
        {
            _storePlaceController.ControllerContext = new ControllerContext();
            _storePlaceController.ControllerContext.HttpContext = new DefaultHttpContext();

            var client = new Client_DTO();
            _storePlaceController.ControllerContext.HttpContext.Items["token"] = null;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);

            var result = _storePlaceController.GetFavoriteStores();
            Assert.IsType<BadRequestResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Never);
            _mockIStorePlaceRepository.Verify(x => x.GetFavoriteStores(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void GetFavoriteStoresSuccessTest()
        {
            _storePlaceController.ControllerContext = new ControllerContext();
            _storePlaceController.ControllerContext.HttpContext = new DefaultHttpContext();

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };
            var client = new Client_DTO() { id = 1 };
            _storePlaceController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);

            var result = _storePlaceController.GetFavoriteStores();
            Assert.IsType<OkObjectResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIStorePlaceRepository.Verify(x => x.GetFavoriteStores(It.IsAny<long>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void AddFavoriteStoreStatusCode201(long id)
        {
            _storePlaceController.ControllerContext = new ControllerContext();
            _storePlaceController.ControllerContext.HttpContext = new DefaultHttpContext();

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };
            var client = new Client_DTO() { id = 1 };
            _storePlaceController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);
            _mockIStorePlaceRepository.Setup(x => x.AddFavoriteStore(It.IsAny<long>(), It.IsAny<long>())).Returns(1);

            var result = _storePlaceController.AddFavoriteStore(id);
            Assert.IsType<ObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(201, ((ObjectResult)result).StatusCode);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIStorePlaceRepository.Verify(x => x.AddFavoriteStore(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
        }
    }
}