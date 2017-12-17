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
using Microsoft.Extensions.DependencyInjection;
using Yaba.App.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.App.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TabsPage : Page
	{
		private readonly TabsPageViewModel _vm;
		public TabsPage()
		{
			this.InitializeComponent();
			_vm = App.ServiceProvider.GetService<TabsPageViewModel>();
			DataContext = _vm;
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			await _vm.Initialize();

			/*

				< ListView.ItemTemplate >

				< DataTemplate >

				< StackPanel >

				< TextBlock x: Name = "TabTitle" />
  
				</ StackPanel >
  
				</ DataTemplate >
  
				</ ListView.ItemTemplate > */

			var rootFrame = Window.Current.Content as Frame;

			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack
				? AppViewBackButtonVisibility.Visible
				: AppViewBackButtonVisibility.Collapsed;
		}

		private void TabsList_OnClick(object sender, ItemClickEventArgs e)
		{
			Detail.Navigate(typeof(TabDetailsPage), e.ClickedItem);
		}
	}
}
