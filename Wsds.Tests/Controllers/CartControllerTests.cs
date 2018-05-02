using Wsds.DAL.Repository.Abstract;
using Xunit;
using Moq;
using Wsds.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Wsds.DAL.Entities;
using System;
using System.Collections.Generic;
using Wsds.DAL.Entities.Communication;

namespace Wsds.Tests.Controllers
{
    public class CartControllerTests
    {
        [Fact]
        public void CartControllerCreated()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            Assert.NotNull(cartController);
        }

        [Fact]
        public void CartProductsTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            var result = cartController.CartProducts();

            Assert.IsType<OkObjectResult>(result);

            mockICartRepository.Verify(x => x.GetClientOrderProductsByUserId(3), Times.Once);
        }

        [Fact]
        public void UpdateCartProductTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);
            ClientOrderProduct_DTO dto = new ClientOrderProduct_DTO();

            var result = cartController.UpdateCartProduct(dto);

            mockICartRepository.Verify(x => x.UpdateCartProduct(dto), Times.Once);
        }

        [Fact]
        public void CreateCartProductTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            ClientOrderProduct_DTO dto = InstanceClientOrderProduct();

            mockICartRepository.Setup(x => x.InsertCartProduct(It.IsAny<ClientOrderProduct_DTO>()))
                .Returns(dto);

            var result = cartController.CreateCartProduct(dto);

            mockICartRepository.Verify(x => x.InsertCartProduct(dto), Times.Once);

            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public void DeleteCartProductTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            mockICartRepository.Setup(x => x.DeleteCartProduct(It.IsAny<long>()));

            var result = cartController.DeleteCartProduct(1);

            mockICartRepository.Verify(x => x.DeleteCartProduct(1), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetClientDraftOrderTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            mockICartRepository.Setup(x => x.GetOrCreateClientDraftOrder()).Returns(InstanceClientOrder_DTO());

            var result = cartController.getClientDraftOrder();

            mockICartRepository.Verify(x => x.GetOrCreateClientDraftOrder(), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetClientOrderProductsByOrderIdTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            mockICartRepository.Setup(x => x.GetClientOrderProductsByOrderId(It.IsAny<long>())).Returns(GetListClientOrderProduct_DTO());

            var result = cartController.GetClientOrderProductsByOrderId(1);

            mockICartRepository.Verify(x => x.GetClientOrderProductsByOrderId(1), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetClientOrdersTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            mockICartRepository.Setup(x => x.GetClientOrders()).Returns(GetListClientOrder_DTO());

            var result = cartController.GetClientOrders();

            mockICartRepository.Verify(x => x.GetClientOrders(), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void SaveClientOrderTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);

            mockICartRepository.Setup(x => x.SaveClientOrder(It.IsAny<ClientOrder_DTO>())).Returns(InstanceClientOrder_DTO());

            var clientOrder_DTO = InstanceClientOrder_DTO();
            var result = cartController.SaveClientOrder(clientOrder_DTO);

            mockICartRepository.Verify(x => x.SaveClientOrder(clientOrder_DTO), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CalculateCartTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);
            var calculateCartRequest = new CalculateCartRequest();
            IEnumerable<CalculateCartResponse> response = new List<CalculateCartResponse>() { new CalculateCartResponse(), new CalculateCartResponse()};

            mockICartRepository.Setup(x => x.CalculateCart(It.IsAny<CalculateCartRequest>())).Returns(response);

            var result = cartController.CalculateCart(calculateCartRequest);

            mockICartRepository.Verify(x => x.CalculateCart(calculateCartRequest), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostOrderTest()
        {
            var mockICartRepository = new Mock<ICartRepository>();
            var cartController = new CartController(mockICartRepository.Object);
            var clientOrder_DTO = InstanceClientOrder_DTO();
            var postOrderResponse = new PostOrderResponse();

            mockICartRepository.Setup(x => x.PostOrder(It.IsAny<ClientOrder_DTO>())).Returns(postOrderResponse);

            var result = cartController.PostOrder(clientOrder_DTO);

            mockICartRepository.Verify(x => x.PostOrder(clientOrder_DTO), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        private ClientOrderProduct_DTO InstanceClientOrderProduct()
        {
            var clientOrderProduct = new ClientOrderProduct_DTO()
            {
                id = 1111111,
                idOrder = 1,
                idQuotationProduct = 1,
                price = 1,
                qty = 1,
                idStorePlace = 1,
                idLoEntity = 1,
                loTrackTicket = "loTrackTicket",
                loDeliveryCost = 1,
                loDeliveryCompleted = true,
                loEstimatedDeliveryDate = DateTime.Today,
                loDeliveryCompletedDate = DateTime.Now,
                errorMessage = "error",
                warningMessage = "warning",
                warningRead = true,
                payPromoCode = "promo",
                payPromoCodeDiscount = 1,
                payBonusCnt = 1,
                payPromoBonusCnt = 1,
                earnedBonusCnt = 1
            };

            return clientOrderProduct;
        }

        private ClientOrder_DTO InstanceClientOrder_DTO()
        {
            return new ClientOrder_DTO();
        }

        private IEnumerable<ClientOrderProduct_DTO> GetListClientOrderProduct_DTO()
        {
            var list = new List<ClientOrderProduct_DTO>();
            list.Add(InstanceClientOrderProduct());
            list.Add(InstanceClientOrderProduct());

            return list;
        }

        private IEnumerable<ClientOrder_DTO> GetListClientOrder_DTO()
        {
            var list = new List<ClientOrder_DTO>();
            list.Add(InstanceClientOrder_DTO());
            list.Add(InstanceClientOrder_DTO());

            return list;
        }
    }
}
