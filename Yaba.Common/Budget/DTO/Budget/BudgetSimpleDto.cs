using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Common.Budget.DTO
{
	public class BudgetSimpleDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is BudgetSimpleDto dto)
			{
				return dto.Id == Id && dto.Name == Name;
			}
			return false;
		}
	}
}
