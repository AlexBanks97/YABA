using System.IdentityModel.Tokens.Jwt;

namespace Yaba.App.Models
{
	public class User
	{
		public JwtSecurityToken AccessToken { get; private set; }
		public JwtSecurityToken IdentityToken { get; private set; }

		public User(string accessToken, string identityToken)
		{
			var handler = new JwtSecurityTokenHandler();
			AccessToken = handler.ReadJwtToken(accessToken);
			IdentityToken = handler.ReadJwtToken(identityToken);
		}
	}
}
