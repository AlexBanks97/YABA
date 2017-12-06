using System;
using System.Collections.Generic;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Common.Budget.DTO.Category
{
	public class CategoryDetailsDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<EntrySimpleDto> Entries { get; set; }
	}
}
