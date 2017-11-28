using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yaba.Entities.BudgetEntities;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities
{
    public interface IYabaDBContext : IDisposable
    {
        // Budget-related entities:
        DbSet<Budget> Budgets { get; set; }
        DbSet<BudgetCategory> Categories { get; set; }
        DbSet<BudgetIncome> Incomes { get; set; } 
        DbSet<BudgetExpense> Expenses { get; set; }
        DbSet<BudgetEntry> Entries { get; set; }
        DbSet<BudgetGoal> Goals { get; set; }
        
        // Tab-related entities
        DbSet<Tab> Tabs { get; set; }
        DbSet<TabItem> TabItems { get; set; }
        DbSet<TabCategory> TabCategories { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
