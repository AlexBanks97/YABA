using System.Threading.Tasks;
using Windows.Storage;
using Auth0.OidcClient;
using System.IdentityModel.Tokens.Jwt;
using Yaba.App.Services;

namespace Yaba.App.Models
{
	public class AuthenticationHelper : IAuthenticationHelper
	{
		private const string SettingsUserAccessToken = "user:access_token";
		private const string SettingsUserIdentityToken = "user:identity_token";

		private readonly ApplicationDataContainer _appData;
		private readonly Auth0Client _client;

		public AuthenticationHelper(AppConstants constants)
		{
			_client = new Auth0Client(new Auth0ClientOptions
			{
				Domain = constants.Auth0Domain,
				ClientId = constants.Auth0ClientId,
				Scope = "openid email profile",
			});

			_appData = ApplicationData.Current.RoamingSettings;
		}


		public async Task<AppUser> SignInAsync()
		{
			var loginResult = await _client.LoginAsync(new {audience = "https://yaba.dev"});

			if (!loginResult.IsError)
			{
				var user = new AppUser(loginResult.AccessToken, loginResult.IdentityToken);
				_appData.Values[SettingsUserAccessToken] = loginResult.AccessToken;
				_appData.Values[SettingsUserIdentityToken] = loginResult.IdentityToken;
				return user;
			}
			return null;
		}

		public async Task<AppUser> GetAccountAsync()
		{
			var accessToken = _appData.Values[SettingsUserAccessToken] as string;
			var identityToken = _appData.Values[SettingsUserIdentityToken] as string;
			if (!string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(identityToken))
			{
				return new AppUser(accessToken, identityToken);
			}
			return await SignInAsync();
		}

		public async Task SignOutAsync()
		{
			_appData.Values[SettingsUserAccessToken] = null;
			_appData.Values[SettingsUserIdentityToken] = null;
		}

		public async Task<string> AcquireTokenAsync()
		{
			return _appData.Values[SettingsUserAccessToken] as string;
		}
	}
}
