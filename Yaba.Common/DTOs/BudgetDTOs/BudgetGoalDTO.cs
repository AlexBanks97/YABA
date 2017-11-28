using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class BudgetGoalDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public BudgetCategoryDTO BudgetCategory { get; set; }
    }
}
