using System;
using System.Collections.Generic;
using Yaba.Common.Tab.DTO.Item;

namespace Yaba.Common.Tab.DTO
{
	public class TabDto
	{
		public Guid Id { get; set; }
		public decimal Balance { get; set; }
		public IEnumerable<TabItemSimpleDTO> TabItems { get; set; }
		public State State { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is TabDto dto)
			{
                return dto.Id.Equals(Id) && dto.Balance.Equals(Balance);
			}
			return false;
		}

	}
}
