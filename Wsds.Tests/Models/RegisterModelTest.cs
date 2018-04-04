using System.Text.RegularExpressions;
using Wsds.WebApp.Models;
using Xunit;

namespace Wsds.Tests.Models
{
    public class RegisterModelTest
    {
        [Fact]
        public void RegisterModelNotNull()
        {
            var registerModel = new RegisterModel();
            Assert.NotNull(registerModel);
        }

        [Fact]
        public void IdFieldTest()
        {
            var model = new RegisterModel();
            model.Id = "12345";

            Assert.True(IsIdFieldSet(model));
        }

        [Fact]
        public void EmailFieldTest()
        {
            var model = new RegisterModel();
            model.Email = "12345@gmail.com";

            Assert.True(IsEmailFieldSet(model));
        }

        [Theory]
        [InlineData("12345@gmail.com")]
        [InlineData("foxtrot@gmail.com")]
        [InlineData("foxtrot12345@gmail.com")]
        [InlineData("12345foxtrot@gmail.com")]
        [InlineData("--_12345foxtrot@gmail.com")]
        [InlineData("12345foxtrot12345@gmail.com")]
        [InlineData("12345foxtrot__@gmail.com")]
        [InlineData("______12345@gmail.com")]
        [InlineData("_________12345foxtrot__@gmail.com")]
        [InlineData("foxtrotSomeEmail@gmail.com")]
        [InlineData("12345foxtrot__SomeEmail@gmail.com")]
        [InlineData("______12345---_____@gmail.com")]
        [InlineData("_________12345foxtrot__Email@gmail.com")]
        //[InlineData(null)] //Fail mode
        //[InlineData("")] //Fail mode
        public void EmailPatternIsRight(string email)
        {
            var registerModel = new RegisterModel();
            registerModel.Email = email;

            Assert.True(IsEmailRight(registerModel.Email));
        }

        public bool IsIdFieldSet(RegisterModel model)
        {
            return string.IsNullOrEmpty(model.Id) ? false : true;
        }

        public bool IsEmailFieldSet(RegisterModel model)
        {
            return string.IsNullOrEmpty(model.Email) ? false : true;
        }

        public bool IsEmailRight(string email)
        {
            Regex rgx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return rgx.IsMatch(email);
        }
    }
}
