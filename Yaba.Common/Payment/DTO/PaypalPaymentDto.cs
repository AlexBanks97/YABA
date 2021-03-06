﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Payment.DTO
{
    public class PaypalPaymentDto
    {
        [Required]
        public String Amount { get; set; }
        public string Description { get; set; }
    }
}
