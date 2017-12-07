using Microsoft.EntityFrameworkCore;
using Yaba.Entities.Budget;
using Yaba.Entities.Tab;

namespace Yaba.Entities
{
	public class YabaDBContext : DbContext, IYabaDBContext
	{

		private readonly string _connectionString;

		// Budget-related entities
		public DbSet<BudgetEntity> Budgets { get; set; }
		public DbSet<CategoryEntity> BudgetCategories { get; set; }
		public DbSet<RecurringEntity> BudgetRecurrings { get; set; }
		public DbSet<EntryEntity> BudgetEntries { get; set; }
		public DbSet<GoalEntity> BudgetGoals { get; set; }

		// Tab-related entities
		public DbSet<TabEntity> Tabs { get; set; }
		public DbSet<ItemEntity> TabItems { get; set; }

		public YabaDBContext() { }

		public YabaDBContext(DbContextOptions<YabaDBContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{ }
	}
}
