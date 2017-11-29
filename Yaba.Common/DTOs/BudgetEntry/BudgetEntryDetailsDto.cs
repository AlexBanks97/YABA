using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common.DTOs.Category;

namespace Yaba.Common.DTOs.BudgetEntry
{
    public class BudgetEntryDetailsDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public CategorySimpleDto BudgetCategory { get; set; }
    }
}
