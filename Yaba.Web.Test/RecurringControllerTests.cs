using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Recurring;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
	public class RecurringControllerTests
    {
		[Fact]
		public async void GetAll_Given_No_Params_Returns_All()
		{
			var mock = new Mock<IRecurringRepository>();

			var recurrings = new List<RecurringSimpleDto>
			{
				new RecurringSimpleDto(),
				new RecurringSimpleDto(),
				new RecurringSimpleDto()
			};

			mock.Setup(f => f.FindAllBudgetRecurrings()).ReturnsAsync(recurrings);

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Get() as OkObjectResult;
				Assert.Equal(recurrings, response.Value);
			}
		}

		[Fact]
		public async void Get_Given_Id_Returns_Income_With_Id()
		{
			var mock = new Mock<IRecurringRepository>();

			var guid = Guid.NewGuid();
			var recurring = new RecurringSimpleDto();

			mock.Setup(m => m.FindBudgetRecurring(guid)).ReturnsAsync(recurring);

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.Equal(recurring, response.Value);
			}
		}

		[Fact]
		public async void Get_Given_Exisiting_Id_With_Content_Returns_Ok()
		{
			var mock = new Mock<IRecurringRepository>();

			var guid = Guid.NewGuid();
			var dto = new RecurringSimpleDto { Name = "Paycheck " };
			mock.Setup(m => m.FindBudgetRecurring(guid)).ReturnsAsync(dto);

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.Equal(dto, response.Value);
			}
		}

		[Fact]
		public async void Get_Given_Nonexisting_Id_Returns_Not_Found()
		{
			var mock = new Mock<IRecurringRepository>();
			var guid = Guid.NewGuid();

			mock.Setup(m => m.FindBudgetRecurring(It.IsAny<Guid>()))
				.ReturnsAsync(default(RecurringSimpleDto));

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Get(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}

		[Fact]
		public async void Post_Creates_New_DTO_With_Content_Returns_Created_At_Action()
		{
			var mock = new Mock<IRecurringRepository>();

			mock.Setup(m => m.CreateBudgetRecurring(It.IsAny<RecurringCreateDto>()))
				.ReturnsAsync(Guid.NewGuid);

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Post(new RecurringCreateDto { Name = "Paycheck" });
				Assert.IsType<CreatedAtActionResult>(response);
			}
		}

		[Fact(Skip = "Fix this")]
		public async void Post_Given_Bad_Model_State_Returns_Bad_Request()
		{
			var mock = new Mock<IRecurringRepository>();
			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Post(new RecurringCreateDto());
				Assert.IsType<BadRequestResult>(response);
			}
		}

		[Fact]
		public async void Put_Given_Recurring_Returns_No_Content()
		{
			var mock = new Mock<IRecurringRepository>();
			mock.Setup(m => m.UpdateBudgetRecurring(It.IsAny<RecurringUpdateDto>()))
				.ReturnsAsync(true);

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Put(Guid.NewGuid(), new RecurringUpdateDto { Name = "New Year, new me" });
				Assert.IsType<NoContentResult>(response);
			}
		}

		[Fact]
		public async void Delete_Given_Id_Returns_NoContent()
		{
			var mock = new Mock<IRecurringRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(m => m.DeleteBudgetRecurring(guid))
				.ReturnsAsync(true);

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Delete(guid);
				Assert.IsType<NoContentResult>(response);
			}
		}

		[Fact]
		public async void Delete_Given_Nonexisting_Id_Returns_NotFound()
		{
			var mock = new Mock<IRecurringRepository>();
			var guid = Guid.NewGuid();

			mock.Setup(m => m.DeleteBudgetRecurring(guid)).ReturnsAsync(false);

			using (var controller = new RecurringController(mock.Object))
			{
				var response = await controller.Delete(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}
    }
}
