using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class BudgetExpenseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public BudgetDTO budget { get; set; }
    }
}
