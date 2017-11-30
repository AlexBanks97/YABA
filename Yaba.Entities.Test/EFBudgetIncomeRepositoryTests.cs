﻿using Moq;
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

        [Fact(DisplayName = "FindAllBudgetIncomes returns collection of incomes")]
        public async void Find_All_Budget_Incomes_Returns_Collection_Of_Incomes()
        {
            var context = Util.GetNewContext(nameof(Find_All_Budget_Incomes_Returns_Collection_Of_Incomes));
            
            var budgetIncome1 = new BudgetIncome { Name = "Life support from mom", Amount = 12000, Recurrence = Recurrence.Monthly };
            var budgetIncome2 = new BudgetIncome { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };
            var budgetIncome3 = new BudgetIncome { Name = "Paycheck", Amount = 4500, Recurrence = Recurrence.Monthly };

            using (var repo = new EFBudgetIncomeRepository(context))
            {
                context.BudgetIncomes.AddRange(budgetIncome1, budgetIncome2, budgetIncome3);
                await context.SaveChangesAsync();

                var budgetFinder = await repo.FindAllBudgetIncomes();
                Assert.Equal(3, budgetFinder.Count);
            }
        }
    }
}
