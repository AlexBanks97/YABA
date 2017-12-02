using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
    public class TabItemControllerTests
    {
		[Fact]
		public async void GetTabItem_given_existing_id_returns_ok()
		{
			var mock = new Mock<ITabItemRepository>();

			var guid = Guid.NewGuid();
			var tabItem = new TabItemSimpleDTO();

			mock.Setup(m => m.Find(guid)).ReturnsAsync(tabItem);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.IsType<OkObjectResult>(response);
			}

				//var tabItem = new TabItemSimpleDTO { Description = "Pizza", Amount = 300, };
				//var guid = Guid.NewGuid();

				//var mock = new Mock<ITabItemRepository>();
				//mock.Setup(m => m.Find(guid))
				//	.ReturnsAsync(tabItem);

				//using (var controller = new TabItemController(mock.Object))
				//{
				//	var actual = await controller.Get(guid) as OkObjectResult;
				//	Assert.Equal(tabItem, actual.Value);
				//}
			}

		[Fact]
		public async void GetTabItem_given_nonexisting_id_returns_notfound()
		{
			var guid = Guid.NewGuid();
			var mock = new Mock<ITabItemRepository>();

			mock.Setup(m => m.Find(guid))
				.ReturnsAsync(default(TabItemSimpleDTO));

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Get(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}
    }
}
