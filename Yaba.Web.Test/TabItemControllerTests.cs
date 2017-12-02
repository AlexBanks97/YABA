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
		}

		[Fact]
		public async void GetTabItem_given_TabItem_with_id_returns_id()
		{
			var mock = new Mock<ITabItemRepository>();

			var guid = Guid.NewGuid();
			var tabItem = new TabItemSimpleDTO { Id = guid, Description = "pizza", Amount = 120 };

			mock.Setup(m => m.Find(guid)).ReturnsAsync(tabItem);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.Equal(tabItem.Id, response.Value);
			}
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

		[Fact]
		public async void GetTabItem_from_tab_returns_ok()
		{
			var mock = new Mock<ITabItemRepository>();
			var guid = Guid.NewGuid();
			var tabItems = new List<TabItemSimpleDTO>
			{
				new TabItemSimpleDTO(),
				new TabItemSimpleDTO(),
			};

			var tab = new TabDTO { Id = guid, TabItems = tabItems };

			mock.Setup(m => m.FindFrom(tab))
				.ReturnsAsync(tabItems);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Get(tab);
				Assert.IsType<OkResult>(response);
			}
		}

		public async void GetTabItem_from_tab_returns_notfound()
		{
			var mock = new Mock<ITabItemRepository>();
			var guid = Guid.NewGuid();

			var tab = new TabDTO { Id = guid, };

			mock.Setup(m => m.FindFrom(tab))
				.ReturnsAsync(new List<TabItemSimpleDTO> { });

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Get(tab);
				Assert.IsType<NotFoundResult>(response);
			}
		}

		[Fact]
		public async void Post_given_tab_returns_createdataction()
		{
			var mock = new Mock<ITabItemRepository>();
			mock.Setup(m => m.Create(It.IsAny<TabItemCreateDTO>()))
				.ReturnsAsync(Guid.NewGuid());

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Post(new TabItemCreateDTO { Amount = 120 });
				Assert.IsType<CreatedAtActionResult>(response);
			}
		}

		[Fact]
		public async void Put_given_tabItem_returns_nocontent()
		{

		}

		[Fact]
		public async void Put_given_no_tabItem_returns_badrequest()
		{

		}
    }
}
