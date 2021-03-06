﻿using System.Collections.Generic;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Entities.User;

namespace Yaba.Entities.Tab
{
	public static class TabItemExtensions
	{
		public static TabItemSimpleDTO ToTabItemSimpleDTO(this ItemEntity itemEntity)
		{
			return new TabItemSimpleDTO
			{
				Id = itemEntity.Id,
				Amount = itemEntity.Amount,
				Description = itemEntity.Description,
				CreatedBy = itemEntity.CreatedBy?.ToUserDto(),
			};
		}
	}
}
