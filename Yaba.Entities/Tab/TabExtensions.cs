using System.Linq;
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
				Users = tabEntity.Users.ToUserDto(),
				TabItems = tabEntity.TabItems.ToTabItemSimpleDTO().ToList()
			};
		}
	}
}
