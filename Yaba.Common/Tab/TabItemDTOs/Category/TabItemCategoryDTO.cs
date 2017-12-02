using System;
using System.Collections.Generic;

namespace Yaba.Common.DTO.TabDTOs
{
	public class TabItemCategoryDTO
	{
		public Guid Id { get; set; }
		public ICollection<TabItemDTO> TabItems { get; set; }
		public string Name { get; set; }
	}
}
