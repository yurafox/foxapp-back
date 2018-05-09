using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Wsds.WebApp.Auth;

namespace Wsds.Tests.Controllers
{
    public class ClientMessageControllerTests
    {
        private Mock<IClientRepository> _mockIClientRepository;
        private Mock<IClientMessageRepository> _mockIClientMessageRepository;
        private ClientMessageController _clientMessageController;

        public ClientMessageControllerTests()
        {
            //Arrange
            _mockIClientRepository = new Mock<IClientRepository>();
            _mockIClientMessageRepository = new Mock<IClientMessageRepository>();
            _clientMessageController = new ClientMessageController(_mockIClientRepository.Object, _mockIClientMessageRepository.Object);
        }

        [Fact]
        public void ClientMessageControllerCreated()
        {
            Assert.NotNull(_clientMessageController);
        }

        [Fact]
        public void SaveClientMessageTest()
        {
            var clientMessage = new ClientMessage_DTO() { id = 1};
            //var mockHttpContext1 = new Mock<HttpContext>();

            _clientMessageController.ControllerContext = new ControllerContext();
            _clientMessageController.ControllerContext.HttpContext = new DefaultHttpContext();

            //mockHttpContext1.Setup(x => x.GeTokenModel()).Returns(new TokenModel());

            var clientDto = new Client_DTO() { phone = "380990000001", id = 1 };
            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };

            _clientMessageController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(clientDto);
            _mockIClientMessageRepository.Setup(x => x.SaveClientMessage(It.IsAny<ClientMessage_DTO>(), It.IsAny<long>())).Returns(clientMessage);

            var result = _clientMessageController.SaveClientMessage(clientMessage);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.NotNull(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIClientMessageRepository.Verify(x => x.SaveClientMessage(It.IsAny<ClientMessage_DTO>(), It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void SaveClientMessageBadRequestTest1() //because of the token model is null
        {
            var clientMessage = new ClientMessage_DTO() { id = 1 };

            _clientMessageController.ControllerContext = new ControllerContext();
            _clientMessageController.ControllerContext.HttpContext = new DefaultHttpContext();

            var clientDto = new Client_DTO() { phone = "380990000001", id = 1 };
            _clientMessageController.ControllerContext.HttpContext.Items["token"] = null;

            var result = _clientMessageController.SaveClientMessage(clientMessage);
            Assert.IsType<BadRequestResult>(result);
            Assert.NotNull(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Never);
            _mockIClientMessageRepository.Verify(x => x.SaveClientMessage(It.IsAny<ClientMessage_DTO>(), It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void SaveClientMessageBadRequestTest2() //because of the client.id is null
        {
            var clientMessage = new ClientMessage_DTO() { id = 1 };

            _clientMessageController.ControllerContext = new ControllerContext();
            _clientMessageController.ControllerContext.HttpContext = new DefaultHttpContext();

            var clientDto = new Client_DTO() { phone = "380990000001"};

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };

            _clientMessageController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(clientDto);
            _mockIClientMessageRepository.Setup(x => x.SaveClientMessage(It.IsAny<ClientMessage_DTO>(), It.IsAny<long>())).Returns(clientMessage);

            var result = _clientMessageController.SaveClientMessage(clientMessage);

            Assert.IsType<BadRequestResult>(result);
            Assert.NotNull(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIClientMessageRepository.Verify(x => x.SaveClientMessage(It.IsAny<ClientMessage_DTO>(), It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void SaveClientMessageBadRequestTest3() //because of ClientMessage_DTO is null
        {
            ClientMessage_DTO clientMessage = null;

            _clientMessageController.ControllerContext = new ControllerContext();
            _clientMessageController.ControllerContext.HttpContext = new DefaultHttpContext();

            var clientDto = new Client_DTO() { phone = "380990000001" };

            var token = new TokenModel() { Phone = "380120000000", Card = 1, ClientId = 1 };

            _clientMessageController.ControllerContext.HttpContext.Items["token"] = token;

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(clientDto);
            _mockIClientMessageRepository.Setup(x => x.SaveClientMessage(It.IsAny<ClientMessage_DTO>(), It.IsAny<long>())).Returns(clientMessage);

            var result = _clientMessageController.SaveClientMessage(clientMessage);

            Assert.IsType<BadRequestResult>(result);
            Assert.NotNull(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(It.IsAny<string>()), Times.Once);
            _mockIClientMessageRepository.Verify(x => x.SaveClientMessage(It.IsAny<ClientMessage_DTO>(), It.IsAny<long>()), Times.Never);
        }
    }
}
