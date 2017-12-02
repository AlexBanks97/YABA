using System.Collections.Generic;

namespace Yaba.Common.DTO.TabDTOs
{
	public class TabDTO
	{
		public decimal Balance { get; set; }
		public IEnumerable<TabItemSimpleDTO> TabItems { get; set; }
		public State State { get; set; }

	}
}
