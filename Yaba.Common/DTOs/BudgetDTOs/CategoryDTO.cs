using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.BudgetDTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BudgetDTO Budget { get; set; }
        public ICollection<EntryDTO> Entries { get; set; }
        public GoalDTO Goal { get; set; }
    }
}
