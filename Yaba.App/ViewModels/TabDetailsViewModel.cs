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
		public IView View { get; set; }
		public StripePaymentViewModel StripePaymentViewModel { get; set; }
		public PayPalPaymentViewModel PayPalPaymentViewModel { get; set; }
		private readonly PaymentRepository paymentRepository;

		public ICommand PayWithStripe { get; }
		public ICommand PayWithPayPal { get; }

		private readonly IAuthenticationHelper _helper;

		private Uri _approvalUri = new Uri("http://www.microsoft.com");
		public Uri ApprovalUri
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
			PayPalPaymentViewModel = new PayPalPaymentViewModel();
			
			paymentRepository = repo;
			_helper = helper;

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
			else if (uriOrSuccess.Contains("http"))
				{
					Uri targetUri = new Uri(uriOrSuccess);
					ApprovalUri = targetUri;

					// Open webview and load uri

					View.OpenUriInWebView(ApprovalUri);
					

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
