using Microsoft.EntityFrameworkCore;

namespace Yaba.Entities.Test
{
	public class Util
	{


		internal static DbContextOptions<YabaDBContext> GetInMemoryDatabase(string name = "default_in_memory")
		{
			return new DbContextOptionsBuilder<YabaDBContext>()
				.UseInMemoryDatabase(name)
				.Options;
		}

		public static YabaDBContext GetNewContext(string name)
		{
			var context = new YabaDBContext(GetInMemoryDatabase(name));
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}
	}
}
