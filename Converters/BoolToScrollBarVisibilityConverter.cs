using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class BoolToScrollBarVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isVisible)
        {
            return isVisible ? ScrollBarVisibility.Auto : ScrollBarVisibility.Disabled;
        }

        return ScrollBarVisibility.Disabled;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ScrollBarVisibility visibility)
        {
            return visibility == ScrollBarVisibility.Auto;
        }

        return false;
    }
}
