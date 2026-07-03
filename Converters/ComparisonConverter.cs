using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class ComparisonConverter : IValueConverter
{
    public bool IsInverse { get; set; } = false;
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        bool result = value.ToString() == parameter.ToString();
        return IsInverse ? !result : result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.Equals(true) == true ? parameter : Binding.DoNothing;
    }
}
