using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities.Test
{
	public class EFTabItemRepositoryTests
	{
		[Fact]
		public async void Find_Given_Existing_Id_And_Amount_Returns_TabItem()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_And_Amount_Returns_TabItem));

			var tabitem = new TabItem { Amount = 42 };

			context.TabItems.Add(tabitem);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemRepository(context))
			{
				var actual = await repo.Find(tabitem.Id);
				Assert.Equal(42, actual.Amount);
			}
		}

		[Fact]
		public async void Find_Given_Existing_Id_With_Category_Returns_TabItem_With_Category()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_With_Category_Returns_TabItem_With_Category));

			var tabitem = new TabItem { Category = new TabCategory { Name = "Food" } };

			context.TabItems.Add(tabitem);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemRepository(context))
			{
				var actual = await repo.Find(tabitem.Id);
				Assert.NotNull(actual.Category);
				Assert.Equal("Food", actual.Category.Name);
			}
		}

		[Fact]
		public async void Find_Given_Existing_Id_Without_Category_Returns_TabItem_Without_Category()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_With_Category_Returns_TabItem_With_Category));

			var tabitem = new TabItem();

			context.TabItems.Add(tabitem);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemRepository(context))
			{
				var actual = await repo.Find(tabitem.Id);
				Assert.Null(actual.Category);
			}
		}


		[Fact]
		public async void Find_Given_Nonexistent_Id_Returns_Null()
		{
			using (var repo = new EFTabItemRepository(Util.GetNewContext(nameof(Find_Given_Nonexistent_Id_Returns_Null))))
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
		public async void FindFrom_Given_Tab_Returns_TabItems(int count)
		{
			var context = Util.GetNewContext(nameof(FindFrom_Given_Tab_Returns_TabItems));

			var tabItems = new List<TabItem>();
			for(var i = 0; i < count; i++)
			{
				tabItems.Add(new TabItem());
			}

			var tab = new Tab { TabItems = tabItems };
			context.Tabs.Add(tab);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemRepository(context))
			{
				var tabItemsDTO = await repo.FindFrom(tab.ToDTO());
				Assert.Equal(count, tabItemsDTO.ToList().Count);
			}
		}

		[Fact]
		public async void FindFrom_Given_Tab_Returns_TabItems_With_Same_Values()
		{
			var context = Util.GetNewContext(nameof(FindFrom_Given_Tab_Returns_TabItems_With_Same_Values));

			var tabItems = new List<TabItem>();
			tabItems.Add(new TabItem
			{
				Amount = 42,
				Description = "Pizza last week",
				Category = new TabCategory { Name = "Food" }
			});

			var tab = new Tab { TabItems = tabItems };

			context.Tabs.Add(tab);
			await context.SaveChangesAsync();

			using (var repo = new EFTabItemRepository(context))
			{
				var tabItemsDTO = await repo.FindFrom(tab.ToDTO());
				var tabItem = tabItemsDTO.First();
				Assert.Equal(1, tabItemsDTO.Count());
				Assert.Equal(42, tabItem.Amount);
				Assert.Equal("Pizza last week", tabItem.Description);
				Assert.Equal("Food", tabItem.Category.Name);

			}
		}
	}
}
