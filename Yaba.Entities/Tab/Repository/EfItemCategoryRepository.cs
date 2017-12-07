using System;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common.Tab;
using Yaba.Common.Tab.DTO.ItemCategory;

namespace Yaba.Entities.Tab.Repository
{
	public class EFItemCategoryRepository : IItemCategoryRepository
	{
		private readonly IYabaDBContext _context;
		public EFItemCategoryRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<Guid> Create(TabItemCategoryCreateDTO category)
		{
			var entity = new ItemCategoryEntity { Name = category.Name };
			_context.TabItemCategories.Add(entity);
			await _context.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<bool> Delete(Guid categoryId)
		{
			var entity = _context.TabItemCategories.SingleOrDefault(c => c.Id == categoryId);
			if (entity == null) return false;
			_context.TabItemCategories.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<TabItemCategoryDTO> Find(Guid id)
		{
			var entity = _context.TabItemCategories.SingleOrDefault(c => c.Id == id);
			if (entity == null) return null;
			return new TabItemCategoryDTO
			{
				Id = entity.Id,
				Name = entity.Name,
			};
		}

		public async Task<TabItemCategoryDTO> FindFromTabItemId(Guid tabItemID)
		{
			var tabEntity = _context.TabItems.SingleOrDefault(t => t.Id == tabItemID);
			if (tabEntity == null || tabEntity.CategoryEntity == null) return null;
			return new TabItemCategoryDTO
			{
				Id = tabEntity.CategoryEntity.Id,
				Name = tabEntity.CategoryEntity.Name,
			};
		}

		public async Task<bool> Update(TabItemCategoryDTO category)
		{
			var entity = _context.TabItemCategories.SingleOrDefault(c => c.Id == category.Id);
			if (entity == null) return false;

			entity.Name = category.Name;
			_context.TabItemCategories.Update(entity);
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
