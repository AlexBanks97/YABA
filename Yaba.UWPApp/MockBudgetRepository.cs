using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.UWPApp
{
	public class MockBudgetRepository : IBudgetRepository
	{
		public async Task<ICollection<BudgetSimpleDto>> All()
		{
			return new[]
			{
				new BudgetSimpleDto(){Id = Guid.NewGuid(), Name="My Personal Budget" },
				new BudgetSimpleDto(){Id = Guid.NewGuid(), Name="My Company Budget" },
			};
		}

		public Task<Guid> Create(BudgetCreateUpdateDto budget)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(Guid budgetId)
		{
			throw new NotImplementedException();
		}

		public async Task<BudgetDetailsDto> Find(Guid id) => new BudgetDetailsDto
		{
			Id = Guid.NewGuid(),
			Name = "blank",
			Categories = new List<CategoryGoalDto> { new CategoryGoalDto{ Balance = (decimal) 2.0, Goal = null, Id = Guid.NewGuid(), Name = "mad" }},
			Recurrings = new List<RecurringSimpleDto> { },
		};

		public Task<bool> Update(BudgetCreateUpdateDto budget)
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
		// ~MockBudgetRepository() {
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
