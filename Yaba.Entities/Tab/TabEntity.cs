using System;
using System.Collections.Generic;
using Yaba.Common;

namespace Yaba.Entities.Tab
{
	public class TabEntity
	{
		public Guid Id { get; set; }
		public ICollection<ItemEntity> TabItems { get; set; }
		public State State { get; set; }
		public decimal Balance { get; set; }
		// Add prop for useres
	}
}
