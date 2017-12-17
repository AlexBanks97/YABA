using System;
using System.Collections.Generic;

using Yaba.Common.Payment;

using PayPal.Api;

namespace Yaba.Web.Payments
{
    public class PaypalPay : IPaymentRepository
    {
        // OLD: New methods (GetApprovalUrl/Execute Payment)
       public bool Pay(PaymentDto dto){

            APIContext apiContext = GetApiContext();

            // Make an API call
            var paymentobj = new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                id = Guid.NewGuid().ToString().Substring(0, 8),
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = dto.Description,
                        amount = new Amount
                        {
                            currency = "DKK",
                            total = dto.Amount,
                            details = new Details{}
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = "http://mysite.com/return",
                    cancel_url = "http://mysite.com/cancel"
                }
            };

            var payment = paymentobj.Create(apiContext);

			return true;

        }

        public String GetApprovalUri(PaymentDto dto)
		{
            APIContext apiContext = GetApiContext();

			// Create payment object to be paied.
			var paymentobj = new Payment
			{
				intent = "sale",
				payer = new Payer
				{
					payment_method = "paypal"
				},
				id = Guid.NewGuid().ToString().Substring(0, 8),
				transactions = new List<Transaction>
				{
					new Transaction
					{
						description = dto.Description,
						amount = new Amount
						{
							currency = "DKK",
							total = dto.Amount,
                            details = new Details{}
						}
					}
				},
				redirect_urls = new RedirectUrls
				{
					return_url = "http://localhost:5000/api/payment/", // Change to production before release
					cancel_url = "http://mysite.com/cancel"
				}
			};

			var payment = paymentobj.Create(apiContext);

            // Extract approval_url
			var links = payment.links.GetEnumerator();
			while (links.MoveNext())
			{
				var link = links.Current;
				if (link.rel.ToLower().Trim().Equals("approval_url"))
				{
					return link.href;
				}
			}

			return "  ";
		}

		public String executePayment(String paymentId, String payerId)
		{
            APIContext apiContext = GetApiContext();

            // Execute the payment.
			var paymentExecution = new PaymentExecution() { payer_id = payerId };
			var payment = new Payment() { id = paymentId };
			var executedPayment = payment.Execute(apiContext, paymentExecution);

			return "Success";
		}

		public bool PayOut(PaymentDto dto)
		{
            APIContext apiContext = GetApiContext();

			try { 
				var payout = new Payout
				{
					sender_batch_header = new PayoutSenderBatchHeader
					{
						sender_batch_id = "batch_" + Guid.NewGuid().ToString().Substring(0, 8),
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

        APIContext GetApiContext()
        {
            var config = new Dictionary<String, String>();
            config.Add("clientId", "AVW3VSprOUqMbB3FKDrMFH2e504IO6h3Qss9LmGjq0kcfkj6glmqqD7jMCbxIIFeqGrDcy7B2dt9_u_N");
            config.Add("clientSecret", "ECXUaE-0M5RCk3ut-enH-SFZrHMi70R8YEUgLJFS4nnd0A973fE8YPo9cuHx1jyINStSDl6P6gkjIJlI");
            config.Add("mode", "sandbox"); // Pls dont remove

            // Authenticate with PayPal
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            if (apiContext.HTTPHeaders == null)
            {
                apiContext.HTTPHeaders = new Dictionary<string, string>();
            }
            apiContext.HTTPHeaders["Content-Type"] = "application/json";

            return apiContext;
        }

    }
}
