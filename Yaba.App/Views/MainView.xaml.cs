using System;
using System.Collections.Generic;
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
using Yaba.App.Models;
using Yaba.App.Services;
using Yaba.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.App.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainView : Page
	{
		private readonly MainViewModel _vm;
		public MainView()
		{
			InitializeComponent();
			_vm = App.ServiceProvider.GetService<MainViewModel>();
			DataContext = _vm;
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			await _vm.Initialize();

			// set nav items
			GridView.ItemsSource = new[]
			{
				new {Name = "Budgets", Page = typeof(BudgetsPage), Symbol = "Accept" },
				new {Name = "Tabs", Page = typeof(TabsPage), Symbol = "AddFriend" },
			};

			//

			var rootFrame = Window.Current.Content as Frame;
			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack
				? AppViewBackButtonVisibility.Visible
				: AppViewBackButtonVisibility.Collapsed;
		}

		public void setWebViewUrl(Uri uri)
		{
			WebView1.Navigate(uri);
		}

		private void GridView_OnItemClick(object sender, ItemClickEventArgs e)
		{
			var item = e.ClickedItem;
			var page = item?.GetType().GetProperty("Page")?.GetValue(item, null) as Type;
			if (page != null)
			{
				var nav = App.ServiceProvider.GetService<INavigationService>();
				nav.Navigate(page, null);
			}
		}

		private void SignOut_OnClick(object sender, RoutedEventArgs e)
		{
		}
	}
}
