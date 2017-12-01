using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaba.Common.Budget.DTO;

namespace Yaba.Common.Budget
{
	public interface IBudgetRepository : IDisposable
	{
		Task<BudgetDetailsDto> Find(Guid id);
		Task<ICollection<BudgetSimpleDto>> All();
		Task<Guid> Create(BudgetCreateUpdateDto budget);
		Task<bool> Update(BudgetCreateUpdateDto budget);
		Task<bool> Delete(Guid budgetId);
	}
}
