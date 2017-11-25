using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class EntryDTO
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public CategoryDTO Category { get; set; }
    }
}
