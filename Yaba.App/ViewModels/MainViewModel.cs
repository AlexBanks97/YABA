using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using System.Linq;
using System.Net.Http;
using Yaba.Common.Payment;
using System;

namespace Yaba.App.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IAuthenticationHelper _authenticationHelper;

		private User _user;
		public User User
		{
			get => _user;
			private set
			{
				_user = value;
				Name = User?.IdentityToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value
				       ?? User?.IdentityToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
				IsLoggedIn = _user != null;
				OnPropertyChanged();
			}
		}

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public ICommand SignInOutCommand { get; }
		public ICommand PayWithPayPal { get; }

		private bool _isLoggedIn;
		public bool IsLoggedIn
		{
			get => _isLoggedIn;
			private set
			{
				_isLoggedIn = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel(IAuthenticationHelper authenticationHelper, PaymentRepository paymentRepository)
		{
			_authenticationHelper = authenticationHelper;

			SignInOutCommand = new RelayCommand(_ =>
			{
				if (IsLoggedIn)
				{
					SignOut();
				}
				else
				{
					SignIn();
				}
			});

			PayWithPayPal = new RelayCommand( async _ =>
			{

				// Ask API to create payment

				PaymentDto dto = new PaymentDto()
				{
					Amount = "100.00",
					PaymentProvider = "PayPal",
					Token = "tok_visa",
					RecipientEmail = "christoffer.nissen-buyer@me.com"
				};

				var xx = (await _authenticationHelper.GetAccountAsync())?.AccessToken;
				// Receive linkOrMessage, and open accept link if link
				var uriOrSuccess = await paymentRepository.Pay(dto, xx.RawData);

			if (uriOrSuccess.Equals("Success"))
			{
				// Stripe payment
				// Show Success Screen
			}
			else if (uriOrSuccess.Contains("http"))
				{
					//Extract if link
					Uri targetUri = new Uri(uriOrSuccess);

					// Open link in webView

					// Show success screen if success
					// Upon redirection from paypal, the payment is executed and payout is issued.



				}
			else
				{
					// Failure, show sad screen
				}

			});


		}

		public async Task Initialize()
		{
			SignIn();
		}

		private async void SignIn()
		{
			User = await _authenticationHelper.GetAccountAsync();
		}

		private async void SignOut()
		{
			await _authenticationHelper.SignOutAsync();
			User = null;
		}
	}
}
