using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.User.DTO
{
    class UserDetailsDto
    {
		public Guid Id { get; set; }

		public string Name { get; set; }

		public ICollection<UserDetailsDto> Friends { get; set; }
	}
}
