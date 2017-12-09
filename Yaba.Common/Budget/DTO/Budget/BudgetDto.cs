using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Recurring;


namespace Yaba.Common.Budget.DTO
{
	public class BudgetDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		public ICollection<CategoryDto> Categories { get; set; }
		public ICollection<RecurringSimpleDto> Recurrings { get; set; }
		public ICollection<RecurringDto> Expenses { get; set; }
	}
}
