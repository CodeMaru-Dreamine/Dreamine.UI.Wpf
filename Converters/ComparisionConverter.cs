using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class ComparisionConverter : IValueConverter
{
    public bool ToVisibility { get; set; }
    public bool Inverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        var isEqual = value.ToString() == parameter.ToString();

        if (Inverse)
        {
            isEqual = !isEqual;
        }

        if (ToVisibility)
        {
            return isEqual ? Visibility.Visible : Visibility.Collapsed;
        }

        return isEqual;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.Equals(true) == true ? parameter : Binding.DoNothing;
    }
}
