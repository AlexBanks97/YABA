using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Budget.DTO.Category
{
	public class CategorySimpleDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
