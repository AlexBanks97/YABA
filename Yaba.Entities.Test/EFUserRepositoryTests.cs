using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaba.Common.User.DTO;
using Yaba.Entities.User.Repository;

namespace Yaba.Entities.Test
{
    public class EFUserRepositoryTests
    {
		[Fact]
		public async void Find_Given_Existing_Id_Returns_User()
		{
			var context = Util.GetNewContext(nameof(Find_Given_Existing_Id_Returns_User));

			var user = new UserDetailsDto { Name = "Alexander" };
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
