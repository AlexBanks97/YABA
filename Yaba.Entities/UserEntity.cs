using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.User
{
    class UserEntity
    {
		public Guid Id { get; set; }

		public string Name { get; set; }

		public ICollection<UserEntity> Friends { get; set; }
	}
}
