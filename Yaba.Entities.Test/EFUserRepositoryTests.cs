using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Yaba.Common.User;
using Yaba.Common.User.DTO;
using Yaba.Entities.User.Repository;

namespace Yaba.Entities.Test
{
    public class EFUserRepositoryTests
    {
	    [Fact]
	    public async void Find_Given_Existing_Id_Returns_User()
	    {
		    var ctx = Util.GetNewContext(nameof(Find_Given_Existing_Id_Returns_User));
		    var user = new UserEntity {Name = "Alexander"};
		    ctx.Users.Add(user);
		    await ctx.SaveChangesAsync();

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var actual = await repo.Find(user.Id);
				Assert.NotNull(actual);
				Assert.Equal("Alexander", actual.Name);
		    }
	    }

		[Fact]
	    public async void Find_Given_Non_Existing_Id_Returns_Null()
	    {
		    var ctx = Util.GetNewContext(nameof(Find_Given_Non_Existing_Id_Returns_Null));

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var actual = await repo.Find(Guid.NewGuid());
			    Assert.Null(actual);
		    }
	    }

	    [Fact]
	    public async void FindFromFacebookId_Given_Existing_Id_Returns_User()
	    {
		    var ctx = Util.GetNewContext(nameof(FindFromFacebookId_Given_Existing_Id_Returns_User));
		    var user = new UserEntity { Name = "Alexander", FacebookId = "101238871102"};
		    ctx.Users.Add(user);
		    await ctx.SaveChangesAsync();

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var actual = await repo.FindFromFacebookId(user.FacebookId);
			    Assert.NotNull(actual);
			    Assert.Equal("101238871102", actual.FacebookId);
				Assert.Equal("Alexander", actual.Name);
		    }
	    }

	    [Fact]
	    public async void FindFromFacebookId_Given_Non_Existing_Id_Returns_Null()
	    {
		    var ctx = Util.GetNewContext(nameof(FindFromFacebookId_Given_Existing_Id_Returns_User));

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var actual = await repo.FindFromFacebookId("101238871102");
			    Assert.Null(actual);
		    }
	    }

	    [Fact]
	    public async void Create_Creates_Given_CreateDto()
	    {
		    var ctx = Util.GetNewContext(nameof(FindFromFacebookId_Given_Existing_Id_Returns_User));

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var dto = new UserCreateDto {Name = "Alexander", FacebookId = "101238871102"};
			    var guid = await repo.CreateUser(dto);
			    var created = ctx.Users.SingleOrDefault(u => u.Id == guid);
			    Assert.True(guid != Guid.Empty);
				Assert.NotNull(created);
				Assert.Equal("Alexander", created.Name);
				Assert.Equal("101238871102", created.FacebookId);
		    }
	    }

	    [Fact]
	    public async void FindAll_Returns_All_Users()
	    {
			var ctx = Util.GetNewContext(nameof(FindAll_Returns_All_Users));

		    var users = new List<UserEntity>
		    {
			    new UserEntity {Name = "Alexander"},
			    new UserEntity {Name = "Mikkel"},
			    new UserEntity {Name = "Phillip"},
		    };

			ctx.AddRange(users);
		    await ctx.SaveChangesAsync();

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var expected = users.Select(u => new UserDto
				{
					Name = u.Name,
					FacebookId = u.FacebookId,
					Id = u.Id,
			    }).ToList();

				var actual = await repo.FindAll();

				Assert.Equal(expected, actual);
		    }
		}

	    [Fact]
	    public async void Update_Given_dto_returns_true()
	    {
			var ctx = Util.GetNewContext(nameof(Update_Given_dto_returns_true));

		    var user = new UserEntity { Name = "John Do"};

		    ctx.Add(user);
		    await ctx.SaveChangesAsync();

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var dto = new UserDto
			    {
				    Name = "John Doe",
				    FacebookId = user.FacebookId,
				    Id = user.Id,
			    };

			    var updated = await repo.Update(dto);

				Assert.True(updated);
			    Assert.Equal("John Doe", user.Name);
		    }
		}

	    [Fact]
	    public async void Update_Given_Dto_Not_In_Context_Returns_False()
	    {
			var ctx = Util.GetNewContext(nameof(Update_Given_dto_returns_true));

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var dto = new UserDto
			    {
					Name = "John Doe",
				    Id = Guid.NewGuid(),
			    };

			    var updated = await repo.Update(dto);
				Assert.False(updated);

		    }
		}

	    [Fact]
	    public async void Delete_Given_Dto_Returns_True()
	    {
		    var ctx = Util.GetNewContext(nameof(Update_Given_dto_returns_true));

		    var user = new UserEntity {Name = "John Doe"};

		    ctx.Users.Add(user);
		    await ctx.SaveChangesAsync();

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var deleted = await repo.Delete(user.Id);

			    user = ctx.Users.SingleOrDefault(u => u.Id == user.Id);
				Assert.Null(user);
			    Assert.True(deleted);

		    }
	    }

	    [Fact]
	    public async void Delete_Given_Dto_Not_In_Context_Returns_False()
	    {
		    var ctx = Util.GetNewContext(nameof(Delete_Given_Dto_Not_In_Context_Returns_False));

		    using (var repo = new EFUserRepository(ctx))
		    {
			    var deleted = await repo.Delete(Guid.NewGuid());
				Assert.False(deleted);

		    }
	    }




	}
}
