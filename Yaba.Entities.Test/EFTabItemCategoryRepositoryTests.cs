using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common.Tab.TabItemDTOs.Category;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities.Test
{
    public class EFTabItemCategoryRepositoryTests
    {
		[Fact]
		public async void Find_Given_Valid_Id_Returns_DTO_With_Same_Data()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Valid_Id_Returns_DTO_With_Same_Data));

			var entity = new TabItemCategory { Name = "Food" };

			context.TabItemCategories.Add(entity);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = await repo.Find(entity.Id);
				Assert.NotNull(dto);
				Assert.Equal("Food", dto.Name);
			}
		}

		[Fact]
		public async void Find_Given_Invalid_Id_Returns_Null()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Valid_Id_Returns_DTO_With_Same_Data));
			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = await repo.Find(Guid.NewGuid());
				Assert.Null(dto);
			}
		}

		[Fact]
		public async void FindFromTabItem_Given_Valid_Tab_Id_Returns_dto()
		{
			var context = Util.GetNewContext(nameof(FindFromTabItem_Given_Valid_Tab_Id_Returns_dto));

			var category = new TabItemCategory { Name = "Food" };
			var tabItem = new TabItem { Category = category };

			context.TabItemCategories.Add(category);
			context.TabItems.Add(tabItem);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = await repo.FindFromTabItemId(tabItem.Id);
				Assert.NotNull(dto);
				Assert.Equal("Food", dto.Name);
			}
		}

		[Fact]
		public async void FindFromTabItem_Given_Invalid_Tab_Id_Returns_null()
		{
			var context = Util.GetNewContext(nameof(FindFromTabItem_Given_Invalid_Tab_Id_Returns_null));
			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = await repo.FindFromTabItemId(Guid.NewGuid());
				Assert.Null(dto);
			}
		}
	}
}
