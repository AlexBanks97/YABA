using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Common
{
    public interface IBudgetIncomeRepository : IDisposable
    {
        Task<BudgetIncomeDTO> FindBudgetIncome(Guid budgetIncomeId);

        Task<ICollection<BudgetIncomeDTO>> FindBudgetIncomesFromSpecificBudget(Guid BudgetId);

        Task<ICollection<BudgetIncomeDTO>> FindAllBudgetIncomes();

        Task<Guid> CreateBudgetIncome(BudgetIncomeDTO budgetIncome);

        Task<bool> UpdateBudgetIncome(BudgetIncomeDTO budgetIncome);
    }
}