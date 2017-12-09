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
		public async void Find_returns_all_budgets()
		{
			var ctx = Util.GetNewContext(nameof(Find_returns_all_budgets));
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
		public async void 
    }
}
