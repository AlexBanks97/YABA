using System;
using Microsoft.EntityFrameworkCore;
using Yaba.Entities.BudgetEntities;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities
{
    public class YabaDBContext : DbContext, IYabaDBContext
    {

        private readonly string _connectionString;

        // Budget-related entities
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<BudgetIncome> BudgetIncomes { get; set; }
        public DbSet<BudgetExpense> BudgetExpenses { get; set; }
        public DbSet<BudgetEntry> BudgetEntries { get; set; }
        public DbSet<BudgetGoal> BudgetGoals { get; set; }

        // Tab-related entities
        public DbSet<Tab> Tabs { get; set; }
        public DbSet<TabItem> TabItems { get; set; }
        public DbSet<TabCategory> TabCategories { get; set; }

        public YabaDBContext() {}

        public YabaDBContext(DbContextOptions<YabaDBContext> options) : base(options) {}
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
