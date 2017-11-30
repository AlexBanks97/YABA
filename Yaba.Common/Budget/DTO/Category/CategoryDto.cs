using System;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Common.Budget.DTO.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BudgetDto Budget { get; set; }
        public ICollection<EntryDto> Entries { get; set; }
        public GoalDto Goal { get; set; }
    }
}
