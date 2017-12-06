using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Budget.DTO.Category
{
	public class CategoryCreateDto
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public Guid BudgetId { get; set; }
	}
}
