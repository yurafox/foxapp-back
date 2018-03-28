using Wsds.WebApp.Models;
using Xunit;
using System;

namespace Wsds.Tests.Models
{
    public class PaymentModelTest
    {
        [Fact]
        public void PaymentModelNotNull()
        {
            var paymentModel = new PaymentModel();
            Assert.NotNull(paymentModel);
        }

        [Fact]
        public void IdFieldTest()
        {
            var model = new PaymentModel
            {
                Id = 22
            };

            Assert.True(IsIdFieldNotEmpty(model));
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(100.0)]
        [InlineData(200.0)]
        [InlineData(10000.0)]
        public void TotalFieldTest(decimal value)
        {
            var model = new PaymentModel
            {
                Total = value
            };

            Assert.True(IsTotalFieldNotEmpty(model));
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(100.0)]
        [InlineData(200.0)]
        [InlineData(10000.0)]
        public void TotalRange(decimal value)
        {
            var model = new PaymentModel
            {
                Total = value
            };

            var withinRange = (model.Total >= decimal.MinValue) && (model.Total <= decimal.MaxValue);

            Assert.True(withinRange);
        }

        public bool IsIdFieldNotEmpty(PaymentModel paymentModel)
        {
            int? id = paymentModel.Id;
            return  id != null ? true : false;
        }

        public bool IsTotalFieldNotEmpty(PaymentModel paymentModel)
        {
            decimal? total = paymentModel.Total;
            return total != null ? true : false;
        }
    }
}
