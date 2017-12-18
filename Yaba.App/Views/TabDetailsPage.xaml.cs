using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Yaba.App.Models;
using Yaba.App.ViewModels;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.App.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TabDetailsPage : Page, IView
	{
		private readonly TabDetailsViewModel _vm;

		public TabDetailsPage()
		{
			InitializeComponent();
			_vm = App.ServiceProvider.GetService<TabDetailsViewModel>();
			DataContext = _vm;
			(DataContext as TabDetailsViewModel).View = this as IView; //

		}

		public async void OpenUriInWebView(Uri uri)
		{
			
			var success = await Windows.System.Launcher.LaunchUriAsync(uri);
			if (success)
			{
				// URI launched
				PayPalPopup.IsOpen = false;
			}
			else
			{
				// URI launch failed
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			var tabvm = e.Parameter as TabViewModel;
			if (tabvm == null) throw new Exception();
			_vm.CurrentTabId = tabvm.Id;
			_vm.Initialize(tabvm.Id, tabvm.UserNotCurrentUser.Name);

		}



		private void TogglePayPalPopup(object sender, RoutedEventArgs e)
		{
			if (!PayPalPopup.IsOpen)
				PayPalPopup.IsOpen = true;
			else
				PayPalPopup.IsOpen = false;
		}

		private void ToggleStripePopup(object sender, RoutedEventArgs e)
		{
			if (!StripePopup.IsOpen)
				StripePopup.IsOpen = true;
			else
				StripePopup.IsOpen = false;
		}

		private void DismissSuccessPopup(object sender, RoutedEventArgs e)
		{
			SuccessPopup.IsOpen = false;
			_vm.Success = false;
		}

		private void DismissFailurePopup(object sender, RoutedEventArgs e)
		{
			FailurePopup.IsOpen = false;
			_vm.Failure = false;
		}
	}
}
