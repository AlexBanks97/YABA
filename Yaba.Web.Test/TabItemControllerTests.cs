﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Yaba.Common;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
	public class TabItemControllerTests
    {
		[Fact]
		public async void GetTabItem_given_existing_id_returns_ok()
		{
			var mock = new Mock<IItemRepository>();

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
			var mock = new Mock<IItemRepository>();

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
			var mock = new Mock<IItemRepository>();

			mock.Setup(m => m.Find(guid))
				.ReturnsAsync(default(TabItemSimpleDTO));

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Get(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}

		[Fact]
		public async void GetAll_Given_Valid_TabId_Returns_Ok_With_TabItems()
		{
			var tabItems = new List<TabItemSimpleDTO>
			{
				new TabItemSimpleDTO(),
				new TabItemSimpleDTO(),
			};
			var mock = new Mock<IItemRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(t => t.FindFromTab(guid))
				.ReturnsAsync(tabItems);

			using (var ctrl = new TabItemController(mock.Object))
			{
				var response = await ctrl.GetAll(guid) as OkObjectResult;
				var result = response.Value as ICollection<TabItemSimpleDTO>;
				Assert.Equal(tabItems, result);
			}
		}

		public async void GetAll_Given_No_TabId_Returns_Forbidden()
		{
			using (var ctrl = new TabItemController(null))
			{
				var respone = await ctrl.GetAll(null);
				Assert.IsType<ForbidResult>(respone);

			};
		}

		[Fact]
		public async void Post_given_tab_returns_createdataction()
		{
			var mock = new Mock<IItemRepository>();
			mock.Setup(m => m.Create(It.IsAny<TabItemCreateDTO>()))
				.ReturnsAsync(Guid.NewGuid());

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Post(new TabItemCreateDTO { Amount = 120 });
				Assert.IsType<CreatedAtActionResult>(response);
			}
		}

		[Fact(Skip = "Fix this!")]
		public async void Post_given_malformed_tabItem_returns_BadRequest()
		{
			var mock = new Mock<IItemRepository>();
			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Post(new TabItemCreateDTO());
				Assert.IsType<BadRequestResult>(response);
			}
		}

		[Fact]
		public async void Put_given_tabItem_returns_nocontent()
		{
			var mock = new Mock<IItemRepository>();
			mock.Setup(m => m.Update(It.IsAny<TabItemSimpleDTO>()))
				.ReturnsAsync(true);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Put(Guid.NewGuid(), new TabItemSimpleDTO { Description = "Who can relate, whoa!" });
				Assert.IsType<NoContentResult>(response);
			}
		}

		[Fact]
		public async void Delete_given_existing_id_returns_NoContent()
		{
			var mock = new Mock<IItemRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(m => m.Delete(guid))
				.ReturnsAsync(true);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Delete(guid);
				Assert.IsType<NoContentResult>(response);
			}
		}

		[Fact]
		public async void Delete_given_nonexisting_id_returns_NotFound()
		{
			var mock = new Mock<IItemRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(r => r.Delete(guid))
				.ReturnsAsync(false);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Delete(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}
	}
}
