using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class EnumToVisibilityConverter : IValueConverter
{
    public bool IsInverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return Visibility.Collapsed;

        string enumValue = value.ToString() ?? string.Empty;
        string[] targetValues = parameter.ToString()?
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];

        bool result = targetValues.Contains(enumValue, StringComparer.OrdinalIgnoreCase)
            ? true
            : false;

        result = IsInverse ? !result : result;

        return result ? Visibility.Visible : Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
