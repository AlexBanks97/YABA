using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Yaba.Common.Budget.DTO;
using Yaba.UWPApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BudgetsOverview : Page
	{
		private readonly BudgetOverviewViewModel _vm;
		public BudgetsOverview()
		{
			this.InitializeComponent();
			_vm = App.ServiceProvider.GetService<BudgetOverviewViewModel>();
			
			// change to proper Dto (the one returned from apo)
			//List<BudgetSimpleDto> items = new List<BudgetSimpleDto>
			//{
			//	new BudgetSimpleDto(){Id = new Guid(), Name="My Personal Budget" },
			//	new BudgetSimpleDto(){Id = new Guid(), Name="My Company Budget" },
			//};
			//BudgetOverviewList.ItemsSource = items;

		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			await _vm.Initialize();

			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
		}

		private void List_Click(object sender, ItemClickEventArgs e)
		{
			this.Frame.Navigate(typeof(Yaba.UWPApp.Views.BudgetView), e.ClickedItem, null);
		}

		private void Add_Budget_Click(object sender, RoutedEventArgs e)
		{
			addBudgetPopup.IsOpen = true;
			this.Frame.Background = new SolidColorBrush { Color = Colors.Black };
			this.Frame.Opacity = 0.2;
		}

		private void AddBudgetPopup_OnClosed(object sender, object e)
		{
			addBudgetPopup.IsOpen = false;
			this.Frame.Opacity = 1;
		}
	}
}
