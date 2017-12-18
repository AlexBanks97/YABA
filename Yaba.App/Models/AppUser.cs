using System.IdentityModel.Tokens.Jwt;
using Yaba.Common.User.DTO;

namespace Yaba.App.Models
{
	public class AppUser
	{
		public JwtSecurityToken AccessToken { get; private set; }
		public JwtSecurityToken IdentityToken { get; private set; }

		public AppUser(string accessToken, string identityToken)
		{
			var handler = new JwtSecurityTokenHandler();
			AccessToken = handler.ReadJwtToken(accessToken);
			IdentityToken = handler.ReadJwtToken(identityToken);
		}
	}
}
