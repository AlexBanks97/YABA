using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.Category;
using Yaba.Common.DTOs.BudgetEntry;
using Yaba.Common.DTOs.BudgetDTOs;
using Yaba.Entities.BudgetEntities;

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

        public async Task<bool> DeleteBudgetEntry(Guid Id)
        {
            var entry = _context.BudgetEntries.SingleOrDefault(e => e.Id == Id);
            if(entry == null)
            {
                return false;
            }
            _context.BudgetEntries.Remove(entry);
            var NumberOfRowsRemoved = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<BudgetEntryDTO>> Find()
        {
            var allEntries = _context.BudgetEntries;

            var result = new List<BudgetEntryDTO>();

            foreach (BudgetEntry entry in allEntries)
            {
                var BudgetCategoryDTO = new BudgetCategoryDTO
                {
                     Id = entry.BudgetCategory.Id,
                      Name = entry.BudgetCategory.Name
                };
                result.Add(new BudgetEntryDTO
                {
                    Id = entry.Id,
                    Amount = entry.Amount,
                    Date = entry.Date,
                    BudgetCategory = BudgetCategoryDTO,
                    Description = entry.Description
                });
            }
            return result;
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

        public async Task<ICollection<BudgetEntryDTO>> FindFromBudgetCategory(Guid BudgetCategoryId)
        {
            var cat = _context.BudgetCategories.FirstOrDefault(c => c.Id == BudgetCategoryId);
            if(cat == null)
            {
                return null;
            }
            var BudgetCategoryDTO = new BudgetCategoryDTO
            {
                 Id = cat.Id,
                 Name = cat.Name
            };
            var entries = _context.BudgetEntries.Select(b => b).Where(b => b.BudgetCategory.Id == cat.Id);

            var result = new List<BudgetEntryDTO>();

            foreach(BudgetEntry entry in entries)
            {
                result.Add(new BudgetEntryDTO
                {
                    Id = entry.Id,
                    Amount = entry.Amount,
                    Date = entry.Date,
                    BudgetCategory = BudgetCategoryDTO,
                    Description = entry.Description
                });
            }
            return result;
        }

        public async Task<bool> UpdateBudgetEntry(BudgetEntryDTO entry)
        {
            var entity = _context.BudgetEntries
                .SingleOrDefault(c => c.Id == entry.Id);
            if (entity == null)
            {
                return false;
            }

            if (entry.BudgetCategory != null) {
                var BudgetCategory = new BudgetCategory {
                    Id = entry.BudgetCategory.Id,
                    Name = entry.BudgetCategory.Name
                };
                entity.BudgetCategory = BudgetCategory;
            }

            entity.Description = entry.Description;
            entity.Amount = entry.Amount;
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
