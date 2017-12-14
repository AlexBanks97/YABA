using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common.Payment;
using Yaba.Web.Payments;

namespace Yaba.Web.Test
{
    public class StripePayTests
    {
	    [Fact(Skip="works but is skipped due to secret key")]
	    public void pay_Test_returns_true()
	    {
			//Arrange
		    var payment = new StripePaymentDto
		    {
			    Amount = (decimal) 200,
			    Description = "hej",
			    Token = "tok_visa"
			};

		    var target = new StripePay();
			
			//Act
		    var result = target.Pay(payment);

		    //Assert
			Assert.True(result);
	    }
    }
}
