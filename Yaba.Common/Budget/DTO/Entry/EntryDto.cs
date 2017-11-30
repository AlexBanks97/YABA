using System;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Entry
{
	public class EntryDto
	{
		public Guid Id { get; set; }
		public decimal Amount { get; set; }

		public string Description { get; set; }

		public DateTime Date { get; set; }

		public CategoryDto Category { get; set; }
	}
}
