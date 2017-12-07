using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.Entities.Budget.Repository
{
	public class EFRecurringRepository : IRecurringRepository
	{
		private readonly IYabaDBContext _context;

		public EFRecurringRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<Guid> CreateBudgetRecurring(RecurringCreateDto recurring)
		{
			var budget = await _context.Budgets.SingleOrDefaultAsync(b => b.Id == recurring.BudgetId);
			if (budget == null)
			{
				return Guid.Empty;
			}

			var newBudgetRecurring = new RecurringEntity
			{
				Name = recurring.Name,
				Amount = recurring.Amount,
				Recurrence = recurring.Recurrence,
				BudgetEntity = budget
			};

			_context.BudgetRecurrings.Add(newBudgetRecurring);
			await _context.SaveChangesAsync();
			return newBudgetRecurring.Id;
		}

		public async Task<ICollection<RecurringSimpleDto>> FindAllBudgetRecurrings()
		{
			return _context.BudgetRecurrings.Select(bi => new RecurringSimpleDto
			{
				Id = bi.Id,
				Name = bi.Name,
				Amount = bi.Amount,
				Recurrence = bi.Recurrence,
			}).ToList();
		}

		public async Task<RecurringSimpleDto> FindBudgetRecurring(Guid budgetRecurringId)
		{

			var budgetRecurring = await _context.BudgetRecurrings.SingleOrDefaultAsync(bi => bi.Id == budgetRecurringId);
			if (budgetRecurring == null) { return null; }

			return new RecurringSimpleDto
			{
				Id = budgetRecurring.Id,
				Name = budgetRecurring.Name,
				Amount = budgetRecurring.Amount,
				Recurrence = budgetRecurring.Recurrence,
			};
		}

		public async Task<ICollection<RecurringSimpleDto>> FindAllBudgetRecurringsFromSpecificBudget(BudgetDto budget)
		{
			var recurrings = budget.Recurrings;
			if (recurrings == null) return null;
			return recurrings;
		}




		public async Task<bool> UpdateBudgetRecurring(RecurringUpdateDto recurring)
		{
			var entity = await _context.BudgetRecurrings.SingleOrDefaultAsync(bi => recurring.Id == bi.Id);
			if (entity == null) return false;

			entity.Name = recurring.Name ?? entity.Name;
			entity.Amount = recurring.Amount;
			entity.Recurrence = recurring.Recurrence;

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteBudgetRecurring(Guid budgetRecurring)
		{
			var entity = await _context.BudgetRecurrings
				.SingleOrDefaultAsync(t => t.Id == budgetRecurring);

			if (entity == null) { return false; }

			var toRemove = _context.BudgetRecurrings.Remove(entity);

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
