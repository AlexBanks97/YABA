using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yaba.Common.Tab.DTO;
using Yaba.Common.Tab.DTO.Item;

namespace Yaba.Common.User.DTO
{
    public class UserDto
    {
	    public Guid Id { get; set; }

		[Required]
	    public string FacebookId { get; set; }
		[Required]
	    public string Name { get; set; }

		public override bool Equals(object obj)
		{
			return obj is UserDto dto &&
				   Id.Equals(dto.Id) &&
				   FacebookId == dto.FacebookId &&
				   Name == dto.Name;
		}
	}
}
