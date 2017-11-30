using System.Collections.Generic;
using Moq;
using Xunit;
using Yaba.Common;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Income;
using Yaba.Entities.Budget;

namespace Yaba.Entities.Test.Budget
{
    public class EFIncomeRepositoryTests
    {
        [Fact(DisplayName = "Create budget income creates a budget income")]
        public async void Create_Budget_Income_Creates_Budget_Income()
        {
            var context = Util.GetNewContext(nameof(Create_Budget_Income_Creates_Budget_Income));

            var entity = default(IncomeEntity);
            var mock = new Mock<IYabaDBContext>();

            mock.Setup(m => m.BudgetIncomes.Add(It.IsAny<IncomeEntity>()))
                .Callback<IncomeEntity>(t => entity = t);

            var budgetIncome = new IncomeEntity
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
            
            var budgetIncome1 = new IncomeEntity { Name = "Life support from mom", Amount = 12000, Recurrence = Recurrence.Monthly };
            var budgetIncome2 = new IncomeEntity { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };
            var budgetIncome3 = new IncomeEntity { Name = "Paycheck", Amount = 4500, Recurrence = Recurrence.Monthly };

            using (var repo = new EFIncomeRepository(context))
            {
                context.BudgetIncomes.AddRange(budgetIncome1, budgetIncome2, budgetIncome3);
                await context.SaveChangesAsync();

                var budgetFinder = await repo.FindAllBudgetIncomes();
                Assert.Equal(3, budgetFinder.Count);
            }
        }

        [Fact(DisplayName = "FindBudgetIncome by Id returns budget indcome with supplied Id")]
        public async void Find_Budget_Income_With_Specific_Id_Returns_Specific_Budget_Income()
        {
            var context = Util.GetNewContext(nameof(Find_Budget_Income_With_Specific_Id_Returns_Specific_Budget_Income));

            var budgetIncome = new IncomeEntity { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };

            using (var repo = new EFIncomeRepository(context))
            {
                context.BudgetIncomes.Add(budgetIncome);
                await context.SaveChangesAsync();
                var budgetIncomeToFind = repo.FindBudgetIncome(budgetIncome.Id);
                Assert.Equal("Life support from pops", budgetIncome.Name);
                Assert.Equal(8000, budgetIncome.Amount);
                Assert.Equal(Recurrence.Monthly, budgetIncome.Recurrence);
            }
        }

        [Fact(DisplayName = "FindAllBudgetIncomesFromSpecificBudget returns a collection of incomes for the budget")]
        public async void Find_All_Budget_Incomes_Within_Specific_Budget_Returns_Collection_Of_Incomes()
        {
            var context = Util.GetNewContext(nameof(Find_All_Budget_Incomes_Within_Specific_Budget_Returns_Collection_Of_Incomes));

            var budgetIncome1 = new IncomeSimpleDto { Name = "Life support from mom", Amount = 12000, Recurrence = Recurrence.Monthly };
            var budgetIncome2 = new IncomeSimpleDto { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };

            var budget = new BudgetDto { Name = "Main budget", Incomes = new List<IncomeSimpleDto> { budgetIncome1, budgetIncome2 } };

            using (var repo = new EFIncomeRepository(context))
            {
                var incomesToFind = await repo.FindAllBudgetIncomesFromSpecificBudget(budget);
                Assert.Equal(2, incomesToFind.Count);
            }
        }

        [Fact(DisplayName = "UpdateBudgetIncome returns true")]
        public async void Update_Budget_Income_Returns_True()
        {
            var context = Util.GetNewContext(nameof(Update_Budget_Income_Returns_True));
            var budgetIncome = new IncomeEntity { Name = "Paycheck" };

            context.BudgetIncomes.Add(budgetIncome);
            await context.SaveChangesAsync();

            using (var repo = new EFIncomeRepository(context))
            {
                var dto = new IncomeUpdateDto
                {
                    Id = budgetIncome.Id,
                    Name = "Paycheck v2",
                    Amount = 1500,
                    Recurrence = Recurrence.Monthly
                };
                var updated = await repo.UpdateBudgetIncome(dto);
                Assert.True(updated);
                Assert.Equal("Paycheck v2", budgetIncome.Name);
                Assert.Equal(1500, budgetIncome.Amount);
                Assert.Equal(Recurrence.Monthly, budgetIncome.Recurrence);
            }
        }
    }
}
