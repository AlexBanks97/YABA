using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.Payment
{
    public class StripePayment
    {
	    public decimal Amount { get; set; }
	    public string Description { get; set; }
	    public string Token { get; set; }
    }
}
