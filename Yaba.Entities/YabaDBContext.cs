using System;
using Microsoft.EntityFrameworkCore;
using Yaba.Entities.BudgetEntities;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities
{
    public class YabaDBContext : DbContext, IYabaDBContext
    {

        private readonly string _connectionString;

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Tab> Tabs { get; set; }
        
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
