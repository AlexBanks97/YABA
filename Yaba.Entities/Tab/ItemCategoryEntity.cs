using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Entities.Tab
{
	public class ItemCategoryEntity
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
