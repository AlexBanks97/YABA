using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common.Payment;
using Yaba.Common.Payment.DTO;
using Yaba.Web.Payments;

namespace Yaba.Web.Test
{
    public class PaypalPayTests
    {
        [Fact]
        public void pay_Test_returns_true()
        {

            var payment = new StripePaymentDto
            {
                Amount = "100.00",
                Description = "Test Transaction"

            };


            var target = new PaypalPay();
            
            //Act
            var result = target.Pay(payment);

            //Assert
            Assert.True(result);
        }
    }
}
