using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yaba.Entities.Budget;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities
{
    public interface IYabaDBContext : IDisposable
    {
        // Budget-related entities:
        DbSet<Budget.BudgetEntity> Budgets { get; set; }
        DbSet<CategoryEntity> BudgetCategories { get; set; }
        DbSet<IncomeEntity> BudgetIncomes { get; set; } 
        DbSet<ExpenseEntity> BudgetExpenses { get; set; }
        DbSet<EntryEntity> BudgetEntries { get; set; }
        DbSet<GoalEntity> BudgetGoals { get; set; }
        
        // Tab-related entities
        DbSet<Tab> Tabs { get; set; }
        DbSet<TabItem> TabItems { get; set; }
        DbSet<TabCategory> TabCategories { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
