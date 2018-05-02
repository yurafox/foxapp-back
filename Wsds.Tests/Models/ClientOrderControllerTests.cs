using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;

namespace Wsds.Tests.Models
{
    public class ClientOrderControllerTests
    {
        private Mock<IOrdersRepository> _mockIOrdersRepository;
        private ClientOrderController _clientOrderController;

        public ClientOrderControllerTests()
        {
            //Arrange
            _mockIOrdersRepository = new Mock<IOrdersRepository>();
            _clientOrderController = new ClientOrderController(_mockIOrdersRepository.Object);
        }

        [Fact]
        public void ClientOrderControllerCreated()
        {
            Assert.NotNull(_clientOrderController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Client_Order>() { new Client_Order() };
            _mockIOrdersRepository.Setup(x => x.ClientOrders).Returns(list);
        }
    }
}
