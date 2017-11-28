using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class BudgetEntryDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public BudgetCategoryDTO BudgetCategory { get; set; }
    }
}
