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
		    var payment = new PaymentDto
		    {
                Amount = "200",
			    
			    Token = "tok_visa",
                PaymentProvider = "Stripe"
			};

		    var target = new StripePay();
			
			//Act
		    var result = target.Pay(payment);

		    //Assert
            Assert.True(result.Equals("true"));
	    }
    }
}
