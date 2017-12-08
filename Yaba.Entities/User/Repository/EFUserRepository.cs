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
		public Task<bool> AddFriend(Guid myId, Guid otherId)
		{
			throw new NotImplementedException();
		}

		public Task<Guid> CreateUser(UserCreateDto user)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<UserDetailsDto>> FindAll()
		{
			throw new NotImplementedException();
		}

		public Task<UserDetailsDto> FindUser(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Update(Guid userId)
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
					// TODO: dispose managed state (managed objects).
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
