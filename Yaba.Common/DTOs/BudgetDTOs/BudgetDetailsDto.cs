using System;
using System.Collections.Generic;
using Yaba.Common.DTOs.Category;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class BudgetDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategorySimpleDto> Categories { get; set; }
        public ICollection<BudgetIncomeSimpleDTO> Incomes { get; set; }
        // public ICollection<BudgetExpense> Expenses { get; set; }
    }
}