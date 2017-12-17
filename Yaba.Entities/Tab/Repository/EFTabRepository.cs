using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yaba.Common;
using Yaba.Common.Tab.DTO;
using Yaba.Entities.User;

namespace Yaba.Entities.Tab.Repository
{
	public class EFTabRepository : ITabRepository
	{
		private readonly IYabaDBContext _context;

		public EFTabRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<Guid> CreateTab(TabCreateDto tab)
		{
			var userOne = await _context.Users.SingleOrDefaultAsync(u => u.Id == tab.UserOne);
			var userTwo = await _context.Users.SingleOrDefaultAsync(u => u.Id == tab.UserTwo);

			if (userOne == null || userTwo == null)
				throw new Exception();

			var tabEntity = new TabEntity
			{
				Balance = tab.Balance,
				State = tab.State,
				UserOne = userOne,
				UserTwo = userTwo,
			};

			_context.Tabs.Add(tabEntity);
			await _context.SaveChangesAsync();
			return tabEntity.Id;
		}

		public async Task<bool> UpdateTab(TabUpdateDto tab)
		{
			var entity = await _context.Tabs.SingleOrDefaultAsync(t => t.Id == tab.Id);
			if (entity == null) return false;
			// set entity balance to tab balance if tab is not null, otherwise keep original value.
			entity.Balance = tab.Balance ?? entity.Balance;
			entity.State = tab.State ?? entity.State;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<ICollection<TabDto>> FindAllTabs()
		{
			return _context.Tabs
				.Include(t => t.TabItems)
				.Select(t => new TabDto
					{
						Id = t.Id,
						TabItems = t.TabItems.ToTabItemSimpleDTO(),
						Balance = t.Balance,
						State = t.State,
						UserOne = t.UserOne.ToUserDto(),
						UserTwo = t.UserTwo.ToUserDto(),
					}).ToList();
		}

		public async Task<TabDto> FindTab(Guid id)
		{
			var tab = await _context.Tabs
				.Include(t => t.TabItems)
				.SingleOrDefaultAsync(b => b.Id == id);
			if (tab == null) return null;
			return tab.ToDTO();

		}

		public async Task<bool> Delete(Guid id)
		{
			var entity = await _context.Tabs
				.SingleOrDefaultAsync(t => t.Id == id);

			if (entity == null) return false;

			_context.Tabs.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		#region

		IDisposable Support;
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
		// ~EFTabRepository() {
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
