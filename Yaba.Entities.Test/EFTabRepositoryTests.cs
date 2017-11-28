﻿using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaba.Common;
using Yaba.Common.DTOs.TabDTOs;
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
            using (var repo = new EFTabRepository(context))
            {
                var result = await repo.FindTab(Guid.NewGuid());
                Assert.Null(result);
            }
        }

        [Fact (DisplayName = "FindAllTabs finds all tabs")]
        public async void FindAllTabs_finds_all_tabs()
        {
            var context = Util.GetNewContext(nameof(FindAllTabs_finds_all_tabs));

            var tab1 = new Tab { Balance = 120, State = State.Active };
            var tab2 = new Tab { Balance = 240, State = State.Active };
            var tab3 = new Tab { Balance = 480, State = State.Active };

            using (var repo = new EFTabRepository(context))
            {
                context.Tabs.AddRange(tab1, tab2, tab3);
                await context.SaveChangesAsync();

                var tabs = await repo.FindAllTabs();
                Assert.Equal(3, tabs.Count);
            }
        }

        [Fact (DisplayName = "CreateTab creates a tab")]
        public async void CreateTab_Creates_Tab()
        {
            var entity = default(Tab);
            var mock = new Mock<IYabaDBContext>();

            using (var repo = new EFTabRepository(mock.Object))
            {
                mock.Setup(m => m.Tabs.Add(It.IsAny<Tab>()))
                .Callback<Tab>(t => entity = t);

                var tabToAdd = new TabDTO { Balance = 120, State = State.Active };
                await repo.CreateTab(tabToAdd);
            }

            Assert.Equal(120, entity.Balance);
            Assert.Equal(State.Active, entity.State);
        }
    }
}
