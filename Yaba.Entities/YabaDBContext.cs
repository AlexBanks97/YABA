using Microsoft.EntityFrameworkCore;
using Yaba.Common.User;
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

		// User-related entities
		public DbSet<UserEntity> Users { get; set; }

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
		{
			modelBuilder.Entity<TabEntity>()
				.HasOne(t => t.UserOne)
				.WithMany();

			modelBuilder.Entity<TabEntity>()
				.HasOne(t => t.UserTwo)
				.WithMany();

			/*
		odelBuilder.Entity<AnEventUser>()
			.HasOne(pt => pt.AnEvent)
			.WithMany(p => p.AnEventUsers)
			.HasForeignKey(pt => pt.AnEventId); */

		}
	}
}
