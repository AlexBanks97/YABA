using System;
using Yaba.Common;

namespace Yaba.Entities.Budget
{
	public class GoalEntity
	{
		public Guid Id { get; set; }

		public decimal Amount { get; set; }

		public Recurrence Recurrence { get; set; }

		public CategoryEntity CategoryEntity { get; set; }
	}
}
