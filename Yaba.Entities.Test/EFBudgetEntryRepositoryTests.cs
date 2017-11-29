using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities.Test
{
    public class EFBudgetEntryRepositoryTests
    {

        [Fact]
        public async void Find_BudgetEntry_Returns_Budget_entry_Given_correct_id()
        {
            //Arrange
            var ctx = Util.GetNewContext(nameof(Find_BudgetEntry_Returns_Budget_entry_Given_correct_id));
            var cat = new BudgetCategory { Name ="hej" };
            ctx.BudgetCategories.Add(cat);

            var entry = new BudgetEntry { Amount = 2, Description = "hey", BudgetCategory = cat};
            ctx.BudgetEntries.Add(entry);
            ctx.SaveChanges();
            Common.DTOs.BudgetEntry.BudgetEntryDetailsDto result = null;
            //Act
            using (var repo = new EFBudgetEntryRepository(ctx))
            {
                result = await repo.Find(entry.Id);
            }
            //Assert
            Assert.Equal(result.Id, entry.Id);
        }
        [Fact]
        public async void Find_BudgetEntry_Returns_Null_entry_Given_Non_Existing_Id()
        {
            //Arrange
            var ctx = Util.GetNewContext(nameof(Find_BudgetEntry_Returns_Null_entry_Given_Non_Existing_Id));
            var entry = new BudgetEntry { Amount = 2, Description = "hey"};
            Common.DTOs.BudgetEntry.BudgetEntryDetailsDto result = null;
            //Act
            using (var repo = new EFBudgetEntryRepository(ctx))
            {
                result = await repo.Find(entry.Id);
            }
            //Assert
            Assert.Null(result);
        }
    }
}
