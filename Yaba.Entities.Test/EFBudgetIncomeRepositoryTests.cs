using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities.Test
{
    public class EFBudgetIncomeRepositoryTests
    {
        [Fact(DisplayName = "Create budget income creates a budget income")]
        public async void Create_Budget_Income_Creates_Budget_Income()
        {
            var context = Util.GetNewContext(nameof(Create_Budget_Income_Creates_Budget_Income));

            var entity = default(BudgetIncome);
            var mock = new Mock<IYabaDBContext>();

            mock.Setup(m => m.BudgetIncomes.Add(It.IsAny<BudgetIncome>()))
                .Callback<BudgetIncome>(t => entity = t);

            var budgetIncome = new BudgetIncome
            {
                Name = "Life support from mom",
                Amount = 8000,
                Recurrence = Recurrence.Monthly,
            };

            context.BudgetIncomes.Add(budgetIncome);
            await context.SaveChangesAsync();
        }
    }


}
