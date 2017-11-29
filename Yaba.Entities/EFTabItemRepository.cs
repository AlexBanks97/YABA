using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.TabDTOs;

namespace Yaba.Entities
{
    public class EFTabItemRepository : ITabItemRepository
    {
        private readonly IYabaDBContext _context;
        public EFTabItemRepository(IYabaDBContext context)
        {
            _context = context;
        }
        public Task<Guid> Create(TabItemSimpleDTO tabItemDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<TabItemSimpleDTO> Find(Guid id)
        {
            var entity = _context.TabItems.SingleOrDefault(t => t.Id == id);
            if (entity == null) return null;
            return new TabItemSimpleDTO
            {
                Amount = entity.Amount,
                Category = entity.Category != null ? new TabCategoryDTO { Name = entity.Category.Name } : null,
                Description = entity.Description
            };
        }

        public async Task<ICollection<TabItemSimpleDTO>> FindFrom(TabDTO tab)
        {
            return tab.TabItems.Select(t => new TabItemSimpleDTO
            {
                Amount = t.Amount,
                Description = t.Description,
                Category = t.Category != null ? new TabCategoryDTO { Name = t.Category.Name} : null
            }).ToList();
        }

        public Task<bool> Update()
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
