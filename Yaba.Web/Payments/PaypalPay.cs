using System;
using System.Collections.Generic;

using Yaba.Common.Payment;

using PayPal.Api;

namespace Yaba.Web.Payments
{
    public class PaypalPay : IPaymentRepository
    {
       public bool Pay(PaymentDto dto){

            var config = new Dictionary<String, String>();
            config.Add("clientId","AVW3VSprOUqMbB3FKDrMFH2e504IO6h3Qss9LmGjq0kcfkj6glmqqD7jMCbxIIFeqGrDcy7B2dt9_u_N");
            config.Add("clientSecret", "ECXUaE-0M5RCk3ut-enH-SFZrHMi70R8YEUgLJFS4nnd0A973fE8YPo9cuHx1jyINStSDl6P6gkjIJlI");
            config.Add("mode","sandbox"); // Pls dont remove

            // Authenticate with PayPal
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

			// If payout to other user fails, stop the transaction
			if (!PayOut(accessToken,dto)) return false;

            // Make an API call
            var payment = Payment.Create(apiContext, new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },

                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = dto.Description,
                        amount = new Amount
                        {
                            currency = "DKK",
                            total = dto.Amount,
                            details = new Details
                            {
                                /*
                                tax = "15",
                                shipping = "10",
                                subtotal = "75"
                                */
                            }
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = "http://mysite.com/return",
                    cancel_url = "http://mysite.com/cancel"
                }
                });

            return true;

        }

		public bool PayOut(String accessToken, PaymentDto dto)
		{
			var apiContext = new APIContext(accessToken);

			try { 
				var payout = new Payout
				{
					sender_batch_header = new PayoutSenderBatchHeader
					{
						sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
						email_subject = "You have a payment",
						recipient_type = PayoutRecipientType.EMAIL
					},
					items = new List<PayoutItem>
					{
						new PayoutItem
						{
							recipient_type = PayoutRecipientType.EMAIL,
							amount = new Currency
							{
								value = "0.00", // Set to 0 because senders balance is zero
								currency = "DKK"
							},
							receiver = "christoffer.nissen-buyer@me.com",
							note = "Thank you.",
							sender_item_id = "item_1"
						}
					}

				};

				var createdPayout = payout.Create(apiContext,false);

			} catch (Exception e)
			{
				return false;
			}
			return true;
		}
    }
}
