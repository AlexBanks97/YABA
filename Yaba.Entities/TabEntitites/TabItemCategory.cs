using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Entities.TabEntitites
{
	public class TabItemCategory
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
