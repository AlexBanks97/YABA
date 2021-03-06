﻿using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Yaba.Common;
using Yaba.Common.Tab.DTO;
using Yaba.Common.User;
using Yaba.Entities.Budget;
using Yaba.Entities.Budget.Repository;
using Yaba.Entities.Tab;
using Yaba.Entities.Tab.Repository;

namespace Yaba.Entities.Test
{
	public class EFTabRepositoryTests
	{
		[Fact(DisplayName = "Using respository disposes context - Tab")]
		public void Using_repository_disposes_of_context()
		{
			var mock = new Mock<IYabaDBContext>();
			using (var repo = new EFBudgetRepository(mock.Object));
			mock.Verify(m => m.Dispose(), Times.Once);
		}

		[Fact(DisplayName = "FindTab given existing GUID returns a tab")]
		public async void FindTab_Given_Guid_Returns_Tab()
		{
			var context = Util.GetNewContext(nameof(FindTab_Given_Guid_Returns_Tab));

			var expected = new Tab.TabEntity { Balance = 42, State = State.Active };

			using (var repo = new EFTabRepository(context))
			{
				context.Tabs.Add(expected);
				context.SaveChanges();

				var result = await repo.FindTab(expected.Id);
				Assert.Equal(expected.Balance, result.Balance);
				Assert.Equal(expected.State, result.State);
			}
		}

		[Fact(DisplayName = "FindTab given non-existing GUID returns null")]
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

			var tab1 = new Tab.TabEntity { Balance = 120, State = State.Active };
			var tab2 = new Tab.TabEntity { Balance = 240, State = State.Active };
			var tab3 = new Tab.TabEntity { Balance = 480, State = State.Active };

			using (var repo = new EFTabRepository(context))
			{
				context.Tabs.AddRange(tab1, tab2, tab3);
				await context.SaveChangesAsync();

				var tabs = await repo.FindAllTabs();
				Assert.Equal(3, tabs.Count);
			}
		}

		[Fact]
		public async void FindWithUser_returns_all_tabs_with_user()
		{
			var ctx = Util.GetNewContext(nameof(FindWithUser_returns_all_tabs_with_user));
			var u1 = new UserEntity { Name = "one" };
			var u2 = new UserEntity { Name = "two" };
			var u3 = new UserEntity { Name = "three" };

			var t1 = new TabEntity {UserOne = u1, UserTwo = u2};
			var t2 = new TabEntity {UserOne = u2, UserTwo = u1};
			var t3 = new TabEntity {UserOne = u2, UserTwo = u3};

			ctx.Users.AddRange(u1, u2, u3);
			ctx.Tabs.AddRange(t1, t2, t3);
			ctx.SaveChanges();

			using (var repo = new EFTabRepository(ctx))
			{
				var tabs = await repo.FindWithUser(u1.Id);
				Assert.Equal(2, tabs.Count);
			}
		}

		[Fact (DisplayName = "CreateTab creates a tab", Skip = "Fix this some day")]
		public async void CreateTab_Creates_Tab()
		{
			var entity = default(Tab.TabEntity);
			var mock = new Mock<IYabaDBContext>();
			mock.Setup(m => m.Tabs.Add(It.IsAny<Tab.TabEntity>()))
				.Callback<Tab.TabEntity>(t => entity = t);
			using (var repo = new EFTabRepository(mock.Object))
			{
				var tabToAdd = new TabCreateDto { Balance = 120, State = State.Active };
				await repo.CreateTab(tabToAdd);
			}

			Assert.Equal(120, entity.Balance);
			Assert.Equal(State.Active, entity.State);
		}

		[Fact]
		public async void UpdateTab_Given_Existing_Tab_Returns_True()
		{
			var context = Util.GetNewContext(nameof(UpdateTab_Given_Existing_Tab_Returns_True));
			var tab = new Tab.TabEntity { Balance = 42, State = State.Active };

			context.Tabs.Add(tab);
			await context.SaveChangesAsync();

			using (var repo = new EFTabRepository(context))
			{
				var dto = new TabUpdateDto
				{
					Id = tab.Id,
					Balance = 100,
					State = State.Archived,
				};
				var updated = await repo.UpdateTab(dto);
				Assert.True(updated);
				Assert.Equal(100, tab.Balance);
				Assert.Equal(State.Archived, tab.State);
			}

		}

		[Fact]
		public async void UpdateTab_Given_nonexisting_Tab_Returns_False()
		{
			var context = Util.GetNewContext(nameof(UpdateTab_Given_nonexisting_Tab_Returns_False));
			using (var repo = new EFTabRepository(context))
			{
				var updated = await repo.UpdateTab(new TabUpdateDto { Id = Guid.NewGuid() });
				Assert.False(updated);

			}
		}

		[Fact]
		public async void Delete_Given_Existing_Tab_Returns_True()
		{
			var context = Util.GetNewContext(nameof(Delete_Given_Existing_Tab_Returns_True));

			var tab = new Tab.TabEntity
			{
				Balance = 42,
				State = State.Active
			};

			context.Tabs.Add(tab);
			await context.SaveChangesAsync();

			using (var repo = new EFTabRepository(context))
			{
				var deleted = await repo.Delete(tab.Id);
				Assert.True(deleted);
			}
		}

		[Fact]
		public async void Delete_Given_Non_Existing_Tab_Returns_False()
		{
			var context = Util.GetNewContext(nameof(Delete_Given_Existing_Tab_Returns_True));
			using (var repo = new EFTabRepository(context))
			{
				var deleted = await repo.Delete(Guid.NewGuid());
				Assert.False(deleted);
			}
		}
	}
}
