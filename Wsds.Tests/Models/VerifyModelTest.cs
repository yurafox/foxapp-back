using Wsds.WebApp.Models;
using Xunit;

namespace Wsds.Tests.Models
{
    public class VerifyModelTest
    {
        [Fact]
        public void VerifyModelNotNull()
        {
            var verifyModel = new VerifyModel();
            Assert.NotNull(verifyModel);
        }

        [Theory]
        [InlineData("380970000001")]
        [InlineData("380970000002")]
        [InlineData("380970000005")]
        [InlineData("380970000010")]
        [InlineData("380980000001")]
        public void PhoneFieldTest(string phone)
        {
            var model = new VerifyModel();
            model.Phone = phone;

            Assert.True(IsPhoneFieldSet(model));
        }

        public bool IsPhoneFieldSet(VerifyModel model)
        {
            return string.IsNullOrEmpty(model.Phone) ? false : true;
        }
    }
}
