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

	    private Boolean pay(IPaymentRepository repo, PaymentDto payment)
	    {
		    return repo.Pay(payment);
	    }


	}
}
