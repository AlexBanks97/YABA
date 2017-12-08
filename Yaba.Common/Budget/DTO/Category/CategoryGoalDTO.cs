using System;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Goal;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Common.Budget.DTO.Category
{
    public class CategoryGoalDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public GoalSimpleDto Goal {get; set; }

    }
}