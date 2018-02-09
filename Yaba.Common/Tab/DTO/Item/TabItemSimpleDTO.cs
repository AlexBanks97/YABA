using System;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.User.DTO;

namespace Yaba.Common.Tab.DTO.Item
{
	public class TabItemSimpleDTO
	{
		public Guid Id { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		public UserDto CreatedBy { get; set; }

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

