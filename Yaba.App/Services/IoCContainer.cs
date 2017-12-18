using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yaba.App.Models;
using Yaba.App.ViewModels;
using Yaba.Common;
using Yaba.Common.Budget;


namespace Yaba.App.Services
{
	public class IoCContainer
	{
		public static IServiceProvider Create() => ConfigureServices();

		private static IServiceProvider ConfigureServices()
		{

			IServiceCollection services = new ServiceCollection();

			// global settings
			services.AddSingleton(new AppConstants());

			services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
			services.AddScoped<DelegatingHandler, AuthorizedHandler>();

			services.AddSingleton<INavigationService>(new NavigationService());

			// repositories
			services.AddScoped<IBudgetRepository, BudgetRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<ITabRepository, TabRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<PaymentRepository, PaymentRepository>();
			services.AddScoped<IGoalRepository, GoalRepository>();

			// kek
			services.AddScoped<IUserHelper, UserHelper>();

			// VMs
			services.AddTransient<MainViewModel>();
			services.AddTransient<BudgetsViewModel>();
			services.AddTransient<BudgetsDetailViewModel>();
			services.AddTransient<CategoryViewModel>();
			services.AddTransient<TabsPageViewModel>();
			services.AddTransient<CategoryCreateViewModel>();


			//services.AddScoped<ICharacterRepository, CharacterRepository>();

			return services.BuildServiceProvider();
		}
	}
}
