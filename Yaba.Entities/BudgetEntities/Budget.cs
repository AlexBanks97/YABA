using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Entities.BudgetEntities
{
    public class Budget
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        

        public ICollection<BudgetCategory> Categories { get; set; }
        public ICollection<BudgetIncome> Incomes { get; set; }
        public ICollection<BudgetExpense> Expenses { get; set; }
    }
}
