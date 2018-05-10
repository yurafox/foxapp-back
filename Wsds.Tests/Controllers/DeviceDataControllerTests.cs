using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities.DTO;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Wsds.WebApp.Auth;

namespace Wsds.Tests.Controllers
{
    public class DeviceDataControllerTests
    {
        private Mock<IDeviceDataRepository> _mockIDeviceDataRepository;
        private Mock<IClientRepository> _mockIClientRepository;
        private DeviceDataController _deviceDataController;

        public DeviceDataControllerTests()
        {
            _mockIDeviceDataRepository = new Mock<IDeviceDataRepository>();
            _mockIClientRepository = new Mock<IClientRepository>();
            _deviceDataController = new DeviceDataController(_mockIClientRepository.Object, _mockIDeviceDataRepository.Object);
        }

        [Fact]
        public void DeviceDataControllerCreated()
        {
            Assert.NotNull(_deviceDataController);
        }

        [Fact]
        public void SaveDeviceDataTest()
        {
            var deviceData = new DeviceData_DTO();
            var client = new Client_DTO() { id = 1 };

            _deviceDataController.ControllerContext = new ControllerContext();
            _deviceDataController.ControllerContext.HttpContext = new DefaultHttpContext();

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };
            _deviceDataController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);

            _mockIDeviceDataRepository.Setup(x => x.SaveDeviceData(It.IsAny<DeviceData_DTO>(), It.IsAny<long>())).Returns(deviceData);

            var result = _deviceDataController.SaveDeviceData(deviceData);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.NotNull(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone("380120000000"), Times.Once);
            _mockIDeviceDataRepository.Verify(x => x.SaveDeviceData(deviceData, 1), Times.Once);
        }

        [Fact]
        public void SaveDeviceDataBadRequestTest1() // because of token model is null
        {
            var deviceData = new DeviceData_DTO();

            _deviceDataController.ControllerContext = new ControllerContext();
            _deviceDataController.ControllerContext.HttpContext = new DefaultHttpContext();
            _deviceDataController.ControllerContext.HttpContext.Items["token"] = null;

            var result = _deviceDataController.SaveDeviceData(deviceData);
            Assert.IsType<BadRequestResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Never);
            _mockIDeviceDataRepository.Verify(x => x.SaveDeviceData(It.IsAny<DeviceData_DTO>(), It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void SaveDeviceDataBadRequestTest2() // because of client id is null
        {
            var deviceData = new DeviceData_DTO();
            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };
            var client = new Client_DTO();

            _deviceDataController.ControllerContext = new ControllerContext();
            _deviceDataController.ControllerContext.HttpContext = new DefaultHttpContext();
            _deviceDataController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(client);

            _mockIDeviceDataRepository.Setup(x => x.SaveDeviceData(It.IsAny<DeviceData_DTO>(), It.IsAny<long>())).Returns(deviceData);

            var result = _deviceDataController.SaveDeviceData(deviceData);
            Assert.IsType<BadRequestResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIDeviceDataRepository.Verify(x => x.SaveDeviceData(It.IsAny<DeviceData_DTO>(), It.IsAny<long>()), Times.Never);
        }
    }
}
