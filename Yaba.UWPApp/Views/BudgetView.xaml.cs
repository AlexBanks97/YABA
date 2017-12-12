using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Yaba.Common.Budget.DTO;
using Yaba.UWPApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BudgetView : Page
	{
		private readonly BudgetDetailViewModel _vm;

		public BudgetView()
		{
			InitializeComponent();

			_vm = App.ServiceProvider.GetService<BudgetDetailViewModel>();

			DataContext = _vm;
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			var budget = e.Parameter as BudgetSimpleDto;

			await _vm.Initialize(budget);
		}
	}
}
