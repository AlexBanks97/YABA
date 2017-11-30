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

        Task<ICollection<BudgetIncomeDTO>> FindAllBudgetIncomesFromSpecificBudget(BudgetDTO BudgetId);

        Task<ICollection<BudgetIncomeDTO>> FindAllBudgetIncomes();

        Task<Guid> CreateBudgetIncome(BudgetIncomeCreateDTO budgetIncome);

        Task<bool> UpdateBudgetIncome(BudgetIncomeUpdateDTO budgetIncome);
    }
}