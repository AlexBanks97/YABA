using Moq;
using System;
using System.Linq;
using System.Net.Cache;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities.Test
{
    public class EFBudgetRepositoryTests
    {
        [Fact]
        public async void Hejsa()
        {
            var options = new DbContextOptionsBuilder<YabaDBContext>()
                .UseInMemoryDatabase("test")
                .Options;
            var context = new YabaDBContext(options);

            var budget = new Budget {Name = "New Budget"};

            context.Budgets.Add(budget);
            await context.SaveChangesAsync();
            
            var repo = new EFBudgetRepository(context);
            var budgetDTO = await repo.FindBudget(budget.Id);
            
            Assert.Equal("New Budget", budgetDTO.Name);
        }
    }
}
