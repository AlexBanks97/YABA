using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Auth0.OidcClient;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Yaba.UWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
			
		}

		public async Task<int> LoginResult()
		{
			/* Change once access has been granted
		praffn.eu.auth0.com
		mb46CJma8ApOBjibmEdJzmcEQaJn976G
			 */

			var client = new Auth0Client(new Auth0ClientOptions
			{
				Domain = "cntest.eu.auth0.com",
				ClientId = "kpJCzk6eiBai0XVYQ904cgIHGG907PfD"
			});

			var loginResult = await client.LoginAsync();

			if (loginResult.IsError)
			{
				Debug.WriteLine($"An error occurred during login: {loginResult.Error}");
			}

			if (!loginResult.IsError)
			{
				Debug.WriteLine($"id_token: {loginResult.IdentityToken}");
				Debug.WriteLine($"access_token: {loginResult.AccessToken}");
			}

			// User info
			if (!loginResult.IsError)
			{
				Debug.WriteLine($"name: {loginResult.User.FindFirst(c => c.Type == "name")?.Value}");
				Debug.WriteLine($"email: {loginResult.User.FindFirst(c => c.Type == "email")?.Value}");
			}

			// Claims
			if (!loginResult.IsError)
			{
				foreach (var claim in loginResult.User.Claims)
				{
					Debug.WriteLine($"{claim.Type} = {claim.Value}");
				}
			}

			return 0;
		}

		
		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			
			var x = await LoginResult();

			this.Frame.Navigate(typeof(NavigationPage));
		}
	}
}
