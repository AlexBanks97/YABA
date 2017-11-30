using System;

namespace Yaba.Common.Budget.DTO.Category
{
	public class CategoryCreateDto
	{
		public string Name { get; set; }
		public Guid BudgetId { get; set; }
	}
}
