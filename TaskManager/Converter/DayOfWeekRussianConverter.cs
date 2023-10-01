using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskManager.Converter;

public class DayOfWeekRussianConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return string.Empty;

        string[] days = { "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

        if (value is DayOfWeek dayOfWeek) return days[(int)dayOfWeek];
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}