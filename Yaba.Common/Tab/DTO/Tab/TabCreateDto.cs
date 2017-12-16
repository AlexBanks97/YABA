using System.ComponentModel.DataAnnotations;
using Yaba.Common.User.DTO;

namespace Yaba.Common.Tab.DTO
{
	public class TabCreateDto
	{
		[Required]
		public (UserDto userOne, UserDto userTwo) Users { get; set; }
		public decimal Balance { get; set; }
		public State State { get; set; }
	}
}
