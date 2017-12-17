using System;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.User.DTO;

namespace Yaba.Common.Tab.DTO
{
	public class TabCreateDto
	{
		[Required]
		[ValidGuid]
		public Guid UserOne { get; set; }
		[Required]
		[ValidGuid]
		public Guid UserTwo { get; set; }
		public decimal Balance { get; set; }
		public State State { get; set; }
	}
}
