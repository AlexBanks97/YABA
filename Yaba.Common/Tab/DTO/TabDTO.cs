using System;
using System.Collections.Generic;
using Yaba.Common.Tab.DTO.Item;

namespace Yaba.Common.Tab.DTO
{
	public class TabDTO
	{
		public Guid Id { get; set; }
		public decimal Balance { get; set; }
		public IEnumerable<TabItemSimpleDTO> TabItems { get; set; }
		public State State { get; set; }

	}
}
