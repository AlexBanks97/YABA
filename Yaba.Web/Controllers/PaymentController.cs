using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Payment;
using Yaba.Web.Payments;

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

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var b = Request.QueryString.Value;

			return Ok(b);
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
