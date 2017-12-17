using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yaba.Common;
using Yaba.Common.User;

namespace Yaba.Entities.Tab
{
	public class TabEntity
	{
		[ValidGuid]
		public Guid Id { get; set; }
		public ICollection<ItemEntity> TabItems { get; set; }

		public virtual UserEntity UserOne { get; set; }
		public virtual UserEntity UserTwo { get; set; }

		public State State { get; set; }
		public decimal Balance { get; set; }
	}
}
