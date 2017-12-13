using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Common.User.DTO
{
    public class UserCreateDto
    {
        [Required]
        public string Id { get; set; }

		[Required]
		public string Name { get; set; }

	}
}
