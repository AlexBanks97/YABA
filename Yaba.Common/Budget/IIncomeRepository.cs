using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Income;

namespace Yaba.Common.Budget
{
	public interface IIncomeRepository : IDisposable
	{
		Task<IncomeSimpleDto> FindBudgetIncome(Guid budgetIncomeId);

		Task<ICollection<IncomeSimpleDto>> FindAllBudgetIncomesFromSpecificBudget(BudgetDto BudgetId);

		Task<ICollection<IncomeSimpleDto>> FindAllBudgetIncomes();

		Task<Guid> CreateBudgetIncome(IncomeCreateDto income);

		Task<bool> DeleteBudgetIncome(Guid budgetIncome);

		Task<bool> UpdateBudgetIncome(IncomeUpdateDto income);
	}
}
