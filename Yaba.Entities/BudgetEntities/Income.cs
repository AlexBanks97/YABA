using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yaba.Common;

namespace Yaba.Entities.BudgetEntities
{
    public class Income
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

        public Budget Budget { get; set; }
    }
}
