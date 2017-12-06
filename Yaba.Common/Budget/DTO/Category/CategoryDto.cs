using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.Budget.DTO.Entry;
using Yaba.Common.Budget.DTO.Goal;

namespace Yaba.Common.Budget.DTO.Category
{
	public class CategoryDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public BudgetSimpleDto Budget { get; set; }

		public ICollection<EntryDto> Entries { get; set; }

		public GoalDto Goal { get; set; }
	}
}
