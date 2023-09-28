using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace TaskManager.Converter;

public class EnumDescriptionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;

        var enumValue = (Enum)value;
        return GetDescription(enumValue);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    private static string GetDescription(Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        var descriptionAttributes = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return descriptionAttributes is { Length: > 0 }
            ? ((DescriptionAttribute)descriptionAttributes[0]).Description
            : enumValue.ToString();
    }
}