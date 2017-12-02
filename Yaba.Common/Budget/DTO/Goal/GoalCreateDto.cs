using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Goal
{
    public class GoalCreateDto
    {
		public decimal Amount { get; set; }

		public Recurrence Recurrence { get; set; }

		public CategorySimpleDto CategoryEntity { get; set; }
	}
}
