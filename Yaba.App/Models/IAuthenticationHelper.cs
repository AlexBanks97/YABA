using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Yaba.App.Models
{
	public interface IAuthenticationHelper
	{
		Task<AppUser> SignInAsync();
		Task SignOutAsync();
		Task<string> AcquireTokenAsync();
		Task<AppUser> GetAccountAsync();
	}
}
