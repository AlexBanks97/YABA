using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Yaba.Common.Tab.DTO.Item;
using Yaba.Entities.Tab;
using Yaba.Entities.Tab.Repository;

namespace Yaba.Entities.Test
{
	public class EFTabItemRepositoryTests
	{
		[Fact]
		public async void Find_Given_Existing_Id_And_Amount_Returns_TabItem()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_And_Amount_Returns_TabItem));

			var tabitem = new ItemEntity { Amount = 42 };

			context.TabItems.Add(tabitem);
			await context.SaveChangesAsync();

			using (var repo = new EFItemRepository(context))
			{
				var actual = await repo.Find(tabitem.Id);
				Assert.Equal(42, actual.Amount);
			}
		}

		[Fact]
		public async void Find_Given_Nonexistent_Id_Returns_Null()
		{
			using (var repo = new EFItemRepository(Util.GetNewContext(nameof(Find_Given_Nonexistent_Id_Returns_Null))))
			{
				var actual = await repo.Find(Guid.NewGuid());
				Assert.Null(actual);
			}
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(5)]
		[InlineData(10)]
		public async void FindFromTab_Given_Tab_Returns_TabItems(int count)
		{
			var context = Util.GetNewContext(nameof(FindFromTab_Given_Tab_Returns_TabItems));

			var tabItems = new List<ItemEntity>();
			for (var i = 0; i < count; i++)
			{
				tabItems.Add(new ItemEntity());
			}

			var tab = new Tab.TabEntity { TabItems = tabItems };
			context.Tabs.Add(tab);
			await context.SaveChangesAsync();

			using (var repo = new EFItemRepository(context))
			{
				var tabItemsDTO = await repo.FindFromTab(tab.Id);
				Assert.Equal(count, tabItemsDTO.ToList().Count);
			}
		}

		[Fact]
		public async void FindFromTab_Given_TabId_Returns_TabItems_With_Same_Values()
		{
			var context = Util.GetNewContext(nameof(FindFromTab_Given_TabId_Returns_TabItems_With_Same_Values));

			var tabItems = new List<ItemEntity>();
			tabItems.Add(new ItemEntity
			{
				Amount = 42,
				Description = "Pizza last week",
			});

			var tab = new Tab.TabEntity { TabItems = tabItems };

			context.Tabs.Add(tab);
			await context.SaveChangesAsync();

			using (var repo = new EFItemRepository(context))
			{
				var tabItemsDTO = await repo.FindFromTab(tab.Id);
				var tabItem = tabItemsDTO.First();
				Assert.Equal(1, tabItemsDTO.Count());
				Assert.Equal(42, tabItem.Amount);
				Assert.Equal("Pizza last week", tabItem.Description);
			}
		}

		[Fact]
		public async void Update_Given_Tab_Returns_True()
		{
			var context = Util.GetNewContext(nameof(Update_Given_Tab_Returns_True));

			var tabItem = new ItemEntity { Amount = 100 };

			context.TabItems.Add(tabItem);
			await context.SaveChangesAsync();

			using (var repo = new EFItemRepository(context))
			{
				var dto = new TabItemSimpleDTO
				{
					Id = tabItem.Id,
					Amount = 200,
				};

				var updated = await repo.Update(dto);
				Assert.True(updated);
				Assert.Equal(200, tabItem.Amount);
			}
		}

		[Fact]
		public async void Update_Given_Non_Existing_Tab_Returns_False()
		{
			var context = Util.GetNewContext(nameof(Update_Given_Tab_Returns_True));
			using (var repo = new EFItemRepository(context))
			{
				var dto = new TabItemSimpleDTO
				{
					Id = Guid.NewGuid(),
					Amount = 200,
				};

				var updated = await repo.Update(dto);
				Assert.False(updated);
			}
		}

		[Fact]
		public async void Delete_Given_Existing_TabItem_Returns_True()
		{
			var context = Util.GetNewContext(nameof(Delete_Given_Existing_TabItem_Returns_True));

			var tabItem = new ItemEntity { Amount = 42 };
			context.Add(tabItem);
			await context.SaveChangesAsync();

			using (var repo = new EFItemRepository(context))
			{
				var deleted = await repo.Delete(tabItem.Id);
				Assert.True(deleted);
				Assert.Null(context.TabItems.SingleOrDefault(t => t.Id == tabItem.Id));
			}
		}

		[Fact]
		public async void Delete_Given_Non_Existing_TabItem_Returns_False()
		{
			var context = Util.GetNewContext(nameof(Delete_Given_Non_Existing_TabItem_Returns_False));
			using (var repo = new EFItemRepository(context))
			{
				var deleted = await repo.Delete(Guid.NewGuid());
				Assert.False(deleted);
			}
		}

		[Fact]
		public async void Create_Given_Valid_TabItem_Returns_Dto()
		{
			var context = Util.GetNewContext(nameof(Create_Given_Valid_TabItem_Returns_Dto));

			var tab = new Tab.TabEntity();
			context.Tabs.Add(tab);
			await context.SaveChangesAsync();

			using (var repo = new EFItemRepository(context))
			{
				var dto = new TabItemCreateDTO
				{
					Amount = 42,
					TabId = tab.Id,
				};
				var createdDto = await repo.Create(dto);
				var entity = context.TabItems.SingleOrDefault(t => t.Id == createdDto.Id);
				Assert.NotNull(entity);
			}
		}

		[Fact]
		public async void Create_Given_Invalid_TabItem_Returns_Null()
		{
			var context = Util.GetNewContext(nameof(Create_Given_Invalid_TabItem_Returns_Null));
			using (var repo = new EFItemRepository(context))
			{
				var createDto = new TabItemCreateDTO();
				var simpleDto = await repo.Create(createDto);
				Assert.Null(simpleDto);
			}
		}
	}
}
