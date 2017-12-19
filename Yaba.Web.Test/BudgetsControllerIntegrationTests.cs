using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Entities;
using Yaba.Entities.Budget.Repository;
using Yaba.Entities.Test;
using Yaba.Web.Controllers;
using Yaba.Common.Budget.DTO;
using Newtonsoft.Json;

namespace Yaba.Web.Test
{
    public class BudgetsControllerIntegrationTests
    {
		[Fact]
		public async void Get_returns_all_budgets()
		{
			var ctx = Util.GetNewContext(nameof(Get_returns_all_budgets));
			DbInitializer.Initialize(ctx);
			var expected = ctx.Budgets.Select(b => new BudgetSimpleDto
			{
				Id = b.Id,
				Name = b.Name
			}).ToList();
			using(var ctrl = new BudgetsController(new EFBudgetRepository(ctx)))
			{
				var result = await ctrl.Get() as OkObjectResult;
				Assert.Equal(expected,result.Value);
			}
		}

		[Fact]
		public async void get_given_valid_id_returns_budget()
		{
			var ctx = Util.GetNewContext(nameof(get_given_valid_id_returns_budget));
			DbInitializer.Initialize(ctx);
			var expected = ctx.Budgets.FirstOrDefault();
			using (var ctrl = new BudgetsController(new EFBudgetRepository(ctx)))
			{
				var result = await ctrl.Get(expected.Id) as OkObjectResult;
				var budget = result.Value as BudgetDetailsDto;
				Assert.Equal(expected.Id, budget.Id);
				Assert.Equal(expected.Name, budget.Name);
				Assert.Equal(expected.Recurrings.Count(), budget.Recurrings.Count());
				Assert.Equal(expected.Categories.Count(), budget.Categories.Count());
			}
		}

		[Fact]
		public async void get_given_invalid_id_returns_notFound()
		{
			var ctx = Util.GetNewContext(nameof(get_given_valid_id_returns_budget));
			DbInitializer.Initialize(ctx);

			using (var ctrl = new BudgetsController(new EFBudgetRepository(ctx)))
			{
				var result = await ctrl.Get(Guid.Empty);
				Assert.IsType<NotFoundResult>(result);
			}
		}

		[Fact]
		public async void post_given_create_dto_returns_id_of_budget()
		{
			var ctx = Util.GetNewContext(nameof(post_given_create_dto_returns_id_of_budget));
			DbInitializer.Initialize(ctx);
			var budget = new BudgetCreateUpdateDto
			{
				Name = "Test Budget",
			};
			using (var ctrl = new BudgetsController(new EFBudgetRepository(ctx)))
			{
				var response = await ctrl.Post(budget) as OkObjectResult;
				Assert.IsType<BudgetSimpleDto>(response?.Value);
			}
		}

		[Fact]
		public async void put_given_budget_updates_budget_with_new_name()
		{
			var ctx = Util.GetNewContext(nameof(put_given_budget_updates_budget_with_new_name));
			DbInitializer.Initialize(ctx);
			var FirstBudget = ctx.Budgets.FirstOrDefault();
			var FirstBudgetName = FirstBudget.Name;
			var updateBudget = new BudgetCreateUpdateDto
			{
				 Id = FirstBudget.Id,
				 Name = "new budget name"
			};
			using (var ctrl = new BudgetsController(new EFBudgetRepository(ctx)))
			{
				await ctrl.Put(updateBudget);
				var BudgetAfterUpdate = ctx.Budgets.Find(FirstBudget.Id);

				Assert.NotEqual(updateBudget.Name, FirstBudgetName);
				Assert.Equal(updateBudget.Name, BudgetAfterUpdate.Name);
			}
		}

		[Fact]
		public async void delete_given_id_removes_budget_with_id()
		{
			var ctx = Util.GetNewContext(nameof(delete_given_id_removes_budget_with_id));
			DbInitializer.Initialize(ctx);
			var FirstBudget = ctx.Budgets.FirstOrDefault();
			using (var ctrl = new BudgetsController(new EFBudgetRepository(ctx)))
			{
				await ctrl.Delete(FirstBudget.Id);
				var budget = ctx.Budgets.Find(FirstBudget.Id);
				Assert.Null(budget);
			}
		}

		[Fact]
		public async void delete_given_invalid_id_returns_not_found()
		{
			var ctx = Util.GetNewContext(nameof(delete_given_invalid_id_returns_not_found));
			DbInitializer.Initialize(ctx);

			using (var ctrl = new BudgetsController(new EFBudgetRepository(ctx)))
			{
				var result = await ctrl.Delete(Guid.Empty);
				Assert.IsType<NotFoundResult>(result);
			}
		}
	}
}
