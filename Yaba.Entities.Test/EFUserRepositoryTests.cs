using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Yaba.Common.User;
using Yaba.Common.User.DTO;
using Yaba.Entities.User;
using Yaba.Entities.User.Repository;

namespace Yaba.Entities.Test
{
    public class EFUserRepositoryTests
    {
		[Fact]
		public async void Find_Given_Existing_Id_Returns_User()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_Returns_User));

			var user = new UserEntity { Name = "Alexander" };
			context.Add(user);
			await context.SaveChangesAsync();

			using (var repo = new EFUserRepository(context))
			{
				var result = await repo.FindUser(user.Id);
				Assert.NotNull(result);
				Assert.Equal("Alexander", result.Name);
			}
		}

		[Fact]
		public async void Find_Given_Existing_Id_With_Friends_Returns_User_With_Friends()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_With_Friends_Returns_User_With_Friends));

			var friends = new List<UserEntity>
				{
					new UserEntity { Name = "Mikkel" },
					new UserEntity { Name = "Phillip" },
					new UserEntity { Name = "Philip" },
					new UserEntity { Name = "Christoffer"},
				};
			var user = new UserEntity
			{
				Name = "Alexander",
				Friends = friends,
			};

			context.Add(user);
			await context.SaveChangesAsync();

			using (var repo = new EFUserRepository(context))
			{
				var result = await repo.FindUser(user.Id);
				var friendsDto = friends.Select(u => new UserSimpleDto
				{
					Id = u.Id,
					Name = u.Name,
				}).ToList();

				Assert.NotNull(result);
				Assert.Equal("Alexander", result.Name);
				Assert.Equal(friendsDto, result.Friends);
			}
		}

		[Fact]
		public async void Find_Given_Non_Existing_Id_Returns_Null()
		{
			using (var repo = new EFUserRepository(Util.GetNewContext(nameof(Find_Given_Non_Existing_Id_Returns_Null))))
			{
				var result = await repo.FindUser(Guid.NewGuid());
				Assert.Null(result);
			}
		}
	}
}
