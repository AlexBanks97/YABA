﻿using System.Linq;
using Yaba.Common.Tab.DTO;
using Yaba.Entities.User;

namespace Yaba.Entities.Tab
{
	public static class TabExtensions
	{
		public static TabDto ToDTO(this TabEntity tabEntity)
		{
			return new TabDto
			{
				Id = tabEntity.Id,
				Balance = tabEntity.Balance,
				State = tabEntity.State,
				UserOne = tabEntity.UserOne?.ToUserDto(),
				UserTwo = tabEntity.UserTwo?.ToUserDto(),
				TabItems = tabEntity.TabItems.Select(t => t.ToTabItemSimpleDTO()),
			};
		}
	}
}
