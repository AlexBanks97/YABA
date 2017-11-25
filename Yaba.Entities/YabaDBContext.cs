using System;
using Microsoft.EntityFrameworkCore;
using Yaba.Entities.BudgetEntities;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities
{
    public partial class YabaDBContext : DbContext, IYabaDBContext
    {

        private readonly string _connectionString;

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Tab> Tabs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
