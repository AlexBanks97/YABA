using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Yaba.Common;
using Yaba.Common.DTOs.TabDTOs;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
    public class TabsControllerTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(10)]
        public async void Get_tabs(int count)
        {
            var tabs = new List<TabDTO>();
            for (var i = 0; i < count; i++)
            {
                tabs.Add(new TabDTO());
            }

            var mock = new Mock<ITabRepository>();
            mock.Setup(m => m.FindAllTabs())
                .ReturnsAsync(tabs);

            using (var controller = new TabsController(mock.Object))
            {
                var actual = await controller.Get();
                Assert.Equal(count, actual.Count());
            }

        }

        [Fact]
        public async void Get_given_existing_guid_returns_OK_with_tab()
        {
            var tab = new TabDTO { Balance = 42, State = State.Active };
            var guid = Guid.NewGuid();

            var mock = new Mock<ITabRepository>();
            mock.Setup(m => m.FindTab(guid))
                .ReturnsAsync(tab);

            using (var controller = new TabsController(mock.Object))
            {
                var actual = await controller.Get(guid) as OkObjectResult;
                Assert.Equal(tab, actual.Value);
            }
        }

        [Fact]
        public async void Get_given_nonexisting_guid_returns_NotFound()
        {
            var guid = Guid.NewGuid();
            var mock = new Mock<ITabRepository>();
            mock.Setup(m => m.FindTab(guid))
                .ReturnsAsync(default(TabDTO));

            using (var controller = new TabsController(mock.Object))
            {
                var response = await controller.Get(guid);
                Assert.IsType<NotFoundResult>(response);
            }
        }

    }
}
