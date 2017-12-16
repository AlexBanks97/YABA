using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
			var dto = obj as UserDto;
			return dto != null &&
				   Id.Equals(dto.Id) &&
				   FacebookId == dto.FacebookId &&
				   Name == dto.Name;
		}
	}
}
