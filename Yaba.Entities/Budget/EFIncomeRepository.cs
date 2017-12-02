using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Income;

namespace Yaba.Entities.Budget
{
	public class EFIncomeRepository : IIncomeRepository
	{
		private readonly IYabaDBContext _context;

		public EFIncomeRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<Guid> CreateBudgetIncome(IncomeCreateDto income)
		{
			var budget = await _context.Budgets.SingleOrDefaultAsync(b => b.Id == income.BudgetId);
			if (budget == null)
			{
				return Guid.Empty;
			}

			var newBudgetIncome = new IncomeEntity
			{
				Name = income.Name,
				Amount = income.Amount,
				Recurrence = income.Recurrence,
				BudgetEntity = budget
			};

			_context.BudgetIncomes.Add(newBudgetIncome);
			await _context.SaveChangesAsync();
			return newBudgetIncome.Id;
		}

		public async Task<ICollection<IncomeSimpleDto>> FindAllBudgetIncomes()
		{
			return _context.BudgetIncomes.Select(bi => new IncomeSimpleDto
			{
				Id = bi.Id,
				Name = bi.Name,
				Amount = bi.Amount,
				Recurrence = bi.Recurrence,
			}).ToList();
		}

		public async Task<IncomeSimpleDto> FindBudgetIncome(Guid budgetIncomeId)
		{

			var budgetIncome = await _context.BudgetIncomes.SingleOrDefaultAsync(bi => bi.Id == budgetIncomeId);
			if (budgetIncome == null) { return null; }

			return new IncomeSimpleDto
			{
				Id = budgetIncome.Id,
				Name = budgetIncome.Name,
				Amount = budgetIncome.Amount,
				Recurrence = budgetIncome.Recurrence,
			};
		}

		public async Task<ICollection<IncomeSimpleDto>> FindAllBudgetIncomesFromSpecificBudget(BudgetDto budget)
		{
			var incomes = budget.Incomes;
			if (incomes == null) return null;
			return incomes;
		}




		public async Task<bool> UpdateBudgetIncome(IncomeUpdateDto income)
		{
			var entity = await _context.BudgetIncomes.SingleOrDefaultAsync(bi => income.Id == bi.Id);
			if (entity == null) return false;

			entity.Name = income.Name ?? entity.Name;
			entity.Amount = income.Amount;
			entity.Recurrence = income.Recurrence;

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteBudgetIncome(Guid budgetIncome)
		{
			var entity = await _context.BudgetIncomes
				.SingleOrDefaultAsync(t => t.Id == budgetIncome);

			if (entity == null) { return false; }

			var toRemove = _context.BudgetIncomes.Remove(entity);

			if (toRemove == null) { return false; }

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
