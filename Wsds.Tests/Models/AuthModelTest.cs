using System.Text.RegularExpressions;
using Wsds.WebApp.Models;
using Xunit;

namespace Wsds.Tests.Models
{
    public class AuthModelTest
    {
        [Fact]
        public void AuthModelNotNull()
        {
            var authModel = new AuthModel();
            Assert.NotNull(authModel);
        }

        [Theory]
        [InlineData("380500000002")]
        [InlineData("380500000002")]
        [InlineData("380500000008")]
        [InlineData("380500000009")]
        //[InlineData(null)] //Fail mode
        //[InlineData("")] //Fail mode
        public void PhonePatternIsRight(string phone)
        {
            var authModel = new AuthModel();
            authModel.Phone = phone;

            Assert.True(IsPhoneRight(authModel.Phone));
        }

        public bool IsPhoneRight(string phone)
        {
            Regex rgx = new Regex(@"^380[0-9]{9}$");
            return rgx.IsMatch(phone);
        }

        [Fact]
        public void PhoneFieldTest()
        {
            var model = new AuthModel();
            model.Phone = "380500000001";

            Assert.True(IsPhoneFieldSet(model));
        }

        [Fact]
        public void PasswordFieldTest()
        {
            var model = new AuthModel();
            model.Password = "12345";

            Assert.True(IsPasswordFieldSet(model));
        }

        public bool IsPhoneFieldSet(AuthModel authModel) {
            return string.IsNullOrEmpty(authModel.Phone) ? false : true;
        }

        public bool IsPasswordFieldSet(AuthModel authModel)
        {
            return string.IsNullOrEmpty(authModel.Password) ? false : true;
        }
    }
}
