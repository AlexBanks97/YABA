using System;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Income;

namespace Yaba.Common.Budget.DTO
{
    public class BudgetDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategorySimpleDto> Categories { get; set; }
        public ICollection<IncomeSimpleDto> Incomes { get; set; }
        // public ICollection<BudgetExpense> Expenses { get; set; }
    }
}