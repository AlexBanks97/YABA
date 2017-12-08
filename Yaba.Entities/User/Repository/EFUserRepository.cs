using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.User.DTO;

namespace Yaba.Entities.User.Repository
{
	public class EFUserRepository : IUserRepository
	{
		private readonly IYabaDBContext _context;

		public EFUserRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<bool> AddFriend(Guid myId, Guid otherId)
		{
			throw new NotImplementedException();
		}

		public async Task<Guid> CreateUser(UserCreateDto user)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(Guid userId)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<UserSimpleDto>> FindAll()
		{
			throw new NotImplementedException();
		}

		public async Task<UserDetailsDto> FindUser(Guid userId)
		{
			var user = _context.Users.SingleOrDefault(u => u.Id == userId);
			if (user == null) return null;
			var dto = new UserDetailsDto
			{
				Id = user.Id,
				Name = user.Name,
			};
			if (user.Friends != null)
			{
				dto.Friends = user.Friends.Select(u => new UserSimpleDto
				{
					Id = u.Id,
					Name = u.Name,
				}).ToList();
			}

			return dto;
		}

		public async Task<bool> Update(Guid userId)
		{
			throw new NotImplementedException();
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~EFUserRepository() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
