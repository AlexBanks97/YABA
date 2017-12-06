using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Entry
{
    public class EntrySimpleDto
    {
		public Guid Id { get; set; }
		public decimal Amount { get; set; }

		public string Description { get; set; }

		public DateTime Date { get; set; }

	}
}
