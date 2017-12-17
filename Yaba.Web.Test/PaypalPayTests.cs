using PayPal.Api;
using System;
using System.Collections.Generic;
using Xunit;
using Yaba.Common.Payment;
using Yaba.Web.Payments;

namespace Yaba.Web.Test
{
    public class PaypalPayTests
    {
        [Fact]
        public void pay_Test_returns_true()
        {

            var payment = new PaymentDto
            {
                Amount = "100.00",
                Description = "Test Transaction",
                PaymentProvider = "PayPal"
            };

            var target = new PaypalPay();
            
            //Act
            var result = target.Pay(payment);

            //Assert
            Assert.True(result);


        }

		[Fact]
		public void payout_Test_returns_true()
		{

			var payment = new PaymentDto
			{
				Amount = "100.00",
				Description = "Test Transaction",
				PaymentProvider = "PayPal"
			
			};

			var target = new PaypalPay();

			var config = new Dictionary<String, String>();
			config.Add("clientId", "AVW3VSprOUqMbB3FKDrMFH2e504IO6h3Qss9LmGjq0kcfkj6glmqqD7jMCbxIIFeqGrDcy7B2dt9_u_N");
			config.Add("clientSecret", "ECXUaE-0M5RCk3ut-enH-SFZrHMi70R8YEUgLJFS4nnd0A973fE8YPo9cuHx1jyINStSDl6P6gkjIJlI");
			config.Add("mode", "sandbox"); // Pls dont remove

			// Authenticate with PayPal
			var accessToken = new OAuthTokenCredential(config).GetAccessToken();

			//Act
			var result = target.PayOut(payment);

			//Assert
			Assert.True(result);

		}
	}
}
