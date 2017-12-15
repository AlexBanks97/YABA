using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Payment
{
    public class StripePaymentDto
    {
		[Required]
	    public decimal Amount { get; set; }
	    public string Description { get; set; }
		[Required]
	    public string Token { get; set; }
    }
}
