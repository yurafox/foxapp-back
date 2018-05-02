using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Wsds.DAL.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Wsds.DAL.Entities;

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
            var mockHttpContext1 = new Mock<HttpContext>();
            //mockHttpContext1.Setup(x => x.GeTokenModel()).Returns(new TokenModel());

            var clientDto = new Client_DTO() { phone = "380990000001"};

            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(clientDto);
            //_clientMessageController.SaveClientMessage(clientMessage);
        }

    }
}
