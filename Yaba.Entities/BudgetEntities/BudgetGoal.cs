using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Yaba.Common;

namespace Yaba.Entities.BudgetEntities
{
    public class BudgetGoal
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public BudgetCategory BudgetCategory { get; set; }
    }
}
