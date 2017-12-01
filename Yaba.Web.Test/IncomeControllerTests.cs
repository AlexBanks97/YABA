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
    class IncomeControllerTests
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

			mock.Setup(f => f.FindAllBudgetIncomes());

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

			var incomes = new List<IncomeSimpleDto>
			{
				new IncomeSimpleDto()
			};

			mock.Setup(m => m.FindBudgetIncome(guid));

			using (var controller = new IncomeController(mock.Object))
			{
				var response = await controller.Get(guid) as OkObjectResult;
				Assert.Equal(incomes, response.Value);
			}
		}

		[Fact]
		public async void Post_Creates_New_DTO_Returns_Ok()
		{

		}
    }
}
