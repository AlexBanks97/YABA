using System;
using System.Collections.Generic;
using Yaba.Common;
using Yaba.Common.User;

namespace Yaba.Entities.Tab
{
	public class TabEntity
	{
		public Guid Id { get; set; }
		public ICollection<ItemEntity> TabItems { get; set; }
		public (UserEntity userOne, UserEntity userTwo) Users { get; set; }
		public State State { get; set; }
		public decimal Balance { get; set; }
	}
}
