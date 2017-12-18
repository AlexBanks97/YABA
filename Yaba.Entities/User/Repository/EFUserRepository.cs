using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

		public async Task<UserDto> CreateUser(UserCreateDto user)
		{
			var entity = new UserEntity
			{
				FacebookId = user.FacebookId,
				Name = user.Name,
			};
			_context.Users.Add(entity);
			await _context.SaveChangesAsync();
			return entity.ToUserDto();
		}

		public async Task<UserDto> Find(Guid userId)
		{
			var entity = _context.Users
				.SingleOrDefault(u => u.Id == userId);
			if (entity == null) return null;
			return new UserDto
			{
				Id = entity.Id,
				Name = entity.Name,
				FacebookId = entity.FacebookId,
			};
		}

		public async Task<UserDto> FindFromFacebookId(string facebookId)
		{
			var entity = _context.Users
				.SingleOrDefault(u => u.FacebookId == facebookId);
			if (entity == null) return null;
			return new UserDto
			{
				Id = entity.Id,
				Name = entity.Name,
				FacebookId = entity.FacebookId,
			};
		}

		public async Task<ICollection<UserDto>> FindAll()
		{
			return _context.Users.Select(u => new UserDto
			{
					Name = u.Name,
					Id = u.Id,
					FacebookId = u.FacebookId,
				}).ToList();
		}

		public async Task<bool> Update(UserDto user)
		{
			var entity = _context.Users
				.SingleOrDefault(u => u.Id == user.Id);
			if (entity == null) return false;

			entity.Name = user.Name ?? entity.Name;
			entity.FacebookId = user.FacebookId ?? entity.FacebookId;
			return true;
		}

		public async Task<bool> Delete(Guid userId)
		{
			var entity = _context.Users.SingleOrDefault(u => u.Id == userId);
			if (entity == null) return false;
			_context.Users.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
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
