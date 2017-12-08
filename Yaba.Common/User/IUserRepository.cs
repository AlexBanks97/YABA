using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.User.DTO;

namespace Yaba.Common
{
    public interface IUserRepository : IDisposable
    {
		Task<bool> AddFriend(Guid myId, Guid otherId);

		Task<Guid> CreateUser(UserCreateDto user);

		Task<UserDetailsDto> FindUser(Guid userId);

		Task<ICollection<UserSimpleDto>> FindAll();

		Task<bool> Update(Guid userId);

		Task<bool> Delete(Guid userId);
    }
}
