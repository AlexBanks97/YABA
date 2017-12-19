using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Yaba.Entities;

namespace Yaba.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = BuildWebHost(args);

			/* using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var context = services.GetRequiredService<IYabaDBContext>();
					using (context)
					{
						(context as DbContext).Database.MigrateAsync().Wait();
						DbInitializer.Initialize(context);
					}
				}
				catch
				{
					// ignored
				}
			} */

			host.Run();
		}

		private static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
