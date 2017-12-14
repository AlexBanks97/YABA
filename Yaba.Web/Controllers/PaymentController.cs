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

	    private readonly IPayment _payment;


		public PaymentController(IPayment payment)
		{
			_payment = payment;
		}

		// POST: api/Payment
		[HttpPost]
        public async Task<IActionResult> Post([FromBody]StripePaymentDto payment)
        {
	        if (!ModelState.IsValid)
	        {
		        return BadRequest(ModelState);
	        }
	        var success = _payment.Pay(payment);
	        if (success)
	        {
		        return Ok();
	        }
	        return Forbid();
        }
    }
}
