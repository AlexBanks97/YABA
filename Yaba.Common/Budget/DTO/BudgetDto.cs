using System;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.Common.Budget.DTO
{
	public class BudgetDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<CategoryDto> Categories { get; set; }
		public ICollection<RecurringSimpleDto> Recurrings { get; set; }
		public ICollection<ExpenseDto> Expenses { get; set; }
	}
}
