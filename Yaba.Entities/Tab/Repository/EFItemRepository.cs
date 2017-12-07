using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Common.Tab.DTO.ItemCategory;

namespace Yaba.Entities.Tab.Repository
{
	public class EFItemRepository : ItemRepository
	{
		private readonly IYabaDBContext _context;
		public EFItemRepository(IYabaDBContext context)
		{
			_context = context;
		}
		public async Task<Guid> Create(TabItemCreateDTO tabItemDTO)
		{
			var tab = _context.Tabs.SingleOrDefault(t => t.Id == tabItemDTO.TabId);
			if (tab == null) return Guid.Empty;
			var tabItem = new ItemEntity
			{
				TabEntity = tab,
				Amount = tabItemDTO.Amount,
			};
			_context.TabItems.Add(tabItem);
			await _context.SaveChangesAsync();
			return tabItem.Id;
		}

		public async Task<TabItemSimpleDTO> Find(Guid id)
		{
			var entity = _context.TabItems.SingleOrDefault(t => t.Id == id);
			if (entity == null) return null;
			return new TabItemSimpleDTO
			{
				Amount = entity.Amount,
				Category = entity.CategoryEntity != null ? new TabItemCategoryDTO { Name = entity.CategoryEntity.Name } : null,
				Description = entity.Description
			};
		}

		public async Task<IEnumerable<TabItemSimpleDTO>> FindFromTab(Guid tabId)
		{
			var tabItems = _context.TabItems
				.Where(t => t.TabEntity.Id == tabId)
				.Select(t => t.ToTabItemSimpleDTO());

			return tabItems;


		}

		public async Task<bool> Update(TabItemSimpleDTO tabItemDTO)
		{
			var entity = _context.TabItems.SingleOrDefault(t => t.Id == tabItemDTO.Id);

			if (entity == null) return false;

			entity.Amount = tabItemDTO.Amount;
			entity.Description = tabItemDTO.Description ?? entity.Description;
			entity.CategoryEntity = tabItemDTO.Category != null ? new ItemCategoryEntity { Name = tabItemDTO.Category.Name } : null; // WARNING: This needs to be remade.

			_context.TabItems.Update(entity);

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> Delete(Guid id)
		{
			var entity = _context.TabItems.SingleOrDefault(t => t.Id == id);
			if (entity == null) return false;
			_context.TabItems.Remove(entity);
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
		// ~EFTabItemRepository() {
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
