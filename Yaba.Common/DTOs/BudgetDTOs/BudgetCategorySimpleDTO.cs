using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    class BudgetCategorySimpleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<BudgetEntryDTO> Entries { get; set; }
        public BudgetGoalDTO BudgetGoal { get; set; }
    }
}
