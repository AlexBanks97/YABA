using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.User;
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

		public async Task<String> CreateUser(UserCreateDto user)
		{
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name
            };

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
            return userEntity.Id;
		}

		public async Task<bool> Delete(String userId)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<UserSimpleDto>> FindAll()
		{
            return _context.Users.Select(b => new UserSimpleDto
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();
		}

		public async Task<UserDetailsDto> FindUser(String userId)
		{
			var user = _context.Users.SingleOrDefault(u => u.Id == userId);
			if (user == null) return null;
			var dto = new UserDetailsDto
			{
				Id = user.Id,
				Name = user.Name,
			};

			return dto;
		}

		public async Task<bool> Update(String userId)
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
