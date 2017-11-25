using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class GoalDTO
    {
        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public CategoryDTO Category { get; set; }
    }
}
