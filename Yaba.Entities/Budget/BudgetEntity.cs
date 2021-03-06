﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Entities.Budget
{
	public class BudgetEntity
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public string OwnerId { get; set; }

		public ICollection<CategoryEntity> Categories { get; set; }
		public ICollection<RecurringEntity> Recurrings { get; set; }
		public ICollection<ExpenseEntity> Expenses { get; set; }
	}
}
