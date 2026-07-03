using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class DoubleToIsCheckedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        if (double.TryParse(System.Convert.ToString(value, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, out double targetValue) &&
            double.TryParse(System.Convert.ToString(parameter, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, out double currentValue))
        {
            return Math.Abs(currentValue - targetValue) < 0.0001;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value && double.TryParse(parameter.ToString(), CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }

        return Binding.DoNothing;
    }
}
