
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Yaba.Common;
using Yaba.Common.DTOs.BudgetDTOs;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
    public class BudgetsControllerTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(10)]
        public async void Get_given_no_params_returns_list_of_budgets(int count)
        {
            var budgets = new List<BudgetSimpleDTO>();
            for (var i = 0; i < count; i++)
                budgets.Add(new BudgetSimpleDTO());
            
            var mock = new Mock<IBudgetRepository>();
            mock.Setup(m => m.FindAllBudgets())
                .ReturnsAsync(budgets);

            using (var controller = new BudgetsController(mock.Object))
            {
                var response = await controller.Get() as OkObjectResult;
                var value = response.Value as IEnumerable<BudgetDTO>;
                Assert.Equal(count, value.Count());
            }
        }

        [Fact(DisplayName = "Get Budget given GUID returns OK")]
        public async void Get_given_existing_guid_returns_OK_with_budget()
        {
            var budget = new BudgetDetailsDto {Name = "Budget"};

            var guid = Guid.NewGuid();
            
            var mock = new Mock<IBudgetRepository>();
            mock.Setup(m => m.FindBudget(guid))
                .ReturnsAsync(budget);

            using (var controller = new BudgetsController(mock.Object))
            {
                var actual = await controller.Get(guid) as OkObjectResult;
                Assert.Equal(budget, actual.Value);
            }
        }

        [Fact(DisplayName = "Get Budget given non-existing GUID returns NotFound")]
        public async void Get_given_nonexisting_guid_returns_NotFound()
        {
            var guid = Guid.NewGuid();
            var mock = new Mock<IBudgetRepository>();
            mock.Setup(m => m.FindBudget(guid))
                .ReturnsAsync(default(BudgetDetailsDto));

            using (var controller = new BudgetsController(mock.Object))
            {
                var response = await controller.Get(guid);
                Assert.IsType<NotFoundResult>(response);
            }
        }
    }
}