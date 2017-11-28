using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.TabDTOs;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities
{
    public class EFTabRepository : ITabRepository
    {
        private readonly IYabaDBContext _context;

        public EFTabRepository(IYabaDBContext context)
        {
            _context = context;
        }

        public async Task<TabDTO> FindTab(Guid id)
        {
            var tab = await _context.Tabs
                .Include(t => t.TabItems)
                .SingleOrDefaultAsync(b => b.Id == id);
            if (tab == null)
            {
                return null;
            }
            return new TabDTO
            {
                Balance = tab.Balance,
                State = tab.State,
                TabItems = tab.TabItems
                    .Select(t => new TabItemDTO())
                    .ToList(),
            };

            /* var tab = await _context.Tabs.Include(t => t.TabItems).FirstOrDefaultAsync(t => t.Id == id);
             if (tab == null) return null;
             TabDTO tabDTO = new TabDTO();
             tabDTO.Balance = tab.Balance;
             tabDTO.State = tab.State;
             tabDTO.TabItems = tab.TabItems
                 .Select(t => new TabItemDTO
                     {
                        Amount = t.Amount,
                        Description = t.Description,
                        Category = new TabCategoryDTO
                        {
                            Name = t.Category.Name
                        },
                        Tab = tabDTO
                     }).ToList();
             return tabDTO; */
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
