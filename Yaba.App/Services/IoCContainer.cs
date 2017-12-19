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
using Yaba.Common.Payment;


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
			services.AddScoped<IItemRepository, ItemRepository>();
			services.AddScoped<IEntryRepository, EntryRepository>();
			services.AddScoped<PaymentRepository, PaymentRepository>();
			services.AddScoped<IGoalRepository, GoalRepository>();

			// kek
			services.AddScoped<IUserHelper, UserHelper>();

			// VMs
			services.AddTransient<MainViewModel>();
			services.AddTransient<BudgetsPageViewModel>();
			services.AddTransient<BudgetsDetailViewModel>();
			services.AddTransient<CategoryPageViewModel>();
			services.AddTransient<TabsPageViewModel>();
			services.AddTransient<TabDetailsViewModel>();
			services.AddTransient<TabItemViewModel>();
			services.AddTransient<CategoryCreateViewModel>();
			services.AddTransient<PayPalPaymentViewModel>();

			//services.AddScoped<ICharacterRepository, CharacterRepository>();

			return services.BuildServiceProvider();
		}
	}
}
