using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using System.Linq;

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

		public MainViewModel(IAuthenticationHelper authenticationHelper)
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
		}

		public async Task Initialize()
		{
			SignIn();
		}

		private async void SignIn()
		{
			User = await _authenticationHelper.GetAccountAsync();}

		private async void SignOut()
		{
			await _authenticationHelper.SignOutAsync();
			User = null;
		}
	}
}
