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
        DbSet<Category> Categories { get; set; }
        DbSet<Income> Incomes { get; set; } 
        DbSet<Expense> Expenses { get; set; }
        DbSet<Entry> Entries { get; set; }
        DbSet<Goal> Goals { get; set; }
        
        // Tab-related entities
        DbSet<Tab> Tabs { get; set; }
        DbSet<TabItem> TabItems { get; set; }
        DbSet<TabCategory> TabCategories { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
