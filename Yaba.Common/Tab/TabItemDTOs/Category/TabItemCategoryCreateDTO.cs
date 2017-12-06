using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common.DTO.TabDTOs;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Tab.TabItemDTOs
{
    public class TabItemCategoryCreateDTO
    {
		[Required]
		public string Name { get; set; }
	}
}
