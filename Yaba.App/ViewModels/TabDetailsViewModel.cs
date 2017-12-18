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
	public class TabDetailsViewModel : ViewModelBase
	{
		public StripePaymentViewModel StripePaymentViewModel { get; set; }
		public PayPalPaymentViewModel PayPalPaymentViewModel { get; set; }
		private readonly PaymentRepository paymentRepository;

		public ICommand PayWithStripe { get; }
		public ICommand PayWithPayPal { get; }

		private readonly IAuthenticationHelper _helper;

		private string _approvalUri = "";
		public string ApprovalUri
		{
			get => _approvalUri;
			set
			{
				_approvalUri = value;
				OnPropertyChanged();
			}
		}


		public TabDetailsViewModel(PaymentRepository repo, IAuthenticationHelper helper)
		{
			StripePaymentViewModel = new StripePaymentViewModel();
			if(PayPalPaymentViewModel == null) new PayPalPaymentViewModel();
			
			paymentRepository = repo;
			_helper = helper;

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

			PayWithPayPal = new RelayCommand(async _ =>
			{

				if (!(_ is PayPalPaymentViewModel cc)) return;


				// Get amount and email

				PaymentDto dto = new PaymentDto()
				{
					Amount = PayPalPaymentViewModel.Amount.ToString(),
					PaymentProvider = "PayPal",
					RecipientEmail = PayPalPaymentViewModel.Email
				};

				var xx = (await _helper.GetAccountAsync())?.AccessToken;
				// Ask API to create payment
				// Receive linkOrMessage, and open accept link if link
				var uriOrSuccess = await paymentRepository.Pay(dto, xx.RawData);

				if (uriOrSuccess.Equals("true"))
				{
					// Successful stripe payment



					// Show success screen

				}
				else if (uriOrSuccess.Equals("Failure..."))
				{
					Uri targetUri = new Uri(uriOrSuccess);
					ApprovalUri = targetUri.ToString();

					// Open webview and load uri
					

				}
				else
				{
					// Failure

					// Show failrue screen, and ask user to try again
				}

			});
		}
	}
}
