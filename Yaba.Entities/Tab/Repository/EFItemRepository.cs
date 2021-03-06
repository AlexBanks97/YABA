﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Entities.User;

namespace Yaba.Entities.Tab.Repository
{
	public class EFItemRepository : IItemRepository
	{
		private readonly IYabaDBContext _context;
		public EFItemRepository(IYabaDBContext context)
		{
			_context = context;
		}
		public async Task<TabItemSimpleDTO> Create(TabItemCreateDTO tabItemDTO)
		{
			var tab = _context.Tabs.SingleOrDefault(t => t.Id == tabItemDTO.TabId);
			var user = _context.Users.SingleOrDefault(u => u.Id == tabItemDTO.CreatedBy);
			if (tab == null || user == null) return null;
			var tabItem = new ItemEntity
			{
				Description = tabItemDTO.Description,
				TabEntity = tab,
				CreatedBy = user,
				Amount = tabItemDTO.Amount,
			};
			_context.TabItems.Add(tabItem);
			await _context.SaveChangesAsync();
			return tabItem.ToTabItemSimpleDTO();
		}

		public async Task<TabItemSimpleDTO> Find(Guid id)
		{
			var entity = _context.TabItems.SingleOrDefault(t => t.Id == id);
			if (entity == null) return null;
			return new TabItemSimpleDTO
			{
				Id = entity.Id,
				Amount = entity.Amount,
				CreatedBy = entity.CreatedBy?.ToUserDto(),
				Description = entity.Description,
			};
		}

		public async Task<IEnumerable<TabItemSimpleDTO>> FindFromTab(Guid tabId)
		{

			var tab = _context.Tabs
				.Include(t => t.TabItems)
					.ThenInclude(ti => ti.CreatedBy)
				.SingleOrDefault(t => t.Id == tabId);

			if (tab == null) return null;
			return tab.TabItems
				.Select(t => t.ToTabItemSimpleDTO())
				.ToList();
		}

		public async Task<bool> Update(TabItemSimpleDTO tabItemDTO)
		{
			var entity = _context.TabItems.SingleOrDefault(t => t.Id == tabItemDTO.Id);

			if (entity == null) return false;

			if (tabItemDTO.Amount != 0) entity.Amount = tabItemDTO.Amount;
			entity.Description = tabItemDTO.Description ?? entity.Description;
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
