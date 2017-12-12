﻿using System;
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
using System.Text;

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
			var i = 0;
			var client = new Auth0Client(new Auth0ClientOptions
			{
				Domain = "cntest.eu.auth0.com",
				ClientId = "kpJCzk6eiBai0XVYQ904cgIHGG907PfD",
				ClientSecret = "a2k9vxXuZTXoapriCmugGWNzFxCJnBcR0WwXXFFJxGZuG9E_88evNUQeLKkX_Esw"
			});

			var loginResult = await client.LoginAsync();

			var sb = new StringBuilder();
			if (loginResult.IsError)
			{
				i = 1;
				sb.AppendLine($"An error occurred during login: {loginResult.Error}");
			}
			else
			{
				sb.AppendLine($"ID Token: {loginResult.IdentityToken}");
				sb.AppendLine($"Access Token: {loginResult.AccessToken}");
				sb.AppendLine($"Refresh Token: {loginResult.RefreshToken}");

				sb.AppendLine();

				sb.AppendLine("-- Claims --");
				foreach (var claim in loginResult.User.Claims)
				{
					sb.AppendLine($"{claim.Type} = {claim.Value}");
				}
			}
			Debug.WriteLine($"{sb}");

			return i;
		}

		
		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			
			var x = await LoginResult();

			if(x == 0) this.Frame.Navigate(typeof(NavigationPage));
		}
	}
}
