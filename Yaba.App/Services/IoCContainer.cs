using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yaba.App.Models;
using Yaba.App.ViewModels;
using Yaba.Common.Budget;


namespace Yaba.App.Services
{
	public class IoCContainer
	{
		public static IServiceProvider Create() => ConfigureServices();

		private static IServiceProvider ConfigureServices()
		{

			IServiceCollection services = new ServiceCollection();

			services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
			services.AddScoped<DelegatingHandler, AuthorizedHandler>();

			services.AddSingleton<INavigationService>(new NavigationService());

			// repositories
			services.AddScoped<IBudgetRepository, BudgetRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			

			// VMs
			services.AddTransient<MainViewModel>();
			services.AddTransient<BudgetsViewModel>();
			services.AddTransient<BudgetsDetailViewModel>();
			services.AddTransient<CategoryViewModel>();


			//services.AddScoped<ICharacterRepository, CharacterRepository>();

			return services.BuildServiceProvider();
		}
	}
}
