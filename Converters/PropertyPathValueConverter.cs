using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class PropertyPathValueConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2 || values[0] == null || values[1] == null) return null!;
        var item = values[0];
        var propName = values[1]?.ToString();
        if (string.IsNullOrWhiteSpace(propName)) return null!;
        var prop = item.GetType().GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        return prop?.GetValue(item)!;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return new object[] { Binding.DoNothing, Binding.DoNothing };
    }
}

public class PropertyAccessorConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2 || values[0] == null || values[1] == null) return null!;
        var item = values[0];
        var propName = values[1]?.ToString();
        if (string.IsNullOrWhiteSpace(propName)) return null!;
        var prop = item.GetType().GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        return prop?.GetValue(item)!;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        if (parameter is object[] p && p.Length >= 2) {  }
        return new object[] { Binding.DoNothing, Binding.DoNothing };
    }
}
