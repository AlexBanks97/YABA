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
				var result = ctrl.Get(guid);
				Assert.IsType<OkResult>(result);
			}
		}

		[Fact]
		public async void Get_Given_Non_Existing_Id_Returns_NotFound()
		{
			var mock = new Mock<ITabItemCategoryRepository>();
			using (var ctrl = new TabItemCategoriesController(mock.Object))
			{
				var result = ctrl.Get(Guid.NewGuid());
				Assert.IsType<NotFoundResult>(result);
			}
		}
	}
}
