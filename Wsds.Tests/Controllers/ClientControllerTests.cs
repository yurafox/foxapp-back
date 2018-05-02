using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Wsds.DAL.Entities.Communication;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using Xunit;

namespace Wsds.Tests.Controllers
{
    public class ClientControllerTests
    {
        private Mock<IClientRepository> _mockIClientRepository;
        private ClientController _clientController;
        public ClientControllerTests()
        {
            //Arrange
            _mockIClientRepository = new Mock<IClientRepository>();
            _clientController = new ClientController(_mockIClientRepository.Object);
        }

        [Fact]
        public void ClientControllerCreated()
        {
            Assert.NotNull(_clientController);
        }

        [Fact]
        public void GetClientsTest()
        {
            _mockIClientRepository.Setup(x => x.Clients).Returns(GetClient_DTO_Collection());
            var result = _clientController.GetClients();

            _mockIClientRepository.Verify(x => x.Clients, Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(700)]
        public void GetClientByIdTest(int id)
        {
            _mockIClientRepository.Setup(x => x.Client(id)).Returns(InstanceClient_DTO(id));
            var result = _clientController.GetClientById(id);

            _mockIClientRepository.Verify(x => x.Client(id), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("5")]
        [InlineData("700")]
        public void GetClientByUserIDTest(string userId)
        {
            _mockIClientRepository.Setup(x => x.GetClientByUserID(It.IsAny<Int64>())).Returns(GetClient_DTO(Int64.Parse(userId)));
            var result = _clientController.GetClientByUserID(userId);

            Assert.IsType<OkObjectResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByUserID(Int64.Parse(userId)), Times.Once);
        }

        [Theory]
        [InlineData("12345@gmail.com")]
        public void GetClientByEmailTest(string userEmail)
        {
            _mockIClientRepository.Setup(x => x.GetClientByEmail(It.IsAny<string>())).Returns(GetClient_DTO_ByEmail(userEmail));
            var result = _clientController.GetClientByEmail(userEmail);

            Assert.IsType<OkObjectResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByEmail(userEmail), Times.Once);
        }

        [Theory]
        [InlineData("380990000001")]
        public void GetClientByPhoneTest(string userPhone)
        {
            _mockIClientRepository.Setup(x => x.GetClientByPhone(It.IsAny<string>())).Returns(InstanceClient_DTO(1));
            var result = _clientController.GetClientByPhone(userPhone);

            Assert.IsType<OkObjectResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientByPhone(userPhone), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(500)]
        public void GetPersonByIdTest(long userId)
        {
            
            _mockIClientRepository.Setup(x => x.GetPersonById(It.IsAny<long>())).Returns(new PersonInfo_DTO() { id = userId});
            var result = _clientController.GetPersonById(userId);

            Assert.IsType<OkObjectResult>(result);

            _mockIClientRepository.Verify(x => x.GetPersonById(userId), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(500)]
        public void CreatePersonTest(long userId)
        {
            var personInfo = new PersonInfo_DTO() { id = userId};
            _mockIClientRepository.Setup(x => x.CreatePerson(It.IsAny<PersonInfo_DTO>())).Returns(personInfo);

            var result = _clientController.CreatePerson(personInfo);

            Assert.IsType<CreatedAtRouteResult>(result);
            _mockIClientRepository.Verify(x => x.CreatePerson(personInfo), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(500)]
        public void UpdatePersonTest(long userId)
        {
            var personInfo = new PersonInfo_DTO() { id = userId };
            _mockIClientRepository.Setup(x => x.UpdatePerson(It.IsAny<PersonInfo_DTO>())).Returns(personInfo);

            var result = _clientController.UpdatePerson(personInfo);

            Assert.IsType<OkObjectResult>(result);
            _mockIClientRepository.Verify(x => x.UpdatePerson(personInfo), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetClientBonusesInfoTest(long id)
        {
            var obj = new Object();
            _mockIClientRepository.Setup(x => x.GetClientBonusesInfo(It.IsAny<long>())).Returns(obj);

            var result = _clientController.GetClientBonusesInfo(id);

            Assert.IsType<OkObjectResult>(result);
            _mockIClientRepository.Verify(x => x.GetClientBonusesInfo(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetClientBonusesExpireInfoTest(long clientId)
        {
            var list = new List<object>() { new Object() };
            _mockIClientRepository.Setup(x => x.GetClientBonusesExpireInfo(It.IsAny<long>())).Returns(list);

            var result = _clientController.GetClientBonusesExpireInfo(clientId);
            Assert.IsType<OkObjectResult>(result);

            _mockIClientRepository.Verify(x => x.GetClientBonusesExpireInfo(clientId), Times.Once);
        }

        [Fact]
        public void CreateCartProductTest()
        {
            LogProductViewRequest logProduct = new LogProductViewRequest() { idProduct = 1, viewParams = "params"};
            _mockIClientRepository.Setup(x => x.LogProductView(It.IsAny<long>(), It.IsAny<string>()));

            var result = _clientController.CreateCartProduct(logProduct);

            Assert.IsType<CreatedResult>(result);
            _mockIClientRepository.Verify(x => x.LogProductView(logProduct.idProduct, logProduct.viewParams), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ClientAddressTest(long Id)
        {
            var clientAddress = new ClientAddress_DTO() {id = Id};
            _mockIClientRepository.Setup(x => x.ClientAddress(It.IsAny<long>())).Returns(clientAddress);

            var result = _clientController.ClientAddress(Id);

            Assert.IsType<OkObjectResult>(result);
            _mockIClientRepository.Verify(x => x.ClientAddress(Id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetClientAddressesByClientIdTest(long idClient)
        {
            var list = new List<ClientAddress_DTO>() { new ClientAddress_DTO() { id = idClient} };
            _mockIClientRepository.Setup(x => x.GetClientAddressesByClientId(It.IsAny<long>())).Returns(list);

            var result = _clientController.GetClientAddressesByClientId(idClient);

            Assert.IsType<OkObjectResult>(result);
            _mockIClientRepository.Verify(x => x.GetClientAddressesByClientId(idClient), Times.Once);
        }

        [Fact]
        public void CreateClientAddressTest()
        {
            var clientAddress = new ClientAddress_DTO() { id = 1 };
            _mockIClientRepository.Setup(x => x.CreateClientAddress(It.IsAny<ClientAddress_DTO>())).Returns(clientAddress);

            var result = _clientController.CreateClientAddress(clientAddress);

            Assert.IsType<CreatedAtRouteResult>(result);
            _mockIClientRepository.Verify(x => x.CreateClientAddress(clientAddress), Times.Once);
        }

        [Fact]
        public void UpdateClientAddressTest()
        {
            var clientAddress = new ClientAddress_DTO() { id = 1 };
            _mockIClientRepository.Setup(x => x.UpdateClientAddress(It.IsAny<ClientAddress_DTO>())).Returns(clientAddress);

            var result = _clientController.UpdateClientAddress(clientAddress);

            Assert.IsType<OkObjectResult>(result);
            _mockIClientRepository.Verify(x => x.UpdateClientAddress(clientAddress), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteClientAddressTest(long Id)
        {
            _mockIClientRepository.Setup(x => x.DeleteClientAddress(It.IsAny<long>()));

            var result = _clientController.DeleteClientAddress(Id);

            Assert.IsType<NoContentResult>(result);
            _mockIClientRepository.Verify(x => x.DeleteClientAddress(Id), Times.Once);
        }

        private Client_DTO InstanceClient_DTO(long _id)
        {
            return new Client_DTO() { id =_id };
        }

        private IEnumerable<Client_DTO> GetClient_DTO_Collection()
        {
            return new List<Client_DTO>()
            {
                new Client_DTO() { id=1 },
                new Client_DTO() { id=2 }
            };
        }

        private IEnumerable<Client_DTO> GetClient_DTO(long userId)
        {
            return new List<Client_DTO>()
            {
                new Client_DTO() { id=userId }
            };
        }

        private IEnumerable<Client_DTO> GetClient_DTO_ByEmail(string _email)
        {
            return new List<Client_DTO>()
            {
                new Client_DTO() { email = _email  }
            };
        }
    }
}
