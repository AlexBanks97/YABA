using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Entities.BudgetEntities
{
    public class Entry
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Category Category { get; set; }
    }
}
