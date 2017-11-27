using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Xunit;
using Yaba.Common.DTOs.BudgetDTOs;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities.Test
{
    public class EFBudgetRepositoryTests
    {
        private static DbContextOptions<YabaDBContext> GetInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<YabaDBContext>()
                .UseInMemoryDatabase("in_memory_test_database")
                .Options;
        }

        [Fact]
        public void Using_repository_disposes_of_context()
        {
            var mock = new Mock<IYabaDBContext>();
            using (var repo = new EFBudgetRepository(mock.Object)) ;
            mock.Verify(m => m.Dispose(), Times.Once);
        }
        
        [Fact]
        public async void FindAllBudgets_returns_collection_of_budgets()
        {
            var options = new DbContextOptionsBuilder<YabaDBContext>()
                .UseInMemoryDatabase("test")
                .Options;
            var context = new YabaDBContext(options);

            var budget1 = new Budget {Name = "First"};
            var budget2 = new Budget {Name = "Second"};
            var budget3 = new Budget {Name = "Third"};

            using (var repo = new EFBudgetRepository(context))
            {
                context.Budgets.AddRange(budget1, budget2, budget3);
                await context.SaveChangesAsync();

                var budgets = await repo.FindAllBudgets();
                Assert.Equal(3, budgets.Count);
                context.Database.EnsureDeleted();
            }
        }
        
        [Fact]
        public async void FindBudget_given_existing_id_returns_budget()
        {
            var options = new DbContextOptionsBuilder<YabaDBContext>()
                .UseInMemoryDatabase("test")
                .Options;
            var context = new YabaDBContext(options);

            var budget = new Budget {Name = "New Budget"};

            using (var repo = new EFBudgetRepository(context))
            {
                context.Budgets.Add(budget);
                await context.SaveChangesAsync();
                var budgetDTO = await repo.FindBudget(budget.Id);
                Assert.Equal("New Budget", budgetDTO.Name);
                context.Database.EnsureDeleted();
            }
        }
        
        [Fact]
        public async void FindBudget_given_nonexisting_id_returns_null()
        {
            var options = new DbContextOptionsBuilder<YabaDBContext>()
                .UseInMemoryDatabase("test")
                .Options;
            var context = new YabaDBContext(options);

            using (var repo = new EFBudgetRepository(context))
            {
                var budgetDTO = await repo.FindBudget(Guid.Empty);
                Assert.Null(budgetDTO);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void CreateBudget_creates_budgets()
        {
            var entity = default(Budget);
            var mock = new Mock<IYabaDBContext>();
            
            using (var repo = new EFBudgetRepository(mock.Object))
            {
                mock.Setup(m => m.Budgets.Add(It.IsAny<Budget>()))
                .Callback<Budget>(t => entity = t);
                var bcdto = new BudgetCreateDTO {Name = "My Budget"};
                await repo.CreateBudget(bcdto);
            }
            
            Assert.Equal("My Budget", entity.Name);
        }
    }
}
