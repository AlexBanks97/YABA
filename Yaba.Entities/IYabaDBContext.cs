using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yaba.Entities.Budget;
using Yaba.Entities.Tab;

namespace Yaba.Entities
{
	public interface IYabaDBContext : IDisposable
	{
		// Budget-related entities:
		DbSet<BudgetEntity> Budgets { get; set; }
		DbSet<CategoryEntity> BudgetCategories { get; set; }
		DbSet<RecurringEntity> BudgetRecurrings { get; set; }
		DbSet<EntryEntity> BudgetEntries { get; set; }
		DbSet<GoalEntity> BudgetGoals { get; set; }

		// Tab-related entities
		DbSet<TabEntity> Tabs { get; set; }
		DbSet<ItemEntity> TabItems { get; set; }
		DbSet<ItemCategoryEntity> TabItemCategories { get; set; }

		Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
	}
}
