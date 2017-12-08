using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.User
{
    class UserEntity
    {
		public Guid Id { get; set; }

		public string name { get; set; }

		public ICollection<UserEntity> friends { get; set; }
	}
}
