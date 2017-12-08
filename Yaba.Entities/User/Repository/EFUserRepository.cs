using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.User.DTO;

namespace Yaba.Entities.User.Repository
{
	public class EFUserRepository : IUserRepository
	{
		public Task<UserCreateDto> CreateUser(UserCreateDto user)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<UserDetailsDto> FindAll()
		{
			throw new NotImplementedException();
		}

		public Task<UserDetailsDto> FindUser(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<UserDetailsDto> Update(Guid userId)
		{
			throw new NotImplementedException();
		}
	}
}
