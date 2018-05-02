using Wsds.DAL.Infrastructure.Facade;
using Xunit;
using Moq;
using Wsds.WebApp.Auth.Protection;
using Wsds.WebApp.Controllers;
using System.Threading.Tasks;
using Wsds.WebApp.Models;
using System.Threading;
using Wsds.DAL.Repository.Abstract;
using Wsds.WebApp.Auth;
using Wsds.DAL.Identity;
using Wsds.DAL.Repository.Specific;

namespace Wsds.Tests.Controllers
{

    public class VerifyResponse
    {
        public string message { get; set; }
        public string status { get; set; }
    }

    public class AccountControllerTests
    {
        [Fact]
        public void TokenModelNotNull()
        {
            var mockAccountUserFacade = new Mock<AccountUserFacade>(null, null, null);
            var mockICrypto = new Mock<ICrypto>();

            var tokenModel = new TokenModel();
            Assert.NotNull(tokenModel);
        }

        [Fact]
        public void TokenModelVerified()
        {
            var mockAccountUserFacade = new Mock<AccountUserFacade>(null, null, null);
            var mockICrypto = new Mock<ICrypto>();

            var tokenModel = new TokenModel { ClientId = 1, Card = 1, Phone = "380990000001" };
            var verified = tokenModel.ValidateDataFromToken();

            Assert.True(verified);
        }

        [Fact]
        public async Task AccountControllerCreated()
        {   
            // setup
            //var mockIUserRepository = Mock.Of<IUserRepository>();
            var mockIUserRepository = new Mock<FSUserRepository>(null,null);
            var mockIRoleRepository = Mock.Of<IRoleRepository>();
            var mockIClientRepository = Mock.Of<IClientRepository>();

            // Arrange            
            var mockAccountUserFacade = new Mock<AccountUserFacade>(mockIUserRepository.Object,mockIRoleRepository,mockIClientRepository);

            var mockICrypto = new Mock<ICrypto>();//

            AccountController accountController = new AccountController(mockAccountUserFacade.Object, mockICrypto.Object);
            
            accountController.ModelState.AddModelError("Phone", "Required");

            AuthModel authModel = new AuthModel { Phone="380870000001", Password="12345@@asdw12"};

            Task t = accountController.Login(authModel);
            Assert.NotNull(t);

            var res = t.IsCompleted;
            Assert.Equal(true, res);

            //-------------------------------------------
            //var c = new System.Net.Http.HttpClient();
            //var result = await c.GetStringAsync("http://www.google.com");
            //Assert.Contains("google", result);
        }

        public Task<bool> CheckUser()
        {
            return Task<bool>.Factory.StartNew(() => true);
        }

        public Task<AppUser> GetAppUser()
        {
            return Task<AppUser>.Factory.StartNew(() => new AppUser());
        }

        public async Task<bool> CheckUserWait(string phone, string password)
        {
            await Task.Run(() => Thread.Sleep(2000));
            return true;
        }
    }
}
