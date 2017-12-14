using System.IdentityModel.Tokens.Jwt;

namespace Yaba.UWPApp.Models
{
	public class User
	{
		public string AccessToken { get; set; }
		public string IdentityToken { get; set; }
	}
}
