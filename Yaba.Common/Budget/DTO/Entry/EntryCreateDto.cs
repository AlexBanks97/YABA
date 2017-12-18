using System;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Entry
{
	public class EntryCreateDto
	{
		[Required]
		public decimal Amount { get; set; }

		[StringLength(150)]
		public string Description { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public Guid CategoryId { get; set; }
	}
}
