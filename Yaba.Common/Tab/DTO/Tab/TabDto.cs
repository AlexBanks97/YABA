using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Common.User.DTO;

namespace Yaba.Common.Tab.DTO
{
	public class TabDto
	{
		public Guid Id { get; set; }
		public decimal Balance { get; set; }
		public IEnumerable<TabItemSimpleDTO> TabItems { get; set; }
		public State State { get; set; }
		[Required]
		[ValidGuid]
		public Guid UserOne { get; set; }
		[Required]
		[ValidGuid]
		public Guid UserTwo { get; set; }


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
