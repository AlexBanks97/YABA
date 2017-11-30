using System;

namespace Yaba.Common.Budget.DTO.Income
{
    public class IncomeCreateDto
    { 
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public Guid BudgetId { get; set; }
    }
}
