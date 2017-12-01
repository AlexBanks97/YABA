using System;
using System.Collections.Generic;
using Yaba.Common;

namespace Yaba.Entities.TabEntitites
{
	public class Tab
	{
		public Guid Id { get; set; }
		public ICollection<TabItem> TabItems { get; set; }
		public State State { get; set; }
		public decimal Balance { get; set; }
		// Add prop for useres
	}
}
