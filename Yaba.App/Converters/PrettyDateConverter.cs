using System;
using Windows.UI.Xaml.Data;

namespace Yaba.App.Converters
{
	public class PrettyDateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (!(value is DateTime date)) return null;

			var span = DateTime.Now - date;
			if (span.Days > 100)
			{
				return date.ToShortDateString();
			}
			if (span.Days > 30)
			{
				var months = (span.Days / 30);
				if (span.Days % 31 != 0)
					months += 1;
				return string.Format("About {0} {1} ago", months, months == 1 ? "month" : "months");
			}
			if (span.Days > 0)
			{
				return string.Format("About {0} {1} ago", span.Days, span.Days == 1 ? "day" : "days");
			}
			if (span.Hours > 0)
			{
				return string.Format("About {0} {1} ago", span.Hours, span.Hours == 1 ? "hour" : "hours");
			}
			if (span.Minutes > 0)
			{
				return string.Format("About {0} {1} ago", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
			}
			if (span.Seconds > 5)
			{
				return string.Format("About {0} seconds ago", span.Seconds);
			}
			if (span.Seconds <= 5)
			{
				return "Just now";
			}
			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
