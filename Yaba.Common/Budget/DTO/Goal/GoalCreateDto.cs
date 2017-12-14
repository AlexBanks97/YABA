using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Goal
{
    public class GoalCreateDto
    {
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public Recurrence Recurrence { get; set; }
		public Guid CategoryId { get; set; }
	}
}
