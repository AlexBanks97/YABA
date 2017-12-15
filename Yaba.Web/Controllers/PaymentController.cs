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

	    private readonly IPaymentRepository _paymentRepository;


		public PaymentController(IPaymentRepository paymentRepository)
		{
			_paymentRepository = paymentRepository;
		}

		// POST: api/Payment
		[HttpPost]
        public async Task<IActionResult> Post([FromBody]PaymentDto payment)
        {
	        if (!ModelState.IsValid)
	        {
		        return BadRequest(ModelState);
	        }
	        var success = _paymentRepository.Pay(payment);
	        if (success)
	        {
		        return Ok();
	        }
	        return Forbid();
        }


    }
}
