using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Yaba.App.Models
{
	public interface IAuthenticationHelper
	{
		Task<User> SignInAsync();
		Task SignOutAsync();
		Task<string> AcquireTokenAsync();
		Task<User> GetAccountAsync();
	}
}
