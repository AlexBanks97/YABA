using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Entities.TabEntitites
{
	public class TabItem
	{
		public Guid Id { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[StringLength(150)]
		public string Description { get; set; }
		[Required]
		public Tab Tab { get; set; }
		public TabItemCategory Category { get; set; }
		// Add prop for user whom created tab item
	}
}
