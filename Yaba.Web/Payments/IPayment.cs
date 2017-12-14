using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common.Payment;

namespace Yaba.Web.Payments
{
    public interface IPayment
    {
	    bool Pay(StripePaymentDto pay);
    }
}
