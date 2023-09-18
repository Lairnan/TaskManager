using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace TaskManager.Converter
{
	public class ActualWidthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (double)value - 22d;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
