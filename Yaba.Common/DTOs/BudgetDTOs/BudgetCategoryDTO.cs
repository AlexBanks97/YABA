using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class BudgetCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BudgetDTO Budget { get; set; }
        public ICollection<BudgetEntryDTO> Entries { get; set; }
        public BudgetGoalDTO BudgetGoal { get; set; }
    }
}
