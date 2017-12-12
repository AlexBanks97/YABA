using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yaba.UWPApp.ViewModels;
using Yaba.Common.Budget;

namespace Yaba.UWPApp.Models
{
	public class IoCContainer
	{
		public static IServiceProvider Create() => ConfigureServices();

		private static IServiceProvider ConfigureServices()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddScoped<IBudgetRepository, MockBudgetRepository>();

			// VMs
			// AddTransiet, NOT AddScoped, otherwise it adds new things on every page reload ._.
			services.AddScoped<BudgetDetailViewModel>();
			services.AddTransient<BudgetOverviewViewModel>();
			
			//services.AddScoped<ICharacterRepository, CharacterRepository>();

			return services.BuildServiceProvider();
		}
	}
}
