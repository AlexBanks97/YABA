using System;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Entry
{
	public class EntryDetailsDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public decimal Amount { get; set; }
		
		public string Description { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public CategorySimpleDto BudgetCategory { get; set; }
	}
}
