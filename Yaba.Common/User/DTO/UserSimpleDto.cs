using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.User.DTO
{
    public class UserSimpleDto
    {
		public Guid Id { get; set; }

		public string Name { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is UserSimpleDto dto)
			{
				return dto.Id == Id && dto.Name == Name;
			}
			return false;
		}
	}
}
