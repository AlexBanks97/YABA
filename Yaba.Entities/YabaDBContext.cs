using System;
using Microsoft.EntityFrameworkCore;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities
{
    public partial class YabaDBContext : DbContext, IYabaDBContext
    {

        private readonly string _connectionString;

        public DbSet<Budget> Budgets { get; set; }

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
