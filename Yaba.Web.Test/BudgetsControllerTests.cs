
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
	public class BudgetsControllerTests
	{
		[Fact]
		public async void Get_given_no_params_returns_Ok_with_list_of_budgets()
		{
			var mock = new Mock<IBudgetRepository>();
			var budgets = new List<BudgetSimpleDto>
			{
				new BudgetSimpleDto(),
				new BudgetSimpleDto(),
			};
			mock.Setup(m => m.All())
				.ReturnsAsync(budgets);

			using (var ctrl = new BudgetsController(mock.Object))
			{
				var response = await ctrl.Get() as OkObjectResult;
				Assert.Equal(budgets, response.Value);
			}
		}

		[Fact]
		public async void Get_given_existing_owner_w_budgets_returns_Ok()
		{
			var budgets = new List<BudgetSimpleDto>
			{
				new BudgetSimpleDto(),
				new BudgetSimpleDto(),
			};

			var mock = new Mock<IBudgetRepository>();
			mock.Setup(m => m.AllByUser("uid"))
				.ReturnsAsync(budgets);

			using (var ctrl = new BudgetsController(mock.Object))
			{
				var response = await ctrl.Get("uid") as OkObjectResult;
				Assert.Equal(response.Value, budgets);
			}
		}

		[Fact(DisplayName = "Get Budget given GUID returns OK")]
		public async void Get_given_existing_guid_returns_OK_with_budget()
		{
			var budget = new BudgetDetailsDto {Name = "Budget"};

			var guid = Guid.NewGuid();

			var mock = new Mock<IBudgetRepository>();
			mock.Setup(m => m.Find(guid))
				.ReturnsAsync(budget);

			using (var controller = new BudgetsController(mock.Object))
			{
				var actual = await controller.Get(guid) as OkObjectResult;
				Assert.Equal(budget, actual.Value);
			}
		}

		[Fact(DisplayName = "Get Budget given non-existing GUID returns NotFound")]
		public async void Get_given_nonexisting_guid_returns_NotFound()
		{
			var guid = Guid.NewGuid();
			var mock = new Mock<IBudgetRepository>();
			mock.Setup(m => m.Find(guid))
				.ReturnsAsync(default(BudgetDetailsDto));

			using (var controller = new BudgetsController(mock.Object))
			{
				var response = await controller.Get(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}

		[Fact(DisplayName = "Delete given nonexisting guid returns NotFound")]
		public async void Delete_given_nonexisting_guid_returns_NotFound()
		{
			var guid = Guid.NewGuid();
			var mock = new Mock<IBudgetRepository>();
			mock.Setup(m => m.Delete(guid))
				.ReturnsAsync(false);

			using (var ctrl = new BudgetsController(mock.Object))
			{
				var response = await ctrl.Delete(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}

		[Fact(DisplayName = "Delete given existing guid returns NoContent")]
		public async void Delete_given_existing_guid_returns_NoContent()
		{
			var mock = new Mock<IBudgetRepository>();
			mock.Setup(m => m.Delete(It.IsAny<Guid>()))
				.ReturnsAsync(true);

			using (var ctrl = new BudgetsController(mock.Object))
			{
				var response = await ctrl.Delete(Guid.NewGuid());
				Assert.IsType<NoContentResult>(response);
			}
		}
	}
}
