using System;
using System.Linq;
using Xunit;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Common.Tab.TabItemDTOs;
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

		[Fact]
		public async void FindFromTabItem_Given_Valid_Tab_Id_With_Invalid_TabItemCategory_Returns_null()
		{
			var context = Util.GetNewContext(nameof(FindFromTabItem_Given_Valid_Tab_Id_With_Invalid_TabItemCategory_Returns_null));

			var tabItem = new TabItem();

			context.TabItems.Add(tabItem);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = await repo.FindFromTabItemId(tabItem.Id);
				Assert.Null(dto);
			}
		}

		[Fact]
		public async void Create_Creates_TabItemCategory()
		{
			var context = Util.GetNewContext(nameof(Create_Creates_TabItemCategory));
			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var category = new TabItemCategoryCreateDTO { Name = "Food" };
				var guid = await repo.Create(category);
				var entity = context.TabItemCategories.SingleOrDefault(c => c.Id == guid);
				Assert.NotNull(entity);
				Assert.Equal("Food", entity.Name);
			}
		}

		[Fact]
		public async void Update_Given_Valid_TabItemCategory_Returns_True()
		{
			var context = Util.GetNewContext(nameof(Update_Given_Valid_TabItemCategory_Returns_True));
			var entity = new TabItemCategory { Name = "fuud" };

			context.TabItemCategories.Add(entity);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = new TabItemCategoryDTO
				{
					Name = "Food",
					Id = entity.Id,
				};

				var updated = await repo.Update(dto);
				Assert.True(updated);
				Assert.NotNull(entity.Name);
				Assert.Equal("Food", entity.Name);
			}
		}

		[Fact]
		public async void Update_Given_Non_Existent_TabItemCategory_Returns_False()
		{
			var context = Util.GetNewContext(nameof(Update_Given_Non_Existent_TabItemCategory_Returns_False));

			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = new TabItemCategoryDTO
				{
					Name = "Food",
					Id = Guid.NewGuid(),
				};

				var updated = await repo.Update(dto);
				Assert.False(updated);
			}
		}

		[Fact]
		public async void Delete_Given_Existing_TabItemCategory_Returns_True()
		{
			var context = Util.GetNewContext(nameof(Delete_Given_Existing_TabItemCategory_Returns_True));

			var entity = new TabItemCategory { Name = "Food" };
			context.TabItemCategories.Add(entity);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = new TabItemCategoryDTO
				{
					Id = entity.Id,
					Name = entity.Name,
				};
				var deleted = await repo.Delete(dto.Id);
				entity = context.TabItemCategories.SingleOrDefault(c => c.Id == dto.Id); // Attempt to retrieve entity again
				Assert.True(deleted);
				Assert.Null(entity);
			}
		}

		[Fact]
		public async void Delete_Given_Non_Existing_TabItemCategory_Returns_False()
		{
			var context = Util.GetNewContext(nameof(Delete_Given_Non_Existing_TabItemCategory_Returns_False));

			using (var repo = new EFTabItemCategoryRepository(context))
			{
				var dto = new TabItemCategoryDTO { Id = Guid.NewGuid() };
				var deleted = await repo.Delete(dto.Id);
				Assert.False(deleted);
			}
		}
	}
}
