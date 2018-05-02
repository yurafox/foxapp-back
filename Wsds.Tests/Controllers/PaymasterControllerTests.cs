using Xunit;
using Moq;
using System.Collections.Generic;
using Wsds.WebApp.Controllers;
using Wsds.WebApp.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace Wsds.Tests.Controllers
{
    public class PaymasterControllerTests
    {
        private PaymasterController _paymasterController;

        public PaymasterControllerTests()
        {
            _paymasterController = new PaymasterController();
        }

        [Fact]
        public void PaymasterControllerCreated()
        {
            Assert.NotNull(_paymasterController);
        }

        [Fact]
        public void PaymentSuccessfullTest()
        {
            var payment = new PaymentModel()
            {
                Id = 2,
                Total = 2
            };

            //Mock for TryValidateModel method in controller
            //
            //https://github.com/aspnet/Mvc/issues/3586
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            _paymasterController.ObjectValidator = objectValidator.Object;

            var result = _paymasterController.Payment(payment);

            ViewResult viewResult = result as ViewResult;

            ////Assert
            Assert.IsType<ViewResult>(viewResult);
            Assert.NotNull(result);
            Assert.NotNull(viewResult);
            Assert.True(viewResult.ViewName == "Payment");
            Assert.Equal(payment, viewResult.Model);
        }

        [Fact]
        public void PaymentFailTest()
        {
            var result = _paymasterController.Payment(null);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ошибка состояния данных в запросе к платежной системе",((BadRequestObjectResult)result).Value);
        }

        [Fact]
        public void ResultSuccessTest()
        {
            var paymentResultModel = new PaymentResultModel() {};

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            _paymasterController.ObjectValidator = objectValidator.Object;

            var result = _paymasterController.Result(paymentResultModel);

            ViewResult viewResult = result as ViewResult;

            Assert.IsType<ViewResult>(viewResult);
            Assert.NotNull(result);
            Assert.NotNull(viewResult);
            Assert.True(viewResult.ViewName == "Result");
        }

        [Fact]
        public void ResultVerificationSuccessTest()
        {
            var paymentResultModel = new PaymentResultModel() { receiptToken ="token", LMI_SYS_PAYMENT_DATE = "date"};

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            _paymasterController.ObjectValidator = objectValidator.Object;

            var result = _paymasterController.Result(paymentResultModel);

            ViewResult viewResult = result as ViewResult;

            var resultVerification = paymentResultModel.GetResultVerification();
            Assert.Equal("success", resultVerification);
            Assert.Equal("success", viewResult.ViewData["Result"]);
        }

        [Fact]
        public void ResultVerificationFailTest()
        {
            var paymentResultModel = new PaymentResultModel();

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            _paymasterController.ObjectValidator = objectValidator.Object;

            var result = _paymasterController.Result(paymentResultModel);

            ViewResult viewResult = result as ViewResult;

            var resultVerification = paymentResultModel.GetResultVerification();
            Assert.Equal("fail", resultVerification);
            Assert.Equal("fail", viewResult.ViewData["Result"]);
        }
    }
}
