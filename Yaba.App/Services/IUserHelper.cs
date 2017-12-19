using System.Threading.Tasks;
using Yaba.Common.User.DTO;

namespace Yaba.App.Services
{
	public interface IUserHelper
	{
		Task<UserDto> GetCurrentUser();
		Task SignOut();
	}
}
