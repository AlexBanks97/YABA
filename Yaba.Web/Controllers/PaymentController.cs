using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Payment;
using Yaba.Web.Payments;
using PayPal.Api;

namespace Yaba.Web.Controllers
{
    [Route("api/Payment")]
    public class PaymentController : Controller
    {
		// POST: api/Payment
		[HttpPost]
        public async Task<IActionResult> Post([FromBody]PaymentDto payment)
        {
			if (!ModelState.IsValid)
		    {
			    return BadRequest(ModelState);
		    }
		    bool success;
		    switch (payment.PaymentProvider)
		    {
			    case "PayPal":
				    success = pay(new PaypalPay(), payment);
				    break;
			    case "Stripe":
				    success = pay(new StripePay(), payment);
				    break;
			    default:
				    success = false;
				    break;
		    }
		    if (success)
		    {
			    return Ok();
		    }
		    return Forbid();
	    }

		// For paypal
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			// Extract payerId and paymentId from querystring
			var b = Request.QueryString.Value;
			var x = b.Substring(1, b.Length-1); // lose ?
			var xx = x.Split("&");
			var payerId = xx[2].Split("=")[1];
			var paymentId = xx[0].Split("=")[1];

			var s = "PayerId: " + payerId + ", PaymentId: " + paymentId;

			PaypalPay ppp = new PaypalPay();
			s = ppp.executePayment(paymentId,payerId);

			// If s == success, Payout to other user

			return Ok(s);
		}

		[HttpGet("{amount}")]
		public async Task<IActionResult> GetCreateUri()
		{
			
			PaymentDto dto = new PaymentDto()
			{
				Amount = "100.00",
				Description = "test",
				PaymentProvider = "PayPal"

			};

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			String success = "";
			switch (dto.PaymentProvider)
			{
				case "PayPal":
					success = payTwo(new PaypalPay(), dto);
					break;
				default:
					break;
			}
			return Ok(success);
			
		}

		

	    private Boolean pay(IPaymentRepository repo, PaymentDto payment)
	    {
		    return repo.Pay(payment);
	    }

		private String payTwo(IPaymentRepository repo, PaymentDto payment)
		{
			return repo.PayTwo(payment);
		}


	}
}
