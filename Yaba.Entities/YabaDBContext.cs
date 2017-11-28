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
        public DbSet<BudgetCategory> Categories { get; set; }
        public DbSet<BudgetIncome> Incomes { get; set; }
        public DbSet<BudgetExpense> Expenses { get; set; }
        public DbSet<BudgetEntry> Entries { get; set; }
        public DbSet<BudgetGoal> Goals { get; set; }
        
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
