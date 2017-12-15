using System;
using System.Diagnostics;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Auth0.OidcClient;
using System.Threading.Tasks;
using System.Text;
using Windows.UI.Xaml.Navigation;
using Yaba.UWPApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Yaba.Common.Budget;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Yaba.UWPApp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
    {
		String name;
        public MainPage()
        {
            this.InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(NavigationPage), name);
		}
	}
}
