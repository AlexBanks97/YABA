using System;

namespace Yaba.Common.Payment
{
    public interface IPaymentRepository
    {
	    String Pay(PaymentDto pay);

	}
}
