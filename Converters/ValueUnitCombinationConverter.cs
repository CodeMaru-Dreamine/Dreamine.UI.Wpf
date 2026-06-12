using System.Globalization;
using System.Windows.Data;
using Dreamine.UI.Wpf.Localization;

namespace Dreamine.UI.Wpf.Converters;

public class ValueUnitCombinationConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var value = values[0]?.ToString() ?? string.Empty;
        var unit = values[1]?.ToString();

        var label = value;

        if (string.IsNullOrEmpty(unit))
            return label;

        return $"{label} ({unit})";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
