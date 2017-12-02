using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Income;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
    public class IncomeControllerTests
    {
		[Fact]
		public async void GetAll_Given_No_Params_Returns_All()
		{
			var mock = new Mock<IIncomeRepository>();

			var incomes = new List<IncomeSimpleDto>
			{
				new IncomeSimpleDto(),
				new IncomeSimpleDto(),
				new IncomeSimpleDto()
			};

			mock.Setup(f => f.FindAllBudgetIncomes()).ReturnsAsync(incomes);

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Get() as OkObjectResult;
				Assert.Equal(incomes, response.Value);
			}
		}

		[Fact]
		public async void Get_Given_Id_Returns_Income_With_Id()
		{
			var mock = new Mock<IIncomeRepository>();

			var guid = Guid.NewGuid();
			var income = new IncomeSimpleDto();

			mock.Setup(m => m.FindBudgetIncome(guid)).ReturnsAsync(income);

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.Equal(income, response.Value);
			}
		}

		[Fact]
		public async void Get_Given_Exisiting_Id_With_Content_Returns_Ok()
		{
			var mock = new Mock<IIncomeRepository>();

			var guid = Guid.NewGuid();
			var dto = new IncomeSimpleDto { Name = "Paycheck " };
			mock.Setup(m => m.FindBudgetIncome(guid)).ReturnsAsync(dto);

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.Equal(dto, response.Value);
			}
		}

		[Fact]
		public async void Get_Given_Nonexisting_Id_Returns_Not_Found()
		{
			var mock = new Mock<IIncomeRepository>();
			var guid = Guid.NewGuid();

			mock.Setup(m => m.FindBudgetIncome(It.IsAny<Guid>()))
				.ReturnsAsync(default(IncomeSimpleDto));

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Get(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}

		[Fact]
		public async void Post_Creates_New_DTO_With_Content_Returns_Created_At_Action()
		{
			var mock = new Mock<IIncomeRepository>();

			mock.Setup(m => m.CreateBudgetIncome(It.IsAny<IncomeCreateDto>()))
				.ReturnsAsync(Guid.NewGuid);

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Post(new IncomeCreateDto { Name = "Paycheck" });
				Assert.IsType<CreatedAtActionResult>(response);
			}
		}

		[Fact(Skip = "Fix this")]
		public async void Post_Given_Bad_Model_State_Returns_Bad_Request()
		{
			var mock = new Mock<IIncomeRepository>();
			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Post(new IncomeCreateDto());
				Assert.IsType<BadRequestResult>(response);
			}
		}

		[Fact]
		public async void Put_Given_Income_Returns_No_Content()
		{
			var mock = new Mock<IIncomeRepository>();
			mock.Setup(m => m.UpdateBudgetIncome(It.IsAny<IncomeUpdateDto>()))
				.ReturnsAsync(true);

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Put(Guid.NewGuid(), new IncomeUpdateDto { Name = "New Year, new me" });
				Assert.IsType<NoContentResult>(response);
			}
		}

		[Fact]
		public async void Delete_Given_Id_Returns_NoContent()
		{
			var mock = new Mock<IIncomeRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(m => m.DeleteBudgetIncome(guid))
				.ReturnsAsync(true);

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Delete(guid);
				Assert.IsType<NoContentResult>(response);
			}
		}

		[Fact]
		public async void Delete_Given_Nonexisting_Id_Returns_NotFound()
		{
			var mock = new Mock<IIncomeRepository>();
			var guid = Guid.NewGuid();

			mock.Setup(m => m.DeleteBudgetIncome(guid)).ReturnsAsync(false);

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Delete(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}
    }
}
