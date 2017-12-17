using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaba.Common.Tab.DTO;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Common.User;
using Yaba.Common.User.DTO;

namespace Yaba.Entities.User
{
    public static class UserExtensions
    {
	    public static UserDto ToUserDto(this UserEntity user)
	    {
		    return new UserDto
		    {
				Name = user.Name,
				FacebookId = user.FacebookId,
				Id = user.Id,
				Tabs = user.Tabs.Select(t => new TabDto
				{
					Id = t.Id,
					TabItems = t.TabItems.Select(ti => new TabItemSimpleDTO
					{
						Id = ti.Id,
						Amount = ti.Amount,
						CreatedBy = ti.CreateBy,
						Description = ti.Description,
					}),
					Balance = t.Balance,
					State = t.State,
					UserOne = t.UserOne,
					UserTwo = t.UserTwo,
				}).ToList(),
				
		    };
	    }
	}
}
