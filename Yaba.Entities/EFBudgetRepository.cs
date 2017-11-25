using System;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.BudgetDTOs;

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
                Name = budget.Name,
            };
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
