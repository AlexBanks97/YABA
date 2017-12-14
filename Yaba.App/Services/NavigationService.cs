using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yaba.App.Services
{
	public class NavigationService : INavigationService
	{
		public bool Navigate(Type sourcePage, object parameter)
		{
			if (Window.Current.Content is Frame rootFrame)
			{
				return rootFrame.Navigate(sourcePage, parameter);
			}
			return false;
		}
	}
}
