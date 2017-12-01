namespace Yaba.Common.DTO.TabDTOs
{
	public class TabItemDTO
	{
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public TabDTO Tab { get; set; }
		public TabCategoryDTO Category { get; set; }
	}

}
