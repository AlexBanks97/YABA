using System;

namespace Yaba.Common.DTO.TabDTOs
{
	public class TabItemSimpleDTO
	{
		public Guid Id { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public TabCategoryDTO Category { get; set; }
	}
}
