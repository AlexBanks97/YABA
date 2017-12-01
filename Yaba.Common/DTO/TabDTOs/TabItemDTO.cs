using System;

namespace Yaba.Common.DTO.TabDTOs
{
	public class TabItemDTO
	{
		public Guid Id { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public TabDTO Tab { get; set; }
		public TabCategoryDTO Category { get; set; }
	}

}
