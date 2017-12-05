using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Entry;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
	public class BudgetEntryControllerTests
    {
		[Fact]
		public async void get_Given_No_Id_returns_OK_plus_List_of_entries()
		{
			var mock = new Mock<IEntryRepository>();
			var entries = new List<EntryDto> { new EntryDto(), new EntryDto() };

			mock.Setup(m => m.Find())
				.ReturnsAsync(entries);

			using (var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Get() as OkObjectResult;
				Assert.Equal(entries, result.Value);
			}
		}

		[Fact]
		public async void get_Given_Id_Returns_ok_single_entry_with_correct_Id()
		{
			var entry = new EntryDetailsDto { Description = "" };

			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.Find(It.IsAny<Guid>())).ReturnsAsync(entry);

			using(var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Get(new Guid()) as OkObjectResult;
				Assert.Equal(entry, result.Value);
			}
		}

		[Fact]
		public async void get_given_invalid_id_returns_not_found()
		{
			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.Find(It.IsAny<Guid>())).ReturnsAsync(default(EntryDetailsDto));

			using(var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Get(new Guid());
				Assert.IsType<NotFoundResult>(result);
			}
		}

		[Fact]
		public async void post_given_valid_model_returns_Created_status_code()
		{
			var guid = Guid.NewGuid();
			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.CreateBudgetEntry(It.IsAny<EntryCreateDto>())).ReturnsAsync(guid);

			using(var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Post(new EntryCreateDto()) as CreatedAtActionResult;
				Assert.Equal(guid, result.RouteValues.Values.First());
				Assert.IsType<Guid>(result.RouteValues.Values.First());
			}
		}

		[Fact(Skip = "Model states not set up yet")]
		public async void post_given_invalid_model_returns_bad_request()
		{
			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.CreateBudgetEntry(It.IsAny<EntryCreateDto>())).ReturnsAsync(new Guid());

			using (var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Post(default(EntryCreateDto));
				Assert.IsType<BadRequestResult>(result);
			}
		}

		[Fact]
		public async void put_given_valid_Model_Returns_NoContent()
		{
			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.UpdateBudgetEntry(It.IsAny<EntryDto>())).ReturnsAsync(true);

			using(var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Put(new EntryDto());
				Assert.IsType<NoContentResult>(result);
			}
		}

		[Fact(Skip ="Model states not set up yet")]
		public async void Put_given_invalid_model_returns_bad_request()
		{
			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.UpdateBudgetEntry(It.IsAny<EntryDto>())).ReturnsAsync(false);

			using(var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Put(default(EntryDto));
				Assert.IsType<BadRequestResult>(result);
			}
		}

		[Fact]
		public async void Delete_given_valid_Id_returns_noContent()
		{
			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.DeleteBudgetEntry(It.IsAny<Guid>())).ReturnsAsync(true);

			using(var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Delete(new Guid());
				Assert.IsType<NoContentResult>(result);
			}
		}

		[Fact]
		public async void Delete_Given_non_existing_id_returns_no_content()
		{
			var mock = new Mock<IEntryRepository>();
			mock.Setup(m => m.DeleteBudgetEntry(It.IsAny<Guid>())).ReturnsAsync(false);

			using (var ctrl = new BudgetEntryController(mock.Object))
			{
				var result = await ctrl.Delete(new Guid());
				Assert.IsType<NotFoundResult>(result);
			}
		}
    }
}
