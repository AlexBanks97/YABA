using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common.DTO.TabDTOs;

namespace Yaba.Entities.TabEntitites
{
    public static class TabItemExtensions
    {
        public static TabItemSimpleDTO ToTabItemSimpleDTO(this TabItem tabItem)
        {
            return new TabItemSimpleDTO
            {
                Amount = tabItem.Amount,
                Description = tabItem.Description,
                Category = tabItem.Category != null ? new TabCategoryDTO { Name = tabItem.Category.Name } : null
            };
        }

        public static IEnumerable<TabItemSimpleDTO> ToTabItemSimpleDTO(this IEnumerable<TabItem> tabItems)
        {
            foreach (var tabItem in tabItems)
            {
                yield return tabItem.ToTabItemSimpleDTO();
            }
        }
    }
}
