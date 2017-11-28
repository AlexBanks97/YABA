using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class BudgetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<BudgetCategoryDTO> Categories { get; set; }
        public ICollection<BudgetIncomeDTO> Incomes { get; set; }
        public ICollection<BudgetExpenseDTO> Expenses { get; set; }
    }
}
