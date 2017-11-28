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
        DbSet<BudgetCategory> BudgetCategories { get; set; }
        DbSet<BudgetIncome> BudgetIncomes { get; set; } 
        DbSet<BudgetExpense> BudgetExpenses { get; set; }
        DbSet<BudgetEntry> BudgetEntries { get; set; }
        DbSet<BudgetGoal> BudgetGoals { get; set; }
        
        // Tab-related entities
        DbSet<Tab> Tabs { get; set; }
        DbSet<TabItem> TabItems { get; set; }
        DbSet<TabCategory> TabCategories { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
