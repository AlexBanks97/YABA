using System;

namespace Yaba.Common.Budget.DTO.Income
{
	public class IncomeSimpleDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal Amount { get; set; }

		public Recurrence Recurrence { get; set; }
	}
}
