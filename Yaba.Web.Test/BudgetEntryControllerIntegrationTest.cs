using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Entry;
using Yaba.Entities;
using Yaba.Entities.Budget.Repository;
using Yaba.Entities.Test;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
    public class BudgetEntryControllerIntegrationTest
    {
		[Fact]
		public async void Get_returns_all_entries()
	    {
			var ctx = Util.GetNewContext(nameof(Get_returns_all_entries));
		    DbInitializer.Initialize(ctx);
		    var expected = ctx.BudgetEntries.Select(b => new EntrySimpleDto
		    {
			    Id = b.Id,
				Amount =  b.Amount,
				Date = b.Date,
				Description = b.Description
		    }).ToList();
		    using (var ctrl = new BudgetEntryController(new EFEntryRepository(ctx)))
		    {
			    var result = await ctrl.Get() as OkObjectResult;
			    Assert.Equal(expected, result.Value);
		    }
		}

	    [Fact]
	    public async void Get_Given_Id_of_entry_returns_entry()
	    {
			var ctx = Util.GetNewContext(nameof(Get_returns_all_entries));
		    DbInitializer.Initialize(ctx);
		    var expected = ctx.BudgetEntries.Select(be => new EntryDetailsDto
		    {
				Id = be.Id,
				Amount = be.Amount,
				Date = be.Date,
				Description = be.Description,
				BudgetCategory = new CategorySimpleDto
				{
					Id = be.CategoryEntity.Id,
					Name = be.CategoryEntity.Name
				}
		    }).FirstOrDefault();
		    using (var ctrl = new BudgetEntryController(new EFEntryRepository(ctx)))
		    {
			    var result = await ctrl.Get(expected.Id) as OkObjectResult;
			    Assert.Equal(expected, result.Value);
		    }
		}


	    [Fact]
	    public async void post_returns_result_containing_id_of_created_entry()
	    {
			var ctx = Util.GetNewContext(nameof(Get_returns_all_entries));
		    DbInitializer.Initialize(ctx);
		    var CreateEntry = new EntryCreateDto
		    {
			    Amount = (decimal) 200,
			    Date = DateTime.Now.Date,
			    Description = "blah",
				Category = new CategoryDto()
		    };
		    using (var ctrl = new BudgetEntryController(new EFEntryRepository(ctx)))
		    {
			    var result = await ctrl.Post(CreateEntry) as CreatedAtActionResult;
			    var actual = ctx.BudgetEntries.Find(result.RouteValues.Values.First());
			    Assert.Equal(CreateEntry.Date, actual.Date);
			    Assert.Equal(CreateEntry.Amount, actual.Amount);
				Assert.Equal(CreateEntry.Description, actual.Description);
		    }
		}

	    [Fact]
	    public async void Put_returns_ok_and_updates_entry()
	    {
			var ctx = Util.GetNewContext(nameof(Get_returns_all_entries));
		    DbInitializer.Initialize(ctx);
		    var entryToUpdate = ctx.BudgetEntries.FirstOrDefault();
		    var updateEntry = new EntryDto
		    {
			    Id = entryToUpdate.Id,
			    Amount = entryToUpdate.Amount,
			    Description = "new Description",
			    Date = entryToUpdate.Date
		    };

		    using (var ctrl = new BudgetEntryController(new EFEntryRepository(ctx)))
		    {
				Assert.NotEqual(entryToUpdate.Description, updateEntry.Description);
			    var result = await ctrl.Put(updateEntry);
			    var updatedDto = ctx.BudgetEntries.Find(entryToUpdate.Id);
			    Assert.IsType<NoContentResult>(result);
			    Assert.Equal(updatedDto.Description, updateEntry.Description);
				Assert.Equal(updatedDto.Date, updateEntry.Date);
		    }
		}

	    [Fact]
	    public async void Delete_removes_entry()
	    {
		    var ctx = Util.GetNewContext(nameof(Get_returns_all_entries));
		    DbInitializer.Initialize(ctx);
		    var deleteEntry = ctx.BudgetEntries.FirstOrDefault();
		    using (var ctrl = new BudgetEntryController(new EFEntryRepository(ctx)))
		    {
			    var result = await ctrl.Delete(deleteEntry.Id);
			    var actual = ctx.BudgetEntries.Find(deleteEntry.Id);
			    Assert.IsType<NoContentResult>(result);
			    Assert.Null(actual);
		    }
	    }
    }
}
