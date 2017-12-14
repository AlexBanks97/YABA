using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Yaba.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Yaba.Common.Budget.DTO;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.App.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BudgetsPage : Page
	{
		private readonly BudgetsViewModel _vm;
		public BudgetsPage()
		{
			InitializeComponent();

			_vm = App.ServiceProvider.GetService<BudgetsViewModel>();
			DataContext = _vm;
		}

		private void SetupNavigationView()
		{
			/*NavigationView.MenuItems.Clear();
			var navItems = _vm.Budgets.Select(i => new NavigationViewItem()
			{
				Content = i.Name,
				Tag = i.Id,
			});
			foreach (var item in navItems)
			{
				NavigationView.MenuItems.Add(item);
			}*/
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			//_vm.Budgets.CollectionChanged += (sender, args) => SetupNavigationView();
			await _vm.Initialize();

			//SetupNavigationView();


			var rootFrame = Window.Current.Content as Frame;

			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack
				? AppViewBackButtonVisibility.Visible
				: AppViewBackButtonVisibility.Collapsed;
		}

		private void BudgetsList_OnClick(object sender, ItemClickEventArgs e)
		{
			Detail.Navigate(typeof(BudgetDetailView), e.ClickedItem);
		}
	}
}
