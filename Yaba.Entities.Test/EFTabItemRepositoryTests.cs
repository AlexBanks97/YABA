using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities.Test
{
    public class EFTabItemRepositoryTests
    {
        [Fact]
        public async void Find_Given_Existing_Id_Returns_TabItem()
        {
            var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_Returns_TabItem));

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
        public async void Find_Given_Nonexistent_Id_Returns_Null()
        {
            using (var repo = new EFTabItemRepository(Util.GetNewContext(nameof(Find_Given_Nonexistent_Id_Returns_Null))))
            {
                var actual = await repo.Find(Guid.NewGuid());
                Assert.Null(actual);
            }
        }
    }
}
