using System;
using System.Collections.Generic;
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
using Yaba.Common.Tab.DTO;
using Yaba.Common.Tab.DTO.Item;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yaba.UWPApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SpecificTabPage : Page
    {
		List<TabItemDTO> items;
		public SpecificTabPage()
        {
            this.InitializeComponent();
			items = new List<TabItemDTO>
			{
				new TabItemDTO { Amount = 42, Description = "Pizza" },
				new TabItemDTO { Amount = -123, Description = "Lost bet" },
				new TabItemDTO { Amount = 99, Description = "EA microtransactions" },
			};
			SpecificTabOverview.ItemsSource = items;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var clickedItem = e.Parameter as TabDto;

			tabTitle.Text = clickedItem.Id.ToString();
			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

		}
	}
}
