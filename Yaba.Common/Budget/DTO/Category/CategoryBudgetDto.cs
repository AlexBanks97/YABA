using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Common.Budget.DTO.Category
{
    public class CategoryBudgetDto
    {
		[Required]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public BudgetSimpleDto Budget { get; set; }
	}
}
