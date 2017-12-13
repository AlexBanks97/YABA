using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
		String name;
        public MainPage()
        {
            this.InitializeComponent();
			
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			var code = await Authorize();

			if (code == 0) this.Frame.Navigate(typeof(NavigationPage), name);
		}

		public async Task<int> Authorize()
		{
			/* Change once access has been granted
		praffn.eu.auth0.com
		mb46CJma8ApOBjibmEdJzmcEQaJn976G
			 */
			var i = 0;
			var client = new Auth0Client(new Auth0ClientOptions
			{
				Domain = "praffn.eu.auth0.com",
				ClientId = "DRDKy6mF0gWtMFkExlIt0HY2DvqkxPgO",
				Scope = "openid email profile",
				//ClientSecret = "a2k9vxXuZTXoapriCmugGWNzFxCJnBcR0WwXXFFJxGZuG9E_88evNUQeLKkX_Esw"
			});

			var loginResult = await client.LoginAsync(new
			{
				audience = "https://yaba.dev"
			});

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

					if(claim.Type.ToString() == "name") name = claim.Value;
				}

				
			}

			if (name == null) name = "Mr. Mallory";

			Debug.WriteLine($"{sb}");
			return i;

		}
	
		
	}
}
