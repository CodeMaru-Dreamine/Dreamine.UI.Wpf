using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class UppercaseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return string.Empty;

        return value ?? value?.ToString()!.ToUpperInvariant() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
