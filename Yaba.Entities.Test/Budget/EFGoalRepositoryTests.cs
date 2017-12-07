using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Yaba.Entities.Budget;
using Yaba.Common.Budget.DTO.Goal;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Entities.Budget.Repository;

namespace Yaba.Entities.Test.Budget
{
    public class EFGoalRepositoryTests
    {
		[Fact]
		public async void find_given_no_parameters_returns_list_of_all_goals()
		{
			var ctx = Util.GetNewContext(nameof(find_given_no_parameters_returns_list_of_all_goals));
			var cat = new CategoryEntity { Name = "pizza" };
			var cat2 = new CategoryEntity { Name = "piza" };
			ctx.BudgetCategories.AddRange(cat, cat2);

			ctx.BudgetGoals.Add(new GoalEntity { Amount = 2, CategoryEntity = cat, Recurrence = Common.Recurrence.Monthly, });
			ctx.BudgetGoals.Add(new GoalEntity { Amount = 2, CategoryEntity = cat2, Recurrence = Common.Recurrence.Monthly, });
			ctx.SaveChanges();

			using(var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.Find();
				Assert.Equal(2, result.Count());
			}
		}

		[Fact]
		public async void find_given_id_for_goal()
		{
			var ctx = Util.GetNewContext(nameof(find_given_id_for_goal));
			var cat = new CategoryEntity { Name = "pizza" };
			var goal = new GoalEntity { Amount = 2, Recurrence = Common.Recurrence.Weekly, CategoryEntity = cat};
			ctx.AddRange(cat, goal);
			ctx.SaveChanges();

			using(var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.Find(goal.Id);
				Assert.Equal(goal.Id, result.Id);
				Assert.Equal(goal.Amount, result.Amount);
				Assert.Equal(goal.Recurrence, result.Recurrence);
				Assert.Equal(goal.CategoryEntity.Id, result.Category.Id);
			}
		}

		[Fact]
		public async void find_given_non_existing_id_returns_null()
		{
			var ctx = Util.GetNewContext(nameof(find_given_non_existing_id_returns_null));

			using(var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.Find(Guid.NewGuid());
				Assert.Equal(null, result);
			}
		}

		[Fact]
		public async void find_from_category_given_category_with_goal_returns_goal()
		{
			var ctx = Util.GetNewContext(nameof(find_given_id_for_goal));
			var cat = new CategoryEntity { Name = "pizza" };
			var goal = new GoalEntity { Amount = 2, Recurrence = Common.Recurrence.Weekly, CategoryEntity = cat };
			ctx.AddRange(cat, goal);
			ctx.SaveChanges();

			using (var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.FindFromCategory(cat.Id);
				Assert.Equal(goal.Id, result.Id);
				Assert.Equal(goal.Amount, result.Amount);
				Assert.Equal(goal.Recurrence, result.Recurrence);
				Assert.Equal(goal.CategoryEntity.Id, result.Category.Id);
			}
		}

		[Fact]
		public async void find_from_category_given_category_with_no_goal_returns_null()
		{
			var ctx = Util.GetNewContext(nameof(find_given_non_existing_id_returns_null));
			var cat = new CategoryEntity { Name = "pizza" };
			ctx.SaveChanges();

			using (var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.FindFromCategory(cat.Id);
				Assert.Equal(null, result);
			}
		}

		[Fact]
		public async void Create_given_goal_returns_the_id_of_the_created_goal()
		{
			var ctx = Util.GetNewContext(nameof(find_given_non_existing_id_returns_null));
			var cat = new CategoryEntity { Name = "pizza" };
			ctx.BudgetCategories.Add(cat);
			ctx.SaveChanges();
			var simpleCat = new CategorySimpleDto
			{
				Id = cat.Id,
				Name = cat.Name
			};
			var goal = new GoalCreateDto
			{
				Amount = 200,
				CategoryEntity = simpleCat,
				Recurrence = Common.Recurrence.Weekly
			};

			using (var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.CreateGoal(goal);
				var expected = await repo.FindFromCategory(cat.Id);
				Assert.Equal(expected.Id, result);
			}
		}

		[Fact]
		public async void update_goal_returns_true_given_existing_budget()
		{
			var ctx = Util.GetNewContext(nameof(find_given_id_for_goal));
			var cat = new CategoryEntity { Name = "pizza" };
			var goal = new GoalEntity { Amount = 2, Recurrence = Common.Recurrence.Weekly, CategoryEntity = cat };
			ctx.AddRange(cat, goal);
			ctx.SaveChanges();

			var goalUpdate = new GoalDto
			{
				Id = goal.Id,
				Amount = 100,
				Recurrence = goal.Recurrence
			};

			using(var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.UpdateGoal(goalUpdate);
				var post = ctx.BudgetGoals.Find(goal.Id);

				Assert.True(result);
				Assert.Equal(goalUpdate.Amount, post.Amount);
			}
		}

		[Fact]
		public async void update_goal_returns_false_given_non_existing_goal()
		{
			var ctx = Util.GetNewContext(nameof(find_given_id_for_goal));

			var goalUpdate = new GoalDto
			{
				Id = Guid.NewGuid(),
				Amount = 100
			};

			using (var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.UpdateGoal(goalUpdate);

				Assert.False(result);
			}
		}

		[Fact]
		public async void delete_given_valid_goal_removes_it()
		{
			var ctx = Util.GetNewContext(nameof(find_given_id_for_goal));
			var cat = new CategoryEntity { Name = "pizza" };
			var goal = new GoalEntity { Amount = 2, Recurrence = Common.Recurrence.Weekly, CategoryEntity = cat };
			ctx.AddRange(cat, goal);
			ctx.SaveChanges();

			using(var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.DeleteGoal(goal.Id);
				Assert.True(result);
			}
		}

		[Fact]
		public async void delete_given_non_existing_goal_returns_false()
		{
			var ctx = Util.GetNewContext(nameof(delete_given_non_existing_goal_returns_false));

			using(var repo = new EFGoalRepository(ctx))
			{
				var result = await repo.DeleteGoal(Guid.NewGuid());
				Assert.False(result);
			}
		}
	}
}
