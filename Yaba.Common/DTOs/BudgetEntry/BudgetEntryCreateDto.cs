using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Common.DTOs.BudgetEntry
{
    public class BudgetEntryCreateDto
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public BudgetCategoryDTO BudgetCategory { get; set; }
    }
}
