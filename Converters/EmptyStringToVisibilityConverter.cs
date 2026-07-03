using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class EmptyStringToVisibilityConverter : IValueConverter
{
    public bool Invert { get; set; } = false;
    public bool UseHidden { get; set; } = false;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isNullOrEmpty = string.IsNullOrWhiteSpace(value as string);

        if (Invert)
            isNullOrEmpty = !isNullOrEmpty;

        return !isNullOrEmpty ? Visibility.Visible : (UseHidden ? Visibility.Hidden : Visibility.Collapsed);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
