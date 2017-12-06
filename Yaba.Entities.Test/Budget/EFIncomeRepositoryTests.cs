using System.Collections.Generic;
using Moq;
using Xunit;
using Yaba.Common;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Recurring;
using Yaba.Entities.Budget;
using System;
using System.Linq;

namespace Yaba.Entities.Test.Budget
{
	public class EFIncomeRepositoryTests
	{
		[Fact(DisplayName = "Create budget income creates a budget income")]
		public async void Create_Budget_Income_Creates_Budget_Income()
		{
			var context = Util.GetNewContext(nameof(Create_Budget_Income_Creates_Budget_Income));

			var entity = default(RecurringEntity);
			var mock = new Mock<IYabaDBContext>();

			mock.Setup(m => m.BudgetRecurrings.Add(It.IsAny<RecurringEntity>()))
				.Callback<RecurringEntity>(t => entity = t);

			var budgetIncome = new RecurringEntity
			{
				Name = "Life support from mom",
				Amount = 8000,
				Recurrence = Recurrence.Monthly,
			};

			context.BudgetRecurrings.Add(budgetIncome);
			await context.SaveChangesAsync();
		}

		[Fact(DisplayName = "FindAllBudgetRecurrings returns collection of incomes")]
		public async void Find_All_Budget_Incomes_Returns_Collection_Of_Incomes()
		{
			var context = Util.GetNewContext(nameof(Find_All_Budget_Incomes_Returns_Collection_Of_Incomes));

			var budgetIncome1 = new RecurringEntity { Name = "Life support from mom", Amount = 12000, Recurrence = Recurrence.Monthly };
			var budgetIncome2 = new RecurringEntity { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };
			var budgetIncome3 = new RecurringEntity { Name = "Paycheck", Amount = 4500, Recurrence = Recurrence.Monthly };

			using (var repo = new EFRecurringRepository(context))
			{
				context.BudgetRecurrings.AddRange(budgetIncome1, budgetIncome2, budgetIncome3);
				await context.SaveChangesAsync();

				var budgetFinder = await repo.FindAllBudgetRecurrings();
				Assert.Equal(3, budgetFinder.Count);
			}
		}

		[Fact(DisplayName = "FindBudgetRecurring by Id returns budget indcome with supplied Id")]
		public async void Find_Budget_Income_With_Specific_Id_Returns_Specific_Budget_Income()
		{
			var context = Util.GetNewContext(nameof(Find_Budget_Income_With_Specific_Id_Returns_Specific_Budget_Income));

			var budgetIncome = new RecurringEntity { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };

			using (var repo = new EFRecurringRepository(context))
			{
				context.BudgetRecurrings.Add(budgetIncome);
				await context.SaveChangesAsync();
				var budgetIncomeToFind = repo.FindBudgetRecurring(budgetIncome.Id);
				Assert.Equal("Life support from pops", budgetIncome.Name);
				Assert.Equal(8000, budgetIncome.Amount);
				Assert.Equal(Recurrence.Monthly, budgetIncome.Recurrence);
			}
		}

		[Fact(DisplayName = "FindAllBudgetRecurringsFromSpecificBudget returns a collection of incomes for the budget")]
		public async void Find_All_Budget_Incomes_Within_Specific_Budget_Returns_Collection_Of_Incomes()
		{
			var context = Util.GetNewContext(nameof(Find_All_Budget_Incomes_Within_Specific_Budget_Returns_Collection_Of_Incomes));

			var budgetIncome1 = new RecurringSimpleDto { Name = "Life support from mom", Amount = 12000, Recurrence = Recurrence.Monthly };
			var budgetIncome2 = new RecurringSimpleDto { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };

			var budget = new BudgetDto { Name = "Main budget", Recurrings = new List<RecurringSimpleDto> { budgetIncome1, budgetIncome2 } };

			using (var repo = new EFRecurringRepository(context))
			{
				var incomesToFind = await repo.FindAllBudgetRecurringsFromSpecificBudget(budget);
				Assert.Equal(2, incomesToFind.Count);
			}
		}

		[Fact(DisplayName = "UpdateBudgetRecurring returns true")]
		public async void Update_Budget_Income_Returns_True()
		{
			var context = Util.GetNewContext(nameof(Update_Budget_Income_Returns_True));
			var budgetIncome = new RecurringEntity { Name = "Paycheck" };

			context.BudgetRecurrings.Add(budgetIncome);
			await context.SaveChangesAsync();

			using (var repo = new EFRecurringRepository(context))
			{
				var dto = new RecurringUpdateDto
				{
					Id = budgetIncome.Id,
					Name = "Paycheck v2",
					Amount = 1500,
					Recurrence = Recurrence.Monthly
				};
				var updated = await repo.UpdateBudgetRecurring(dto);
				Assert.True(updated);
				Assert.Equal("Paycheck v2", budgetIncome.Name);
				Assert.Equal(1500, budgetIncome.Amount);
				Assert.Equal(Recurrence.Monthly, budgetIncome.Recurrence);
			}
		}

		[Fact]
		public async void Delete_Returns_False_On_NonExisting_Key()
		{
			var context = Util.GetNewContext(nameof(Delete_Returns_False_On_NonExisting_Key));

			using (var repo = new EFRecurringRepository(context))
			{
				var deleted = await repo.DeleteBudgetRecurring(Guid.NewGuid());
				Assert.False(deleted);
			}
			
		}

		[Fact]
		public async void Delete_Returns_True_On_Existing_Key()
		{
			var context = Util.GetNewContext(nameof(Delete_Returns_True_On_Existing_Key));
			
			var budgetIncomeToDelete = new RecurringEntity { Name = "Delete me", Amount = 0 };
			context.BudgetRecurrings.Add(budgetIncomeToDelete);
			await context.SaveChangesAsync();

			using (var repo = new EFRecurringRepository(context))
			{
				var deleted = await repo.DeleteBudgetRecurring(budgetIncomeToDelete.Id);
				Assert.True(deleted);
			}
		}

	}
}
