using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities
{
    public interface IYabaDBContext : IDisposable
    {
        DbSet<Budget> Budgets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
