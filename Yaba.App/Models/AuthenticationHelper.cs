using System.Threading.Tasks;
using Windows.Storage;
using Auth0.OidcClient;
using System.IdentityModel.Tokens.Jwt;

namespace Yaba.App.Models
{
	public class AuthenticationHelper : IAuthenticationHelper
	{
		private const string SettingsUserAccessToken = "user:access_token";
		private const string SettingsUserIdentityToken = "user:identity_token";

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
			});

			var loginResult = await client.LoginAsync(new {audience = "https://yaba.dev"});

			if (!loginResult.IsError)
			{
				var user = new User(loginResult.AccessToken, loginResult.IdentityToken);
				_appSettings.Values[SettingsUserAccessToken] = loginResult.AccessToken;
				_appSettings.Values[SettingsUserIdentityToken] = loginResult.IdentityToken;
				return user;
			}
			return null;
		}

		public async Task<User> GetAccountAsync()
		{
			var accessToken = _appSettings.Values[SettingsUserAccessToken] as string;
			var identityToken = _appSettings.Values[SettingsUserIdentityToken] as string;
			if (!string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(identityToken))
			{
				return new User(accessToken, identityToken);
			}
			return await SignInAsync();
		}

		public async Task SignOutAsync()
		{
			_appSettings.Values[SettingsUserAccessToken] = null;
			_appSettings.Values[SettingsUserIdentityToken] = null;
		}

		public async Task<string> AcquireTokenAsync()
		{
			return _appSettings.Values[SettingsUserAccessToken] as string;
		}
	}
}
