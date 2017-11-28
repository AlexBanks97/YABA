﻿using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaba.Common;
using Yaba.Entities.TabEntitites;

namespace Yaba.Entities.Test
{
    public class EFTabRepositoryTests
    {
        [Fact]
        public void Using_repository_disposes_of_context()
        {
            var mock = new Mock<IYabaDBContext>();
            using (var repo = new EFBudgetRepository(mock.Object));
            mock.Verify(m => m.Dispose(), Times.Once);
        }

        [Fact]
        public async void FindTab_Given_Guid_Returns_Tab()
        {
            var context = Util.GetNewContext(nameof(FindTab_Given_Guid_Returns_Tab));

            var expected = new Tab { Balance = 42, State = State.Active };
            
            using (var repo = new EFTabRepository(context))
            {
                context.Tabs.Add(expected);
                context.SaveChanges();
                
                var result = await repo.FindTab(expected.Id);
                Assert.Equal(expected.Balance, result.Balance);
                Assert.Equal(expected.State, result.State);
            }
        }

        [Fact]
        public async void FindTab_Given_nonexisting_guid_returns_null()
        {
            var context = Util.GetNewContext(nameof(FindTab_Given_nonexisting_guid_returns_null));
            using(var repo = new EFTabRepository(context))
            {
                var result = await repo.FindTab(Guid.NewGuid());
                Assert.Null(result);
            }
        }
    }
}
