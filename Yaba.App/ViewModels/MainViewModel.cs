using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using System.Linq;
using Yaba.App.Services;
using System.Net.Http;
using Yaba.Common.Payment;
using System;
using Yaba.Common.User.DTO;

namespace Yaba.App.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IAuthenticationHelper _authenticationHelper;
		private readonly IUserHelper _userHelper;



		private UserDto _user;
		public UserDto User
		{
			get => _user;
			set
			{
				_user = value;
				OnPropertyChanged();
			}
		}
		
		public ICommand PayWithPayPal { get; }
		public ICommand SignInOutCommand { get; }

		
		public MainViewModel(IAuthenticationHelper authenticationHelper, PaymentRepository paymentRepository, IUserHelper userHelper)
		{
			_authenticationHelper = authenticationHelper;
			_userHelper = userHelper;

			SignInOutCommand = new RelayCommand(async o =>
			{
				if (User != null)
				{
					await SignOut();
				}
				else
				{
					await SignIn();
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

			});
		}

		public async Task Initialize()
		{
			await SignIn();
		}

		private async Task SignIn()
		{
			User = await _userHelper.GetCurrentUser();
		}

		private async Task SignOut()
		{
			await _userHelper.SignOut();
			User = null;
		}
	}
}
