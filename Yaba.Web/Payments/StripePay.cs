using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Yaba.Common.Payment;

namespace Yaba.Web.Payments
{
    public class StripePay : IPaymentRepository
    {
	    public bool Pay(PaymentDto payment)
	    {
            var payAmount = (int)(Decimal.Parse(payment.Amount) * 100);

		    var ChargeService = new StripeChargeService();

		    var ChargeOptions = new StripeChargeCreateOptions
		    {
			    Amount = payAmount,
				Currency = "dkk",
			    SourceTokenOrExistingSourceId = payment.Token,
			    Description = payment.Description,
			    Capture = true
		    };
			try
		    {
				ChargeService.Create(ChargeOptions);
			    return true;
		    }
		    catch (StripeException e)
		    {
			    switch (e.StripeError.ErrorType)
			    {
				    case "card_error":
					    Console.WriteLine("   Code: " + e.StripeError.Code);
					    Console.WriteLine("Message: " + e.StripeError.Message);
					    break;
				    case "api_connection_error":
					    break;
				    case "api_error":
					    break;
				    case "authentication_error":
					    break;
				    case "invalid_request_error":
					    break;
				    case "rate_limit_error":
					    break;
				    case "validation_error":
					    break;
				    default:
					    // Unknown Error Type
					    break;
			    }
				return false;
		    }

			
	    }
        public String GetApprovalUri(PaymentDto dto)
		{
			return "";
		}
	}
}
