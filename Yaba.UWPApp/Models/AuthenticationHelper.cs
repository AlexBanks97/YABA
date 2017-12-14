using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Services.Maps;
using Windows.Storage;
using Auth0.OidcClient;

namespace Yaba.UWPApp.Models
{
	public class AuthenticationHelper : IAuthenticationHelper
	{
		/*
		public string Tenant => "ondfisk.onmicrosoft.com";

        public string ClientId => "48ad45dd-7c9c-4e94-b2e2-c3b09aae44a3";

        public string RedirectUri => $"ms-appx-web://Microsoft.AAD.BrokerPlugIn/{WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host.ToUpper()}";

        public string Instance => "https://login.microsoftonline.com/";

        public string WebAccountProviderId => "https://login.microsoft.com";

        public string ApiResource => "https://ondfisk.onmicrosoft.com/BDSA2017.Lecture11.Web";

        public Uri ApiBaseAddress => new Uri("https://bdsa2017.azurewebsites.net/");

		public string Authority => $"{Instance}{Tenant}";
*/
		private const string SettingsUser = "user";

		private readonly ApplicationDataContainer _appSettings;

		public AuthenticationHelper()
		{
			_appSettings = ApplicationData.Current.RoamingSettings;
		}


		public async Task<User> SignInAsync()
		{
			var client = new Auth0Client(new Auth0ClientOptions {
				Domain = "praffn.eu.auth0.com",
				ClientId = "DRDKy6mF0gWtMFkExlIt0HY2DvqkxPgO",
				Scope = "openid email profile",
				//ClientSecret = "a2k9vxXuZTXoapriCmugGWNzFxCJnBcR0WwXXFFJxGZuG9E_88evNUQeLKkX_Esw"
			});

			var loginResult = await client.LoginAsync(new {audience = "https://yaba.dev"});

			if (!loginResult.IsError)
			{
				var user = new User
				{
					AccessToken = loginResult.AccessToken,
					IdentityToken = loginResult.IdentityToken
				};
				_appSettings.Values[SettingsUser] = user;
				return user;
			}
			return null;
		}

		public async Task<User> GetAccountAsync()
		{
			if (_appSettings.Values["user"] is User user)
			{
				return user;
			}
			return await SignInAsync();
		}

		public Task SignOutAsync()
		{
			throw new System.NotImplementedException();
		}

		public async Task<string> AcquireTokenAsync()
		{
			var user = await GetAccountAsync();
			return user?.AccessToken;
		}
	}
}
