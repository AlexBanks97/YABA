using System;

namespace Yaba.Common.Budget.DTO.Recurring
{
	public class RecurringCreateDto
	{
		public string Name { get; set; }

		public decimal Amount { get; set; }

		public Recurrence Recurrence { get; set; }

		public Guid BudgetId { get; set; }
	}
}
