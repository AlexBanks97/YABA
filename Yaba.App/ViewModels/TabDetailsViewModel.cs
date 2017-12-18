using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using Yaba.Common.Payment;

namespace Yaba.App.ViewModels
{
	public class TabDetailsViewModel : ViewModelBase
	{
		public StripePaymentViewModel StripePaymentViewModel { get; set; }

		private readonly PaymentRepository paymentRepository;

		public ICommand PayWithStripe { get; }

		public TabDetailsViewModel(PaymentRepository repo)
		{
			StripePaymentViewModel = new StripePaymentViewModel();
			paymentRepository = repo;

			PayWithStripe = new RelayCommand(async e =>
			{
				Debug.WriteLine("blah");
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
				await repo.Pay(payment, "");
			});
		}
	}
}
