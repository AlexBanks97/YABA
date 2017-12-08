using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.User.DTO
{
    class UserCreateDto
    {
		public string name { get; set; }

		public ICollection<UserDetailsDto> friends { get; set; }
	}
}
