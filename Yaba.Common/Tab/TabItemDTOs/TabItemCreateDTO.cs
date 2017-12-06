using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Common.DTO.TabDTOs
{
    public class TabItemCreateDTO
    {
		[Required]
		public Guid TabId { get; set; }

		[Required]
		public decimal Amount { get; set; }
	}
}
