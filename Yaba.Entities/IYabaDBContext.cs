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
        DbSet<Budget> Budgets { get; set; }
        DbSet<Tab> Tabs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
