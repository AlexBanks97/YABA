using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Common.Tab;
using Yaba.Common.Tab.TabItemDTOs;
using Yaba.Common.Tab.TabItemDTOs.Category;

namespace Yaba.Entities
{
	public class EFTabItemCategoryRepository : ITabItemCategoryRepository
	{
		private readonly IYabaDBContext _context;
		public EFTabItemCategoryRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<Guid> Create(TabItemCategoryCreateDTO category)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(TabItemCategorySimpleDTO category)
		{
			throw new NotImplementedException();
		}

		public async Task<TabItemCategorySimpleDTO> Find(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<TabItemCategorySimpleDTO> FindFromTabItemId(Guid tabItemID)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Update(TabItemCategorySimpleDTO category)
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
		// ~EFTabItemCategoryRepository() {
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
