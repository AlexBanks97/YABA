using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Common.Tab;
using Yaba.Common.Tab.TabItemDTOs;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
	public class TabItemCategoriesControllerTests
	{
		[Fact]
		public async void Get_Given_Existing_Id_Returns_Ok()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(c => c.Find(guid))
				.ReturnsAsync(new TabItemCategoryDTO());

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Get(guid);
				Assert.IsType<OkObjectResult>(result);
			}
		}

		[Fact]
		public async void Get_Given_Non_Existing_Id_Returns_NotFound()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Get(Guid.NewGuid());
				Assert.IsType<NotFoundResult>(result);
			}
		}

		[Fact]
		public async void Get_Given_Existing_TabItemCategoryDTO_Returns_Ok()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			var tabItem = new TabItemSimpleDTO() { Id = Guid.NewGuid() };
			mock.Setup(c => c.FindFromTabItemId(tabItem.Id))
				.ReturnsAsync(new TabItemCategoryDTO());

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Get(tabItem);
				Assert.IsType<OkObjectResult>(result);
			}
		}

		[Fact]
		public async void Get_Given_Non_Existing_TabItemCategoryDTO_Returns_NotFound()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Get(new TabItemSimpleDTO());
				Assert.IsType<NotFoundResult>(result);
			}
		}

		[Fact(Skip = "It's a Post method :^)")]
		public async void Post_Given_Valid_TabItemCategory_Returns_CreatedAtActionResult()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			var tabItem = new TabItemCategoryCreateDTO { Name = "Food" };
			var guid = Guid.NewGuid();
			mock.Setup(c => c.Create(tabItem)).
				ReturnsAsync(guid);

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Post(tabItem) as CreatedAtActionResult;
				Assert.IsType<CreatedAtActionResult>(result);
				Assert.Equal(guid, result.RouteValues.Values.First());
			}
		}

		[Fact(Skip="It's a Post method :^)")]
		public async void Post_Given_Invalid_TabItemCategory_Returns_BadRequest()
		{
			var mock = new Mock<ITabItemCategoryRepository>();

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Post(new TabItemCategoryCreateDTO());
				Assert.IsType<BadRequestResult>(result);
			}
		}

		[Fact]
		public async void Put_Given_Existing_TabITemCategory_Returns_NoContent()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			var dto = new TabItemCategoryDTO();
			mock.Setup(c => c.Update(dto))
				.ReturnsAsync(true);

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Put(dto);
				Assert.IsType<NoContentResult>(result);
			}
		}

		[Fact]
		public async void Put_Given_Non_Existing_TabItemCategory_Returns_BadRequest()
		{
			var mock = new Mock<ITabItemCategoryRepository>();

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Put(new TabItemCategoryDTO());
				Assert.IsType<BadRequestResult>(result);
			}
		}

		[Fact]
		public async void Delete_Given_Existing_TabItemCategory_Returns_NoContent()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(c => c.Delete(guid))
				.ReturnsAsync(true);

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Delete(guid);
				Assert.IsType<NoContentResult>(result);
			}
		}

		[Fact]
		public async void Delete_Given_Non_Existing_TabItemCategory_Returns_BadRequest()
		{
			var mock = new Mock<ITabItemCategoryRepository>();

			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Delete(Guid.NewGuid());
				Assert.IsType<BadRequestResult>(result);
			}
		}
	}
}
