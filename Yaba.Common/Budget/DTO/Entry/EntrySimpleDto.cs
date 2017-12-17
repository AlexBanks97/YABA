using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Entry
{
    public class EntrySimpleDto
    {
		[Required]
		public Guid Id { get; set; }

		[Required]
		public decimal Amount { get; set; }

		public string Description { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public override bool Equals(object obj)
		{
			var dto = obj as EntrySimpleDto;
			return dto != null &&
				   Id.Equals(dto.Id) &&
				   Amount == dto.Amount &&
				   Description == dto.Description &&
				   Date == dto.Date;
		}
	}
}
