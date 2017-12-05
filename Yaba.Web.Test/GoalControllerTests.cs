using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Goal;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
	public class GoalControllerTests
    {
		[Fact]
		public async void Get_with_no_parameters_returns_All_Goals()
		{
			var goals = new List<GoalDto>{
				new GoalDto(),
				new GoalDto()
			};

			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.Find())
				.ReturnsAsync(goals);

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Get() as OkObjectResult;
				Assert.Equal(goals, result.Value);
			}

		}

		[Fact]
		public async void get_given_existing_Guid_Returns_goal()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.Find(It.IsAny<Guid>())).ReturnsAsync(new GoalDto());

			using(var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Get(Guid.NewGuid()) as OkObjectResult;
				Assert.IsType<GoalDto>(result.Value);
			}
		}

		[Fact]
		public async void get_given_non_existing_Guid_returns_goal()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.Find(It.IsAny<Guid>())).ReturnsAsync(default(GoalDto));

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Get(Guid.NewGuid());
				Assert.IsType<NotFoundResult>(result);
			}
		}

		[Fact]
		public async void Post_given_valid_model_returns_a_guid()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.CreateGoal(It.IsAny<GoalCreateDto>())).ReturnsAsync(Guid.NewGuid());

			using(var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Post(new GoalCreateDto()) as CreatedAtActionResult;
				Assert.IsType<Guid>(result.RouteValues.Values.First());
			}
		}

		[Fact(Skip ="Model validation needed")]
		public async void Post_given_invalid_model_returns_badrequest()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.CreateGoal(It.IsAny<GoalCreateDto>())).ReturnsAsync(Guid.NewGuid());

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Post(new GoalCreateDto());
				Assert.IsType<BadRequestResult>(result);
			}
		}

		[Fact]
		public async void Put_Given_existing_goal_returns_noContent()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.UpdateGoal(It.IsAny<GoalDto>())).ReturnsAsync(true);

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Put(new GoalDto()) as NoContentResult;
				Assert.IsType<NoContentResult>(result);
			}
		}

		[Fact]
		public async void Put_Given_non_existing_goal_returns_noContent()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.UpdateGoal(It.IsAny<GoalDto>())).ReturnsAsync(false);

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Put(new GoalDto()) as NotFoundResult;
				Assert.IsType<NotFoundResult>(result);
			}
		}

		[Fact(Skip ="no model states")]
		public async void Put_Given_invalid_model_returns_noContent()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.UpdateGoal(It.IsAny<GoalDto>())).ReturnsAsync(false);

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Put(new GoalDto()) as BadRequestResult;
				Assert.IsType<BadRequestResult>(result);
			}
		}

		[Fact]
		public async void Delete_given_existing_Id_returns_noContent()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.DeleteGoal(It.IsAny<Guid>())).ReturnsAsync(true);

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Delete(Guid.NewGuid()) as NoContentResult;
				Assert.IsType<NoContentResult>(result);
			}
		}

		[Fact]
		public async void Delete_given_non_existing_Id_returns_noFound()
		{
			var mock = new Mock<IGoalRepository>();
			mock.Setup(m => m.DeleteGoal(It.IsAny<Guid>())).ReturnsAsync(false);

			using (var ctrl = new GoalController(mock.Object))
			{
				var result = await ctrl.Delete(Guid.NewGuid()) as NotFoundResult;
				Assert.IsType<NotFoundResult>(result);
			}
		}
	}
}
