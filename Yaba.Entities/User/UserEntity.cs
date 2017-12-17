using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yaba.Entities.Tab;

namespace Yaba.Common.User
{
    public class UserEntity
    {
		[ValidGuid]
	    public Guid Id { get; set; }
		[Required]
		public string FacebookId { get; set; }
		[Required]
        public string Name { get; set; }
	}
}
