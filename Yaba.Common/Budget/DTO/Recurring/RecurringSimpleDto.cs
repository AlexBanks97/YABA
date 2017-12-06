using System;

namespace Yaba.Common.Budget.DTO.Recurring
{
	public class RecurringSimpleDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal Amount { get; set; }

		public Recurrence Recurrence { get; set; }
	}
}
