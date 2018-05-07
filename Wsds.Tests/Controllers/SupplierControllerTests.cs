using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;
using System.Collections.Generic;
using Wsds.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Wsds.Tests.Controllers
{
    public class SupplierControllerTests
    {
        private Mock<ISupplierRepository> _mockISupplierRepository;
        private SupplierController _supplierController;

        public SupplierControllerTests()
        {
            _mockISupplierRepository = new Mock<ISupplierRepository>();
            _supplierController = new SupplierController(_mockISupplierRepository.Object);
        }

        [Fact]
        public void SupplierControllerCreated()
        {
            Assert.NotNull(_supplierController);
        }

        [Fact]
        public void GetTest()
        {
            var list = new List<Supplier_DTO>() { new Supplier_DTO()};
            _mockISupplierRepository.Setup(x => x.Suppliers).Returns(list);

            var result = _supplierController.Get();
            Assert.IsType<OkObjectResult>(result);

            _mockISupplierRepository.Verify(x => x.Suppliers, Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void GetTestById(long id)
        {
            var supplier = new Supplier_DTO();
            _mockISupplierRepository.Setup(x => x.Supplier(It.IsAny<long>())).Returns(supplier);

            var result = _supplierController.Get(id);
            Assert.IsType<OkObjectResult>(result);

            _mockISupplierRepository.Verify(x => x.Supplier(id), Times.Once);
        }
    }
}
