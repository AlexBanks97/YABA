using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Yaba.Common.Tab.DTO;
using Yaba.Common.Tab;
using Yaba.Common;
using Windows.UI.Core;
using Windows.UI;
using System.Collections.ObjectModel;
using Yaba.UWPApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TabsOverview : Page
	{
		private readonly TabOverviewViewModel _vm;
		public TabsOverview()
		{
			this.InitializeComponent();
			_vm = App.ServiceProvider.GetService<TabOverviewViewModel>();
		}

		private void List_Click(object sender, ItemClickEventArgs e)
		{
			this.Frame.Navigate(typeof(SpecificTabPage), e.ClickedItem);
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			await _vm.Initialize();
			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

		}

		private void Add_Tab_Click(object sender, RoutedEventArgs e)
		{
			addTabPopup.IsOpen = true;
			this.Frame.Background = new SolidColorBrush { Color = Colors.Black };
			this.Frame.Opacity = 0.2;
		}

		private void AddTabPopup_OnClosed(object sender, object e)
		{
			addTabPopup.IsOpen = false;
			this.Frame.Opacity = 1;
		}
	}

}
