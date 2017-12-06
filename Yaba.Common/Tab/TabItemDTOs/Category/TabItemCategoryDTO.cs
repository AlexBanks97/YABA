using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.DTO.TabDTOs
{
	public class TabItemCategoryDTO
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
