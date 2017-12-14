using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yaba.UWPApp.ViewModels;
using Yaba.Common.Budget;
using Yaba.UWPApp.Models.Repositories;
using Yaba.UWPApp.Views;
using Yaba.Common;

namespace Yaba.UWPApp.Models
{
	public class IoCContainer
	{
		public static IServiceProvider Create() => ConfigureServices();

		private static IServiceProvider ConfigureServices()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
			services.AddScoped<DelegatingHandler, AuthorizedHandler>();
			services.AddScoped<IBudgetRepository, MockBudgetRepository>();
			services.AddScoped<ITabRepository, TabRepository>();

			// VMs
			// AddTransiet, NOT AddScoped, otherwise it adds new things on every page reload ._.
			services.AddScoped<BudgetDetailViewModel>();
			services.AddTransient<BudgetOverviewViewModel>();
			services.AddTransient<TabOverviewViewModel>();
			
			//services.AddScoped<ICharacterRepository, CharacterRepository>();

			return services.BuildServiceProvider();
		}
	}
}
