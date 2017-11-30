using System;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO
{
    public class GoalDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public CategoryDto Category { get; set; }
    }
}
