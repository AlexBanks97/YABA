using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.Category;
using Yaba.Common.DTOs.BudgetEntry;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Entities
{
    public class EFBudgetEntryRepository : IBudgetEntryRepository
    {
        IYabaDBContext _context;
        public EFBudgetEntryRepository(IYabaDBContext context)
        {
            _context = context;
        }
        public Task<Guid> CreateBudgetEntry(BudgetEntryDTO entry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBudgetEntry(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BudgetEntryDTO>> Find()
        {
            throw new NotImplementedException();
        }

        public async Task<BudgetEntryDetailsDto> Find(Guid BudgetEntryId)
        {
            var entry = _context.BudgetEntries.FirstOrDefault(e => e.Id == BudgetEntryId);
            if (entry == null)
            {
                return null;
            }
            var categoryDTO = new CategorySimpleDto
            {
                 Id = entry.BudgetCategory.Id,
                 Name = entry.BudgetCategory.Name
            };
            return new BudgetEntryDetailsDto
            {
                Id = entry.Id,
                Amount = entry.Amount,
                Description = entry.Description,
                BudgetCategory = categoryDTO,
                Date = entry.Date
            };
        }

        public Task<ICollection<BudgetEntryDTO>> FindFromBudgetCategory(Guid BudgetCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBudgetEntry(BudgetEntryDTO entry)
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
        // ~EFBudgetRepository() {
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
