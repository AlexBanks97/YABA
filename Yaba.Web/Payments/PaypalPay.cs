using System;
using System.Collections.Generic;

using Yaba.Common.Payment;
using Yaba.Common.Payment.DTO;
using PayPal.Api;

namespace Yaba.Web.Payments
{
    public class PaypalPay 
    {
        
        public PaypalPay()
        {
            


        }

        public bool Pay(PaypalPaymentDto dto){
            
            // Authenticate with PayPal
            //var config = ConfigManager.Instance.GetProperties();


            var config = new Dictionary<String, String>();
            config.Add("clientId","AVW3VSprOUqMbB3FKDrMFH2e504IO6h3Qss9LmGjq0kcfkj6glmqqD7jMCbxIIFeqGrDcy7B2dt9_u_N");
            config.Add("clientSecret", "ECXUaE-0M5RCk3ut-enH-SFZrHMi70R8YEUgLJFS4nnd0A973fE8YPo9cuHx1jyINStSDl6P6gkjIJlI");
            config.Add("mode","sandbox");


            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

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
                        invoice_number = "101",
                        amount = new Amount
                        {
                            currency = "USD",
                            total = "100.00",
                            details = new Details
                            {
                                tax = "15",
                                shipping = "10",
                                subtotal = "75"
                            }
                        },
                        item_list = new ItemList
                        {
                            items = new List<Item>
                            {
                                new Item
                                {
                                    name = "Item Name",
                                    currency = "USD",
                                    price = "15",
                                    quantity = "5",
                                    sku = "sku"
                                }
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
    }
}
