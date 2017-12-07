using System;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.Tab.DTO.ItemCategory;

namespace Yaba.Common.Tab.DTO.Item
{
	public class TabItemDTO
	{
		public Guid Id { get; set; }

		[Required]
		public decimal Amount { get; set; }
		
		[StringLength(150)]
		public string Description { get; set; }

		[Required]
		public TabDTO Tab { get; set; }
		
		public TabItemCategoryDTO Category { get; set; }
	}

}
