using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.Budget.DTO.Category
{
    public class CategoryBudgetDto
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public BudgetSimpleDto Budget { get; set; }
	}
}
