using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.DTO.TabDTOs
{
	public class TabItemSimpleDTO
	{
		public Guid Id { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[StringLength(150)]
		public string Description { get; set; }

		public TabItemCategoryDTO Category { get; set; }
	}
}
