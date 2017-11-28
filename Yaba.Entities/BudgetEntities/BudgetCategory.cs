using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Entities.BudgetEntities
{
    public class BudgetCategory
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Budget Budget { get; set; }
        public ICollection<BudgetEntry> Entries { get; set; }
        public BudgetGoal BudgetGoal { get; set; }
    }
}
