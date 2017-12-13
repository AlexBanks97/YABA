using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.User.DTO;

namespace Yaba.Common
{
    public interface IUserRepository : IDisposable
    {

		Task<String> CreateUser(UserCreateDto user);

		Task<UserDetailsDto> FindUser(String userId);

		Task<ICollection<UserSimpleDto>> FindAll();

		Task<bool> Update(String userId);

		Task<bool> Delete(String userId);
    }
}
