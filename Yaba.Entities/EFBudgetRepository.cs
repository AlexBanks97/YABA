using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.BudgetDTOs;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities
{
    public class EFBudgetRepository : IBudgetRepository
    {
        private readonly IYabaDBContext _context;

        public EFBudgetRepository(IYabaDBContext context)
        {
            _context = context;
        }
        
        public async Task<BudgetDTO> FindBudget(Guid id)
        {
            var budget = _context.Budgets.FirstOrDefault(b => b.Id == id);
            if (budget == null) return null;
            return new BudgetDTO
            {
                Id = budget.Id,
                Name = budget.Name,
            };
        }

        public async Task<Guid> CreateBudget(BudgetCreateUpdateDTO budget)
        {
            var budgetEntity = new Budget
            {
                Name = budget.Name,
            };

            _context.Budgets.Add(budgetEntity);
            await _context.SaveChangesAsync();
            return budgetEntity.Id;
        }

        public async Task<ICollection<BudgetDTO>> FindAllBudgets()
        {
            return _context.Budgets.Select(b => new BudgetDTO
            {
                Id = b.Id,
                Name = b.Name,
            }).ToList();
        }

        public async Task<bool> UpdateBudget(BudgetCreateUpdateDTO budget)
        {
            if (budget.Id == null)
            {
                return false;                
            }
            var entity = await _context.Budgets.FindAsync(budget.Id);
            if (entity == null)
            {
                return false;
            }

            entity.Name = budget.Name;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddEntryToCategory(BudgetEntryDTO entry)
        {
            if(entry == null || entry.BudgetCategory == default(BudgetCategoryDTO))
            {
                return false;
            }
            var budgetCategory = await _context.BudgetCategories.FindAsync(entry.BudgetCategory.Id);
            var budgetEntry = new BudgetEntry
            {
                Amount = entry.Amount,
                Date = entry.Date,
                Description = entry.Description,
                BudgetCategory = budgetCategory
            };
            var result = _context.BudgetEntries.Add(budgetEntry);
            if(result == null)
            {
                return false;
            }
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
