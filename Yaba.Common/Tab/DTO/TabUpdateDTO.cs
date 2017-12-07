using System;

namespace Yaba.Common.Tab.DTO
{
	public class TabUpdateDTO
	{
		public Guid Id { get; set; }
		public decimal? Balance { get; set; }
		public State? State { get; set; }
	}
}
