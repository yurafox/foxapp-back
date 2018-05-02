using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Wsds.DAL.Entities.Communication;

namespace Wsds.Tests.Controllers
{
    public class LOControllerTests
    {
        private Mock<ILORepository> _mockILORepository;
        private LOController _loController;

        public LOControllerTests()
        {
            _mockILORepository = new Mock<ILORepository>();
            _loController = new LOController(_mockILORepository.Object);
        }

        [Fact]
        public void LOControllerCreated()
        {
            Assert.NotNull(_loController);
        }

        [Fact]
        public void GetLoEntityTest()
        {
            var list = new List<LoEntity_DTO>() { new LoEntity_DTO()};
            _mockILORepository.Setup(x => x.LoEntities).Returns(list);

            var result = _loController.GetLoEntity();
            Assert.IsType<OkObjectResult>(result);

            _mockILORepository.Verify(x => x.LoEntities, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetLoEntityByIdTest(long id)
        {
            var loEntity = new LoEntity_DTO();
            _mockILORepository.Setup(x => x.LoEntity(It.IsAny<long>())).Returns(loEntity);

            var result = _loController.GetLoEntityById(id);
            Assert.IsType<OkObjectResult>(result);

            _mockILORepository.Verify(x => x.LoEntity(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetLoEntitiesForSupplTest(long idSupplier)
        {
            var list = new List<LoSupplEntity_DTO>() { new LoSupplEntity_DTO() };
            _mockILORepository.Setup(x => x.GetLoEntitiesForSuppl(It.IsAny<long>())).Returns(list);

            var result = _loController.GetLoEntitiesForSuppl(idSupplier);
            Assert.IsType<OkObjectResult>(result);

            _mockILORepository.Verify(x => x.GetLoEntitiesForSuppl(idSupplier), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void GetTrackLogForOrderSpecProdTest(long idOrderSpecProd)
        {
            var list = new List<LoTrackLog>() { new LoTrackLog() };
            _mockILORepository.Setup(x => x.GetTrackLogForOrderSpecProd(It.IsAny<long>())).Returns(list);

            var result = _loController.GetTrackLogForOrderSpecProd(idOrderSpecProd);
            Assert.IsType<OkObjectResult>(result);

            _mockILORepository.Verify(x => x.GetTrackLogForOrderSpecProd(idOrderSpecProd), Times.Once);
        }

        [Fact]
        public void GetDeliveryCostTest()
        {
            var mockResult = new object();
            var _order = new ClientOrderProduct_DTO();
            var model = new DeliveryCostRequest() { order = _order, loEntity = 1, loIdClientAddress = 1};
            _mockILORepository.Setup(x => x.GetDeliveryCost(It.IsAny<ClientOrderProduct_DTO>(), It.IsAny<long>(), It.IsAny<long>())).Returns(mockResult);

            var result = _loController.GetDeliveryCost(model);
            Assert.IsType<OkObjectResult>(result);

            _mockILORepository.Verify(x => x.GetDeliveryCost(model.order,model.loEntity,model.loIdClientAddress), Times.Once);
        }

        [Fact]
        public void GetDeliveyDateTest()
        {
            var mockResult = new object();
            var _order = new ClientOrderProduct_DTO();
            var model = new DeliveryDateRequest() { order = _order, loEntity = 1, loIdClientAddress = 1 };
            _mockILORepository.Setup(x => x.GetDeliveryDate(It.IsAny<ClientOrderProduct_DTO>(), It.IsAny<long>(), It.IsAny<long>())).Returns(mockResult);

            var result = _loController.GetDeliveryDate(model);
            Assert.IsType<OkObjectResult>(result);

            _mockILORepository.Verify(x => x.GetDeliveryDate(model.order, model.loEntity, model.loIdClientAddress), Times.Once);

        }
    }
}
