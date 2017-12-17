using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Payment
{
    public class PaymentDto
    {
		[Required]
	    public String Amount { get; set; }
        [Required]
        public string PaymentProvider { get; set; }
        public string Token { get; set; }
        public string RecipientEmail { get; set; }
    }
}
