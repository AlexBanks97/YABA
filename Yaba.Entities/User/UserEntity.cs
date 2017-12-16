using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Common.User
{
    public class UserEntity
    {
	    public Guid Id { get; set; }
		[Required]
		public string FacebookId { get; set; }
		[Required]
        public string Name { get; set; }
	}
}
