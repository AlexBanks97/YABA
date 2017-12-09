using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Yaba.Common.Tab.DTO;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Entities;
using Yaba.Entities.Tab.Repository;
using Yaba.Entities.Test;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
    public class TabsControllerIntegrationTests
    {
		[Fact]
		public async void GetReturns()
		{
			var context = Util.GetNewContext(nameof(GetReturns));
			DbInitializer.Initialize(context);
			var expected = context.Tabs
				.Include(t => t.TabItems)
				.Select(b => new TabDto()
				{
					Balance = b.Balance,
					Id = b.Id,
					State = b.State,
					TabItems = b.TabItems.Select(ti => new TabItemSimpleDTO
					{
						Amount = ti.Amount,
						Description = ti.Description,
						Id = ti.Id,
					}),
				}).ToList().OrderBy(c => c);
			using (var controller = new TabsController(new EFTabRepository(context)))
			{
				var response = await controller.Get() as OkObjectResult;
				var result = response.Value as ICollection<TabDto>;
				
				Assert.Equal(expected, result.OrderBy(c => c));
			}
		}
    }
}
