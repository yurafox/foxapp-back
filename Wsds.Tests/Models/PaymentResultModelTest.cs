using Wsds.WebApp.Models;
using Xunit;

namespace Wsds.Tests.Models
{
    public class PaymentResultModelTest
    {
        [Fact]
        public void AuthModelNotNull()
        {
            var paymentResultModel = new PaymentResultModel();
            Assert.NotNull(paymentResultModel);
        }

        [Fact]
        public void LmiMerchantIdFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_MERCHANT_ID = "12345";

            Assert.True(IsLmiMerchantIdFieldSet(model));
        }

        [Fact]
        public void LmiPaymentNoFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_PAYMENT_NO = "No";

            Assert.True(IsLmiPaymentNoFieldSet(model));
        }

        [Fact]
        public void LmiPaymentAmountFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_PAYMENT_AMOUNT = "12";

            Assert.True(IsLmiPaymentAmountFieldSet(model));
        }

        [Fact]
        public void LmiPaymentDescFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_PAYMENT_DESC = "Desc";

            Assert.True(IsLmiPaymentDescFieldSet(model));
        }

        [Fact]
        public void LmiSysPaymentIdFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_SYS_PAYMENT_ID = "1";

            Assert.True(IsLmiSysPaymentIdFieldSet(model));
        }

        [Fact]
        public void LmiPaymentNotificationUrlFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_PAYMENT_NOTIFICATION_URL = "https://www.google.com/";

            Assert.True(IsLmiPaymentNotoficationUrlFieldSet(model));
        }

        [Fact]
        public void LmiPaymentSystemFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_PAYMENT_SYSTEM = "System";

            Assert.True(IsLmiPaymentSystemFieldSet(model));
        }

        [Fact]
        public void LmiModeFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_MODE = "Mode";

            Assert.True(IsLmiModeFieldSet(model));
        }

        [Fact]
        public void LmiPaidAmounFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_PAID_AMOUN = "Amoun";

            Assert.True(IsLmiPaidAmounFieldSet(model));
        }

        [Fact]
        public void ReceiptTokenFieldTest()
        {
            var model = new PaymentResultModel();
            model.receiptToken = "receiptToken";

            Assert.True(IsReceiptTokenFieldSet(model));
        }

        [Fact]
        public void LmiSysPaymentDateFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_SYS_PAYMENT_DATE = "payment date";

            Assert.True(IsLmiSysPaymentDateFieldSet(model));
        }

        [Fact]
        public void LmiClientMessageFieldTest()
        {
            var model = new PaymentResultModel();
            model.LMI_CLIENT_MESSAGE = "message";

            Assert.True(IsLmiClientMessageFieldSet(model));
        }

        [Fact]
        public void ErrorCodeFieldTest()
        {
            var model = new PaymentResultModel();
            model.ErrorCode = "Error accured";

            Assert.True(IsErrorCodeFieldSet(model));
        }

        [Fact]
        public void ErrorPaySystemCodeFieldTest()
        {
            var model = new PaymentResultModel();
            model.ErrorPaysystemCode = "Error accured";

            Assert.True(IsErrorPaySystemCodeFieldSet(model));
        }

        [Fact]
        public void VerificationSuccessTest()
        {
            var model = new PaymentResultModel();
            model.receiptToken = "token";
            model.LMI_SYS_PAYMENT_DATE = "01.01.2000";

            Assert.Equal("success", model.GetResultVerification());
        }

        [Fact]
        public void VerificationFailTest()
        {
            var model = new PaymentResultModel();
            model.LMI_SYS_PAYMENT_DATE = null;
            model.receiptToken = string.Empty;

            Assert.Equal("fail", model.GetResultVerification());
        }

        public bool IsLmiMerchantIdFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_MERCHANT_ID) ? false : true;
        }

        public bool IsLmiPaymentNoFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_PAYMENT_NO) ? false : true;
        }

        public bool IsLmiPaymentAmountFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_PAYMENT_AMOUNT) ? false : true;
        }

        public bool IsLmiPaymentDescFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_PAYMENT_DESC) ? false : true;
        }

        public bool IsLmiSysPaymentIdFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_SYS_PAYMENT_ID) ? false : true;
        }

        public bool IsLmiPaymentNotoficationUrlFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_PAYMENT_NOTIFICATION_URL) ? false : true;
        }

        public bool IsLmiPaymentSystemFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_PAYMENT_SYSTEM) ? false : true;
        }

        public bool IsLmiModeFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_MODE) ? false : true;
        }

        public bool IsLmiPaidAmounFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_PAID_AMOUN) ? false : true;
        }

        public bool IsReceiptTokenFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.receiptToken) ? false : true;
        }

        public bool IsLmiSysPaymentDateFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_SYS_PAYMENT_DATE) ? false : true;
        }

        public bool IsLmiClientMessageFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.LMI_CLIENT_MESSAGE) ? false : true;
        }

        public bool IsErrorCodeFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.ErrorCode) ? false : true;
        }

        public bool IsErrorPaySystemCodeFieldSet(PaymentResultModel model)
        {
            return string.IsNullOrEmpty(model.ErrorPaysystemCode) ? false : true;
        }
    }
}
