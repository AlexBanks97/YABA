using System;
using System.ComponentModel.DataAnnotations;
using Yaba.Common;

namespace Yaba.Entities.Budget
{
	public class RecurringEntity
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public decimal Amount { get; set; }

		public Recurrence Recurrence { get; set; }

		public BudgetEntity BudgetEntity { get; set; }
	}
}
