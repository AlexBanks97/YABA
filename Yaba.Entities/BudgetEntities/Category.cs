using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Entities.BudgetEntities
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Budget Budget { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public Goal Goal { get; set; }
    }
}
