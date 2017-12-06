using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Common.Budget.DTO.Category
{
	public class CategoryDetailsDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public ICollection<EntryDto> Entries { get; set; }
	}
}
