using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class VisibilityToIntegerConverter : IValueConverter
{
    public bool IsInversed { get; set; }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible ^ !IsInversed ? 1 : 0;
        }

        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
