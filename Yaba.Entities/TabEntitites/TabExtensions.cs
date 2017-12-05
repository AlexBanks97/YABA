using System.Linq;
using Yaba.Common.DTO.TabDTOs;

namespace Yaba.Entities.TabEntitites
{
	public static class TabExtensions
	{
		public static TabDTO ToDTO(this Tab tab)
		{
			return new TabDTO
			{
				Balance = tab.Balance,
				State = tab.State,
				TabItems = tab.TabItems.ToTabItemSimpleDTO().ToList()
			};
		}
	}
}
