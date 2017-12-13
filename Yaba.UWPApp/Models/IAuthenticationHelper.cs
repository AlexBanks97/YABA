using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Yaba.UWPApp.Models
{
	public interface IAuthenticationHelper
	{
		Task<User> SignInAsync();
		Task SignOutAsync();
		Task<string> AcquireTokenAsync();
		Task<User> GetAccountAsync();
	}
}
