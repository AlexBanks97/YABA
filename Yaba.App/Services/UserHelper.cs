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

			var user = await _userRepo.FindFromFacebookId(fbId);
			if (user == null)
			{
				var name = uacc.IdentityToken.Claims
					.FirstOrDefault(c => c.Type == "name")
					?.Value;
				var dto = new UserCreateDto
				{
					Name = name,
					FacebookId = fbId,
				};
				user = await _userRepo.CreateUser(dto);
			}

			_currentUser = user;
			return _currentUser;
		}
	}
}
