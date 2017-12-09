using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Tab.DTO.Item
{
    public class TabItemCreateDTO
    {
		[Required]
		[ValidGuid]
		public Guid TabId { get; set; }
		public string Description { get; set; }
		[Required]
		public decimal Amount { get; set; }

	}
}
