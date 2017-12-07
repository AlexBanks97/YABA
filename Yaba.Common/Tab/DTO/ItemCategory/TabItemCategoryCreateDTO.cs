using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Tab.DTO.ItemCategory
{
    public class TabItemCategoryCreateDTO
    {
		[Required]
		public string Name { get; set; }
	}
}
