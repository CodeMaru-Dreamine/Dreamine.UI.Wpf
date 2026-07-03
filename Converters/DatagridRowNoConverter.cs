using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class DatagridRowNoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int index)
            return (index + 1).ToString();
        return "1";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
