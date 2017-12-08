using System;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.Common.Budget.DTO
{
	public class BudgetDetailsDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public ICollection<CategoryGoalDTO> Categories { get; set; }
		public Decimal CategoriesTotal { get; set; }
		public ICollection<RecurringSimpleDto> Recurrings { get; set; }
		public Decimal RecurringsTotal { get; set; }
		// public ICollection<BudgetExpense> Expenses { get; set; }
	}
}
