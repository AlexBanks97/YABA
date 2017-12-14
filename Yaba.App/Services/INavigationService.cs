using System;

namespace Yaba.App.Services
{
	public interface INavigationService
	{
		bool Navigate(Type sourcePage, object parameter);
	}
}
