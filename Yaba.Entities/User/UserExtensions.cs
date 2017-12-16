using System;
using System.Collections.Generic;
using System.Text;
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

	    public static (UserDto userOne, UserDto userTwo) ToUserDto(this (UserEntity userOne, UserEntity userTwo) users)
	    {
		    return (new UserDto
		    {
				Name = users.userOne.Name,
				FacebookId = users.userOne.FacebookId,
				Id = users.userOne.Id,
		    }, new UserDto
		    {
			    Name = users.userTwo.Name,
			    FacebookId = users.userTwo.FacebookId,
			    Id = users.userTwo.Id,
			});
	    }
	}
}
