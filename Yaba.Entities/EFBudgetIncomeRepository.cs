using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Entities
{
    class EFBudgetIncomeRepository : IBudgetIncomeRepository
    {
        private readonly IYabaDBContext _context;

        public EFBudgetIncomeRepository(IYabaDBContext context)
        {
            _context = context;
        }

        public Task<Guid> CreateBudgetIncome(BudgetIncomeDTO budgetIncome)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BudgetIncomeDTO>> FindAllBudgetIncomes()
        {
            throw new NotImplementedException();
        }

        public Task<BudgetIncomeDTO> FindBudgetIncome(Guid budgetIncomeId)
        {
            
        }

        public Task<ICollection<BudgetIncomeDTO>> FindBudgetIncomesFromSpecificBudget(Guid BudgetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBudgetIncome(BudgetIncomeDTO budgetIncome)
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
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EFBudgetIncomeRepository() {
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
