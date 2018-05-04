using Xunit;
using Moq;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Controllers;

namespace Wsds.Tests.Controllers
{
    public class StockControllerTests
    {
        private Mock<IActionRepository> _mockIActionRepository;
        private StockController _stockController;

        public StockControllerTests()
        {
            _mockIActionRepository = new Mock<IActionRepository>();
            _stockController = new StockController(_mockIActionRepository.Object);
        }

        [Fact]
        public void StockControllerCreated()
        {
            Assert.NotNull(_stockController);
        }
    }
}
