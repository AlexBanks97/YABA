﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.BudgetDTOs;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities
{
    public class EFBudgetIncomeRepository : IBudgetIncomeRepository
    {
        private readonly IYabaDBContext _context;

        public EFBudgetIncomeRepository(IYabaDBContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateBudgetIncome(BudgetIncomeCreateDTO budgetIncome)
        {
            var budget = await _context.Budgets.SingleOrDefaultAsync(b => b.Id == budgetIncome.BudgetId);
            if (budget == null)
            {
                return Guid.Empty;
            }

            var newBudgetIncome = new BudgetIncome
            {
                Name = budgetIncome.Name,
                Amount = budgetIncome.Amount,
                Recurrence = budgetIncome.Recurrence,
                Budget = budget
            };

            _context.BudgetIncomes.Add(newBudgetIncome);
            await _context.SaveChangesAsync();
            return newBudgetIncome.Id;
        }

        public async Task<ICollection<BudgetIncomeDTO>> FindAllBudgetIncomes()
        {
            return _context.BudgetIncomes.Select(bi => new BudgetIncomeDTO
            {
                Id = bi.Id,
                Name = bi.Name,
                Amount = bi.Amount,
                Recurrence = bi.Recurrence,
            }).ToList();
        }

        public async Task<BudgetIncomeDTO> FindBudgetIncome(Guid budgetIncomeId)
        {

            var budgetIncome = _context.BudgetIncomes.FirstOrDefault(bi => bi.Id == budgetIncomeId);
            if (budgetIncome == null) { return null; }

            return new BudgetIncomeDTO
            {
                Id = budgetIncome.Id,
                Name = budgetIncome.Name,
                Amount = budgetIncome.Amount,
                Recurrence = budgetIncome.Recurrence,
            };
        }

        public async Task<ICollection<BudgetIncomeSimpleDTO>> FindAllBudgetIncomesFromSpecificBudget(BudgetDTO budget)
        {
            var incomes = budget.Incomes;
            if (incomes == null) return null;
            return incomes;
        }
    



        public Task<bool> UpdateBudgetIncome(BudgetIncomeUpdateDTO budgetIncome)
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
