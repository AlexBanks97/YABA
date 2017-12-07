using System.Collections.Generic;
using Moq;
using Xunit;
using Yaba.Common;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Recurring;
using Yaba.Entities.Budget;
using System;
using System.Linq;
using Yaba.Entities.Budget.Repository;

namespace Yaba.Entities.Test.Budget
{
	public class EFRecurringRepositoryTests
	{
		public async void Create_Budget_Recurring_Creates_Budget_Recurring()
		{
			var context = Util.GetNewContext(nameof(Create_Budget_Recurring_Creates_Budget_Recurring));

			var entity = default(RecurringEntity);
			var mock = new Mock<IYabaDBContext>();

			mock.Setup(m => m.BudgetRecurrings.Add(It.IsAny<RecurringEntity>()))
				.Callback<RecurringEntity>(t => entity = t);

			var budgetRecurring = new RecurringEntity
			{
				Name = "Life support from mom",
				Amount = 8000,
				Recurrence = Recurrence.Monthly,
			};

			context.BudgetRecurrings.Add(budgetRecurring);
			await context.SaveChangesAsync();
		}

		public async void Find_All_Budget_Recurrings_Returns_Collection_Of_Recurrings()
		{
			var context = Util.GetNewContext(nameof(Find_All_Budget_Recurrings_Returns_Collection_Of_Recurrings));

			var recurring1 = new RecurringEntity { Name = "Life support from mom", Amount = 12000, Recurrence = Recurrence.Monthly };
			var recurring2 = new RecurringEntity { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };
			var recurring3 = new RecurringEntity { Name = "Paycheck", Amount = 4500, Recurrence = Recurrence.Monthly };

			using (var repo = new EFRecurringRepository(context))
			{
				context.BudgetRecurrings.AddRange(recurring1, recurring2, recurring3);
				await context.SaveChangesAsync();

				var budgetFinder = await repo.FindAllBudgetRecurrings();
				Assert.Equal(3, budgetFinder.Count);
			}
		}

		public async void Find_Budget_Recurring_With_Specific_Id_Returns_Specific_Budget_Recurring()
		{
			var context = Util.GetNewContext(nameof(Find_Budget_Recurring_With_Specific_Id_Returns_Specific_Budget_Recurring));

			var recurring = new RecurringEntity { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };

			using (var repo = new EFRecurringRepository(context))
			{
				context.BudgetRecurrings.Add(recurring);
				await context.SaveChangesAsync();
				var budgetIncomeToFind = repo.FindBudgetRecurring(recurring.Id);
				Assert.Equal("Life support from pops", recurring.Name);
				Assert.Equal(8000, recurring.Amount);
				Assert.Equal(Recurrence.Monthly, recurring.Recurrence);
			}
		}

		public async void Find_All_Budget_Recurrings_Within_Specific_Budget_Returns_Collection_Of_Recurrings()
		{
			var context = Util.GetNewContext(nameof(Find_All_Budget_Recurrings_Within_Specific_Budget_Returns_Collection_Of_Recurrings));

			var recurring1 = new RecurringSimpleDto { Name = "Life support from mom", Amount = 12000, Recurrence = Recurrence.Monthly };
			var recurring2 = new RecurringSimpleDto { Name = "Life support from pops", Amount = 8000, Recurrence = Recurrence.Monthly };

			var budget = new BudgetDto { Name = "Main budget", Recurrings = new List<RecurringSimpleDto> { recurring1, recurring2 } };

			using (var repo = new EFRecurringRepository(context))
			{
				var recurringsToFind = await repo.FindAllBudgetRecurringsFromSpecificBudget(budget);
				Assert.Equal(2, recurringsToFind.Count);
			}
		}

		public async void Update_Budget_Recurring_Returns_True()
		{
			var context = Util.GetNewContext(nameof(Update_Budget_Recurring_Returns_True));
			var recurring = new RecurringEntity { Name = "Paycheck" };

			context.BudgetRecurrings.Add(recurring);
			await context.SaveChangesAsync();

			using (var repo = new EFRecurringRepository(context))
			{
				var dto = new RecurringUpdateDto
				{
					Id = recurring.Id,
					Name = "Paycheck v2",
					Amount = 1500,
					Recurrence = Recurrence.Monthly
				};
				var updated = await repo.UpdateBudgetRecurring(dto);
				Assert.True(updated);
				Assert.Equal("Paycheck v2", recurring.Name);
				Assert.Equal(1500, recurring.Amount);
				Assert.Equal(Recurrence.Monthly, recurring.Recurrence);
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
			
			var recurring = new RecurringEntity { Name = "Delete me", Amount = 0 };
			context.BudgetRecurrings.Add(recurring);
			await context.SaveChangesAsync();

			using (var repo = new EFRecurringRepository(context))
			{
				var deleted = await repo.DeleteBudgetRecurring(recurring.Id);
				Assert.True(deleted);
			}
		}

	}
}
