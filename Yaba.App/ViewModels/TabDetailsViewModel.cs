using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using Yaba.Common.Payment;

namespace Yaba.App.ViewModels
{
	public class TabDetailsViewModel
	{
		public StripePaymentViewModel StripePaymentViewModel { get; set; }

		private readonly IPaymentRepository paymentRepository;

		public ICommand PayWithStripe { get; }

		public TabDetailsViewModel(IPaymentRepository repo)
		{
			StripePaymentViewModel = new StripePaymentViewModel();
			paymentRepository = repo;

			PayWithStripe = new RelayCommand(async e =>
			{
				if (!(e is StripePaymentViewModel cc)) return;
				if (!cc.VerifyCreditCardInfo())
				{
					return;
				}
				var payment = new PaymentDto
				{
					Amount = StripePaymentViewModel.Amount.ToString(),
					PaymentProvider = "Stripe",
					Token = StripeTokenHandler.CardToToken(cc),
				};
				//tokenize the shit
				//do payment
			});
		}
	}
}
