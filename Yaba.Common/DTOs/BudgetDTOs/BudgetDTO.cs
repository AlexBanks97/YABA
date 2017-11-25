using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class BudgetDTO
    {
        public string Name { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; }
        public ICollection<IncomeDTO> Incomes { get; set; }
        public ICollection<ExpenseDTO> Expenses { get; set; }
    }
}
