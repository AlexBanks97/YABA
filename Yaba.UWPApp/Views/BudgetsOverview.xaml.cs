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
using Yaba.Common.Budget.DTO;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BudgetsOverview : Page
	{
		public BudgetsOverview()
		{
			this.InitializeComponent();

			// change to proper Dto (the one returned from apo)
			List<BudgetDto> items = new List<BudgetDto>
			{
				new BudgetDto(){Id = new Guid(), Name="My Personal Budget", Categories = { }, Expenses = { }, Recurrings = { } },
				new BudgetDto(){Id = new Guid(), Name="My Company Budget", Categories = { }, Expenses = { }, Recurrings = { } },
			};
			BudgetOverviewList.ItemsSource = items;
		}

		private void List_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Yaba.UWPApp.Views.BudgetView));
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//this.Frame.Navigate(typeof(MainPage));
		}
	}
}
