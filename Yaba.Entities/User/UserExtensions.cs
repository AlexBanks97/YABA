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
		    };
	    }
	}
}
