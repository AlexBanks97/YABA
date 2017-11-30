using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Entities.Budget
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public BudgetEntity BudgetEntity { get; set; }
        public ICollection<EntryEntity> Entries { get; set; }
        public GoalEntity GoalEntity { get; set; }
    }
}
