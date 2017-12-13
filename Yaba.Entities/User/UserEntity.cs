using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.User
{
    public class UserEntity
    {
		public String Id { get; set; }

        public string Name { get; set; }

		public ICollection<UserEntity> Friends { get; set; }
	}
}
