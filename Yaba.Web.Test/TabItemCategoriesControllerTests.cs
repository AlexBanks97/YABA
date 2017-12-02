using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Common.Tab;
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
		public async void Get_Given_Valid_TabItemCategoryDTO_Returns_Ok()
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
		public async void Get_Given_Invalid_TabItemCategoryDTO_Returns_NotFound()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = await ctrl.Get(new TabItemSimpleDTO());
				Assert.IsType<NotFoundResult>(result);
			}
		}
	}
}
