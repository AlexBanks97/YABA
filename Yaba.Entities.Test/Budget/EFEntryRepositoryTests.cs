using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Entry;
using Yaba.Entities.Budget;

namespace Yaba.Entities.Test.Budget
{
	public class EFEntryRepositoryTests
	{
		//Create BudgetEntry
		[Fact]
		public async void CreateBudgetEntry_Given_budgetEntry_dto_returns_the_id_of_the_entry()
		{
			var ctx = Util.GetNewContext(nameof(CreateBudgetEntry_Given_budgetEntry_dto_returns_the_id_of_the_entry));
			var cat = new CategoryEntity
			{
				Name = "1"
			};
			ctx.BudgetCategories.Add(cat);
			ctx.SaveChanges();

			var entry = new EntryCreateDto
			{
				Amount = 2,
				Description = "hey",
				Category = new CategoryDto { Id = cat.Id,Name = cat.Name}
			};

			EntryEntity actual = null;
			Guid id = new Guid();
			using(var repo = new EFEntryRepository(ctx))
			{
				id = await repo.CreateBudgetEntry(entry);
				actual = ctx.BudgetEntries.Find(id);
			}
			Assert.NotNull(id);
			Assert.Equal(actual.Description, entry.Description);
			Assert.Equal(actual.Amount, entry.Amount);
		}


		//Update BudgetEntry
		[Fact]
		public async void UpdateBudgetEntry_Given_existing_budgetEntry_Updates_and_returns_true()
		{
			var ctx = Util.GetNewContext(nameof(UpdateBudgetEntry_Given_existing_budgetEntry_Updates_and_returns_true));
			var entry = new EntryEntity { Description = ".", Amount = 1 };
			ctx.BudgetEntries.Add(entry);
			ctx.SaveChanges();

			var result = false;
			EntryEntity actual = null;
			using(var repo = new EFEntryRepository(ctx))
			{
				result = await repo.UpdateBudgetEntry(new EntryDto { Id = entry.Id, Description = "new Desc." });

				actual = ctx.BudgetEntries.Find(entry.Id);
			}

			Assert.True(result);
			Assert.Equal("new Desc.", actual.Description);
		}

		[Fact]
		public async void UpdateBudgetEntry_Given_nonexisting_budgetEntry_returns_false()
		{
			var ctx = Util.GetNewContext(nameof(UpdateBudgetEntry_Given_nonexisting_budgetEntry_returns_false));

			var result = true;

			using(var repo = new EFEntryRepository(ctx))
			{
				result = await repo.UpdateBudgetEntry(new EntryDto());
			}

			Assert.False(result);
		}


		//Find all budgetEntries
		[Fact]
		public async void Find_Returns_All_BudgetEntries()
		{
			var ctx = Util.GetNewContext(nameof(Find_Returns_All_BudgetEntries));
			var cat1 = new CategoryEntity { Name = "Food" };
			var cat2 = new CategoryEntity { Name = "Drinks" };
			ctx.BudgetCategories.AddRange(cat1, cat2);

			var entry1 = new EntryEntity { CategoryEntity = cat1 };
			var entry2 = new EntryEntity { CategoryEntity = cat2 };
			ctx.BudgetEntries.AddRange(entry1, entry2);
			ctx.SaveChanges();

			ICollection<EntryDto> result = null;
			using (var repo = new EFEntryRepository(ctx))
			{
				result = await repo.Find();
			}
			Assert.Equal(2, result.Count);
		}

		//FindBudgetFromCategory
		[Fact]
		public async void FindFromBudgetCategory_returns_only_the_Entries_of_given_category()
		{
			var ctx = Util.GetNewContext(nameof(FindFromBudgetCategory_returns_only_the_Entries_of_given_category));
			var cat1 = new CategoryEntity { Name = "Food" };
			var cat2 = new CategoryEntity { Name = "Drinks" };
			ctx.BudgetCategories.AddRange(cat1, cat2);

			var entry1 = new EntryEntity { CategoryEntity = cat1 };
			var entry2 = new EntryEntity { CategoryEntity = cat2 };
			ctx.BudgetEntries.AddRange(entry1, entry2);
			ctx.SaveChanges();

			ICollection<EntryDto> result = null;
			using(var repo = new EFEntryRepository(ctx))
			{
				result = await repo.FindFromBudgetCategory(cat1.Id);
			}
			Assert.Equal(1, result.Count);
			Assert.Equal(entry1.Id, result.First().Id);
		}

		[Fact]
		public async void FindFromBudgetCategory_returns_null_given_non_existing_id()
		{
			var ctx = Util.GetNewContext(nameof(FindFromBudgetCategory_returns_null_given_non_existing_id));
			ICollection<EntryDto> result = new List<EntryDto>();

			using (var repo = new EFEntryRepository(ctx))
			{
				result = await repo.FindFromBudgetCategory(new Guid());
			}

			Assert.Equal(null, result);
		}


		//Find Given ID
		[Fact]
		public async void Find_BudgetEntry_Returns_Budget_entry_Given_correct_id()
		{
			//Arrange
			var ctx = Util.GetNewContext(nameof(Find_BudgetEntry_Returns_Budget_entry_Given_correct_id));
			var cat = new CategoryEntity { Name ="hej" };
			ctx.BudgetCategories.Add(cat);

			var entry = new EntryEntity { Amount = 2, Description = "hey", CategoryEntity = cat};
			ctx.BudgetEntries.Add(entry);
			ctx.SaveChanges();
			EntryDetailsDto result = null;
			//Act
			using (var repo = new EFEntryRepository(ctx))
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
			var entry = new EntryEntity { Amount = 2, Description = "hey"};
			EntryDetailsDto result = null;
			//Act
			using (var repo = new EFEntryRepository(ctx))
			{
				result = await repo.Find(entry.Id);
			}
			//Assert
			Assert.Null(result);
		}


		//Delete
		[Fact]
		public async void Delete_Budget_removes_budget_entry_and_returns_true()
		{
			var ctx = Util.GetNewContext(nameof(Delete_Budget_removes_budget_entry_and_returns_true));
			var entry = new EntryEntity();
			ctx.BudgetEntries.Add(entry);// { Amount = 1, Description = "hey" }
			ctx.BudgetEntries.Add(new EntryEntity());
			ctx.SaveChanges();

			bool actual = false;
			var numberOfEntries = 0;
			using (var repo = new EFEntryRepository(ctx))
			{
				actual = await repo.DeleteBudgetEntry(entry.Id);
				numberOfEntries = ctx.BudgetEntries.Count();
			}
			Assert.True(actual);
			Assert.Equal(1, numberOfEntries);
		}

		[Fact]
		public async void Delete_Budget_removes_returns_false_given_none_existing_entry()
		{
			var ctx = Util.GetNewContext(nameof(Delete_Budget_removes_returns_false_given_none_existing_entry));
			bool actual = true;
			using (var repo = new EFEntryRepository(ctx))
			{
				actual = await repo.DeleteBudgetEntry(new Guid());
			}
			Assert.False(actual);
		}
	}
}
