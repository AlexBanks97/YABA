using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Budget.DTO
{
	public class BudgetCreateUpdateDto
	{
		public Guid? Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
