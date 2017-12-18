using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using System.Linq;
using Yaba.App.Services;

namespace Yaba.App.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IAuthenticationHelper _authenticationHelper;
		private readonly IUserHelper _userHelper;

		private AppUser _appUser;
		public AppUser AppUser
		{
			get => _appUser;
			private set
			{
				_appUser = value;
				Name = AppUser?.IdentityToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value
				       ?? AppUser?.IdentityToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
				IsLoggedIn = _appUser != null;
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

		public MainViewModel(IAuthenticationHelper authenticationHelper, IUserHelper userHelper)
		{
			_authenticationHelper = authenticationHelper;
			_userHelper = userHelper;

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
			var user = await _userHelper.GetCurrentUser();
			Debug.WriteLine(user);
		}

		private async void SignIn()
		{
			AppUser = await _authenticationHelper.GetAccountAsync();}

		private async void SignOut()
		{
			await _authenticationHelper.SignOutAsync();
			AppUser = null;
		}
	}
}
