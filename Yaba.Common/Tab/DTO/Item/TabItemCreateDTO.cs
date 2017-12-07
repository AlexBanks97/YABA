using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Tab.DTO.Item
{
    public class TabItemCreateDTO
    {
		[Required]
		public Guid TabId { get; set; }

		[Required]
		public decimal Amount { get; set; }
	}
}
