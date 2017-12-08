using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Common.User.DTO
{
    public class UserCreateDto
    {
		[Required]
		public string Name { get; set; }

		public ICollection<UserDetailsDto> Friends { get; set; }
	}
}
