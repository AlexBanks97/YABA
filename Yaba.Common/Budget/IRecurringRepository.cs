using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.Common.Budget
{
	public interface IRecurringRepository : IDisposable
	{
		Task<RecurringSimpleDto> FindBudgetRecurring(Guid budgetRecurringId);

		Task<ICollection<RecurringSimpleDto>> FindAllBudgetRecurringsFromSpecificBudget(BudgetDto BudgetId);

		Task<ICollection<RecurringSimpleDto>> FindAllBudgetRecurrings();

		Task<Guid> CreateBudgetRecurring(RecurringCreateDto recurring);

		Task<bool> DeleteBudgetRecurring(Guid budgetRecurring);

		Task<bool> UpdateBudgetRecurring(RecurringUpdateDto recurring);
	}
}
