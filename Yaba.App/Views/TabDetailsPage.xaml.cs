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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.App.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TabDetailsPage : Page
	{
		private readonly TabDetailsViewModel _vm;
		public TabDetailsPage()
		{
			this.InitializeComponent();
			_vm = App.ServiceProvider.GetService<TabDetailsViewModel>();
			DataContext = _vm;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
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


	}
}
