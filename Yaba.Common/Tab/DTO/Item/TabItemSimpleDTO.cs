using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Tab.DTO.Item
{
	public class TabItemSimpleDTO
	{
		public Guid Id { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		[ValidGuid]
		public Guid CreatedBy { get; set; }

		[StringLength(150)]
		public string Description { get; set; }
		public override bool Equals(object obj)
		{
			if (obj is TabItemSimpleDTO dto)
			{
				return dto.Id == Id && dto.Amount == Amount && dto.Description == Description;
			}
			return false;
		}
	}
}
