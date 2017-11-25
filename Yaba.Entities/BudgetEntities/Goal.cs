using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Yaba.Common;

namespace Yaba.Entities.BudgetEntities
{
    public class Goal
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public Category Category { get; set; }
    }
}
