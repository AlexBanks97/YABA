using Moq;
using System;
using System.Linq;
using Xunit;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities.Test
{
    public class EFBudgetRepositoryTests
    {
        [Fact]
        public async void Hejsa()
        {
            /* var budget = new Budget
            {
                Name = "Hejsa"
            };

            var mock = new Mock<IYabaDBContext>();
            mock.Setup(m => m.Budgets.FirstOrDefault(It.IsAny<Func<object, bool>>()))
                .Returns(budget);

            var repo = new EFBudgetRepository(mock.Object);
            var budgetDTO = await repo.FindBudget(42); */

            Assert.True(true);
        }
    }
}
