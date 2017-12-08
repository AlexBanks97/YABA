using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.User.DTO;

namespace Yaba.Common
{
    public interface IUserRepository
    {
		Task<UserDetailsDto> AddFriend(Guid myId, Guid otherId);

		Task<UserCreateDto> CreateUser(UserCreateDto user);

		Task<UserDetailsDto> FindUser(Guid userId);

		Task<UserDetailsDto> FindAll();

		Task<UserDetailsDto> Update(Guid userId);

		Task<bool> Delete(Guid userId);
    }
}
