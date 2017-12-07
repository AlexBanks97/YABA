using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Tab.DTO.ItemCategory
{
	public class TabItemCategoryDTO
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
