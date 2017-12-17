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
                    success = new PaypalPay().PayOut(payment);
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
        public async Task<IActionResult> Get([FromQuery] String payerId, [FromQuery]String paymentId)
		{
			var s = "PayerId: " + payerId + ", PaymentId: " + paymentId;
            s = new PaypalPay().ExecutePayment(paymentId,payerId);
			return Ok(s);

		}

        [HttpGet("{amount},{description},{PaymentProvider},{RecipientEmail}")]
        public async Task<IActionResult> GetCreateUri(String amount, String description, String PaymentProvider, String RecipientEmail)
		{
			
			PaymentDto dto = new PaymentDto()
			{
                Amount = amount,
                Description = description,
                PaymentProvider = PaymentProvider,
                RecipientEmail = RecipientEmail

			};


			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			String success = "";
			switch (dto.PaymentProvider)
			{
				case "PayPal":
                    success = GetApprovalUri(new PaypalPay(), dto);
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

        private String GetApprovalUri(IPaymentRepository repo, PaymentDto payment)
		{
            return repo.GetApprovalUri(payment);
		}


	}
}
