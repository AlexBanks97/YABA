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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TabsOverview : Page
	{
		public TabsOverview()
		{
			
			this.InitializeComponent();
			List<TabDto> items = new List<TabDto>
			{
				new TabDto{Balance=100,State= State.Active,TabItems={ } },
				new TabDto{Balance=100,State= State.Active,TabItems={ } },
				new TabDto{Balance=100,State= State.Active,TabItems={ } },
			};
			TabOverviewList.ItemsSource = items;
		}

		private void List_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Yaba.UWPApp.Views.BudgetView));
		}

	}

}
