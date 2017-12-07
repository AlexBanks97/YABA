using System.Collections.Generic;
using Yaba.Common.Tab.DTO.Item;

namespace Yaba.Entities.Tab
{
	public static class TabItemExtensions
	{
		public static TabItemSimpleDTO ToTabItemSimpleDTO(this ItemEntity itemEntity)
		{
			return new TabItemSimpleDTO
			{
				Amount = itemEntity.Amount,
				Description = itemEntity.Description,
			};
		}

		public static IEnumerable<TabItemSimpleDTO> ToTabItemSimpleDTO(this IEnumerable<ItemEntity> tabItems)
		{
			foreach (var tabItem in tabItems)
			{
				yield return tabItem.ToTabItemSimpleDTO();
			}
		}
	}
}
