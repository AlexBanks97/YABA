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

        private static IYabaDBContext GetNewContext()
        {
            var ctx = new YabaDBContext(GetInMemoryDatabase());
            ctx.Database.EnsureDeleted();
            return ctx;
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
            var context = GetNewContext();

            var budget1 = new Budget {Name = "First"};
            var budget2 = new Budget {Name = "Second"};
            var budget3 = new Budget {Name = "Third"};

            using (var repo = new EFBudgetRepository(context))
            {
                context.Budgets.AddRange(budget1, budget2, budget3);
                await context.SaveChangesAsync();

                var budgets = await repo.FindAllBudgets();
                Assert.Equal(3, budgets.Count);
            }
        }
        
        [Fact]
        public async void FindBudget_given_existing_id_returns_budget()
        {
            var context = GetNewContext();

            var budget = new Budget {Name = "New Budget"};

            using (var repo = new EFBudgetRepository(context))
            {
                context.Budgets.Add(budget);
                await context.SaveChangesAsync();
                var budgetDTO = await repo.FindBudget(budget.Id);
                Assert.Equal("New Budget", budgetDTO.Name);
            }
        }
        
        [Fact]
        public async void FindBudget_given_nonexisting_id_returns_null()
        {
            var context = GetNewContext();

            using (var repo = new EFBudgetRepository(context))
            {
                var budgetDTO = await repo.FindBudget(Guid.Empty);
                Assert.Null(budgetDTO);
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
                var bcdto = new BudgetCreateUpdateDTO {Name = "My Budget"};
                await repo.CreateBudget(bcdto);
            }
            
            Assert.Equal("My Budget", entity.Name);
        }

        [Fact]
        public async void UpdateBudget_updates_existing_budget()
        {
            var context = GetNewContext();
            var budget = new Budget
            {
                Name = "Not updated"
            };
            context.Budgets.Add(budget);
            await context.SaveChangesAsync();

            using (var repo = new EFBudgetRepository(context))
            {
                var updatedBudget = new BudgetCreateUpdateDTO
                {
                    Id = budget.Id,
                    Name = "Updated",
                };
                var updated = await repo.UpdateBudget(updatedBudget);
                Assert.True(updated);
                Assert.Equal("Updated", budget.Name);
            }
        }

        [Fact]
        public async void UpdateBudget_given_dto_with_no_id_returns_false()
        {
            var context = GetNewContext();
            using (var repo = new EFBudgetRepository(context))
            {
                var updated = await repo.UpdateBudget(new BudgetCreateUpdateDTO());
                Assert.False(updated);
            }
        }

        [Fact]
        public async void UpdateBudget_given_dto_with_nonexisting_id_returns_false()
        {
            var context = GetNewContext();
            using (var repo = new EFBudgetRepository(context))
            {
                var dto = new BudgetCreateUpdateDTO
                {
                    Id = Guid.NewGuid(),
                };
                var updated = await repo.UpdateBudget(dto);
                Assert.False(updated);
            }
        }
    }
}
