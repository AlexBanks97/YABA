using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Income;


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
		public ICollection<IncomeSimpleDto> Incomes { get; set; }
		public ICollection<ExpenseDto> Expenses { get; set; }
	}
}
