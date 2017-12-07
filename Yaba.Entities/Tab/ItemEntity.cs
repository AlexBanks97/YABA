using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Entities.Tab
{
	public class ItemEntity
	{
		public Guid Id { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[StringLength(150)]
		public string Description { get; set; }
		[Required]
		public TabEntity TabEntity { get; set; }
		// Add prop for user whom created tab item
	}
}
