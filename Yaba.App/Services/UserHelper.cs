using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Yaba.App.Models;
using Yaba.Common;
using Yaba.Common.User.DTO;

namespace Yaba.App.Services
{
	public class UserHelper : IUserHelper
	{
		private static UserDto _currentUser;

		private readonly IAuthenticationHelper _auth;
		private readonly IUserRepository _userRepo;

		public UserHelper(IAuthenticationHelper auth, IUserRepository userRepo)
		{
			_auth = auth;
			_userRepo = userRepo;
		}

		public async Task<UserDto> GetCurrentUser()
		{
			if (_currentUser != null) return _currentUser;
			var uacc = await _auth.GetAccountAsync();
			var fbId = uacc.IdentityToken.Subject
				?.Split('|')[1];
			_currentUser = await _userRepo.FindFromFacebookId(fbId);
			return _currentUser;
		}
	}
}
