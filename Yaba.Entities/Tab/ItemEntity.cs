using System;
using System.ComponentModel.DataAnnotations;
using Yaba.Common;
using Yaba.Common.User;

namespace Yaba.Entities.Tab
{
	public class ItemEntity
	{
		[ValidGuid]
		public Guid Id { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[StringLength(150)]
		public string Description { get; set; }
		[Required]
		public TabEntity TabEntity { get; set; }
		[Required]
		[ValidGuid]
		public UserEntity CreatedBy { get; set; }
		// Add prop for user whom created tab item
	}
}
