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
using Yaba.UWPApp.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class NavigationPage : Page
	{
		public NavigationPage()
		{
			this.InitializeComponent();
			setBackButtonFuctionality();



		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			var parameters = (String) e.Parameter;

			ContentFrame.Navigate(typeof(UserGreeting), parameters);

		}

		private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
		{
			var selected = args.SelectedItem as NavigationViewItem;
			if (selected == null) return;

			if (selected.Tag.ToString() == "budgets")
			{
				ContentFrame.Navigate(typeof(BudgetsOverview));
			}
			else
			{
				ContentFrame.Navigate(typeof(TabsOverview));
			}
		}

		private void setBackButtonFuctionality()
		{
			var rootFrame = ContentFrame;
			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
			SystemNavigationManager.GetForCurrentView().BackRequested += (s, ee) =>
			{
				if (rootFrame == null)
					return;

				// Navigate back if possible, and if the event has not 
				// already been handled .
				if (rootFrame.CanGoBack && ee.Handled == false)
				{
					ee.Handled = true;
					rootFrame.GoBack();
				}
			};
		}
	}
}
