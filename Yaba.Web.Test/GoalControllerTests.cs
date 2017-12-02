using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;

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
				.Returns(goals);

		}
	}
}
