namespace Yaba.Common.Payment
{
    public interface IPaymentRepository
    {
	    bool Pay(PaymentDto pay);
    }
}
