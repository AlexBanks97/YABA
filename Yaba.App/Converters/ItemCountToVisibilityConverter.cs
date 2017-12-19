using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Yaba.App.Converters
{
	public class ItemCountToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (!(value is int count)) return Visibility.Collapsed;

			var visibleIfAboveZero = true;
			if (parameter is string strn)
			{
				if (!bool.TryParse(strn, out visibleIfAboveZero))
				{
					visibleIfAboveZero = true;
				}
			}
			
			var visibility = (count > 0) == visibleIfAboveZero
				? Visibility.Visible
				: Visibility.Collapsed;
			return visibility;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
