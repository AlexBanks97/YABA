using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.User.DTO;

namespace Yaba.Common
{
    public interface IUserRepository : IDisposable
    {

		Task<Guid> CreateUser(UserCreateDto user);

		Task<UserDto> Find(Guid userId);

	    Task<UserDto> FindFromFacebookId(string facebookId);

		Task<ICollection<UserDto>> FindAll();

		Task<bool> Update(UserDto user);

		Task<bool> Delete(Guid userId);
    }
}
