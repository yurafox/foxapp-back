using Wsds.WebApp.Models;
using Xunit;

namespace Wsds.Tests.Models
{
    public class RoleShortModelTest
    {
        [Fact]
        public void RoleShortModelNotNull()
        {
            var roleShortModel = new RoleShortModel();
            Assert.NotNull(roleShortModel);
        }

        [Fact]
        public void IdFieldTest()
        {
            var model = new RoleShortModel();
            model.Id = "12345";

            Assert.True(IsIdFieldSet(model));
        }

        [Fact]
        public void NameFieldTest()
        {
            var model = new RoleShortModel();
            model.Name = "name";

            Assert.True(IsNameFieldSet(model));
        }

        [Theory]
        [InlineData("foxtrotString")]
        [InlineData("new foxtrotString")]
        [InlineData("some string a lot of fish")]
        [InlineData("Microsoft article on this subject")]
        [InlineData("Microsoft article on this subject has some details")]
        //[InlineData("The final part treats a string literal, contained in quotation marks, as an object in the C# language.")]
        public void NameFieldValidationTest(string name)
        {
            var model = new RoleShortModel();
            model.Name = name;

            Assert.True(NameFiledLengthValidation(model.Name));
        }

        public bool IsIdFieldSet(RoleShortModel model)
        {
            return string.IsNullOrEmpty(model.Id) ? false : true;
        }

        public bool IsNameFieldSet(RoleShortModel model)
        {
            return string.IsNullOrEmpty(model.Name) ? false : true;
        }

        public bool NameFiledLengthValidation(string name)
        {
            return name.Length <=50 ? true : false;
        }
    }
}
