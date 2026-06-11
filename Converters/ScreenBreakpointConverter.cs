using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class ScreenBreakpointConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        var input = System.Convert.ToDouble(value);
        var threshold = System.Convert.ToDouble(parameter);

        return input >= threshold;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}