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

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] PaymentDto payment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			String message;
			switch (payment.PaymentProvider)
			{
				case "PayPal":
					message = new PaypalPay().PayOut(payment);
					break;
				default:
					message = "false";
					break;
			}

			if (message == "true")
			{
				return Ok();
			}
			return Forbid();

		}

		// For paypal
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] String payerId, [FromQuery]String paymentId)
		{
			if (payerId == null || paymentId == null)
			{
				return BadRequest();
			}
			var s = "PayerId: " + payerId + ", PaymentId: " + paymentId;
			s = new PaypalPay().ExecutePayment(paymentId, payerId);

			return Ok(s);

		}

		[HttpPost]
		[Route("/api/payment/GetUrl")]
        public async Task<IActionResult> GetCreateUri([FromBody] PaymentDto dto)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			String message = "";
			switch (dto.PaymentProvider)
			{
				case "PayPal":
                    message = pay(new PaypalPay(), dto);
                    break;
				
				case "Stripe":
                    message = pay(new StripePay(), dto);
                    break;
			}

            return Ok(message);
			
		}

	    private String pay(IPaymentRepository repo, PaymentDto payment)
	    {
            return repo.Pay(payment);
	    }

	}
}
