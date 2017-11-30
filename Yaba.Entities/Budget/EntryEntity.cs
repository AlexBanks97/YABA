using System;
using System.ComponentModel.DataAnnotations;

namespace Yaba.Entities.Budget
{
    public class EntryEntity
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public CategoryEntity CategoryEntity { get; set; }
    }
}
