using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class NumberClampConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IComparable comparableValue)
        {
            int? min = null;
            int? max = null;

            if (parameter is string str)
            {
                var parts = str.Split(';');
                if (parts.Length > 0 && int.TryParse(parts[0], out int minVal))
                    min = minVal;
                if (parts.Length > 1 && int.TryParse(parts[1], out int maxVal))
                    max = maxVal;
            }

            var numeric = System.Convert.ToInt32(value);

            if (min.HasValue && numeric < min.Value)
                return min.Value;

            if (max.HasValue && numeric > max.Value)
                return max.Value;

            return numeric;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
