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
		[Required]
		[ValidGuid]
		public Guid UserOne { get; set; }
		[Required]
		[ValidGuid]
		public Guid UserTwo { get; set; }
		public State State { get; set; }
		public decimal Balance { get; set; }
	}
}
