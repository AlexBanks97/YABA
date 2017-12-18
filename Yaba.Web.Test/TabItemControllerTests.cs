using Microsoft.AspNetCore.Mvc;
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
		public async void Get_given_existing_id_returns_ok_with_correct_information()
		{
			var mock = new Mock<IItemRepository>();

			var guid = Guid.NewGuid();
			var tabItem = new TabItemSimpleDTO() { Amount = 42, Description = "Pizza friday" };

			mock.Setup(m => m.Find(guid)).ReturnsAsync(tabItem);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.IsType<OkObjectResult>(response);
				var result = response.Value as TabItemSimpleDTO;
				Assert.Equal(42, result.Amount);
				Assert.Equal("Pizza friday", result.Description);
			}
		}

		[Fact]
		public async void Get_given_nonexisting_id_returns_notfound()
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
				new TabItemSimpleDTO() { Amount = 42 },
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

		[Fact]
		public async void Post_given_tab_returns_Ok()
		{
			var mock = new Mock<IItemRepository>();
			var dto = new TabItemSimpleDTO {Amount = 10, Id = Guid.NewGuid()};
			mock.Setup(m => m.Create(It.IsAny<TabItemCreateDTO>()))
				.ReturnsAsync(dto);

			using (var controller = new TabItemController(mock.Object))
			{
				var response = await controller.Post(new TabItemCreateDTO { TabId = dto.Id, Amount = 120 });
				Assert.IsType<OkObjectResult>(response);
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
