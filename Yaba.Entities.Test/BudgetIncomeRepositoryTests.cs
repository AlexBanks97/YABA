using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common;

namespace Yaba.Entities.Test
{
    class BudgetIncomeRepositoryTests
    {
        [Fact(DisplayName = "Create budget income creates a budget income")]
        public void Create_Budget_Income_Creates_Budget_Income()
        {
            var entity = default(IBudgetIncomeRepository);
            var mock = new Mock<IYabaDBContext>();

            mock.Setup(m => m.BudgetIncomes.Add(It.IsAny<BudgetIn>()))
                .Callback<Budget>(t => entity = t);
        }
    }
}
