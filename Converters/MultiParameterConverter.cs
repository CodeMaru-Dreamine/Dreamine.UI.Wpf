using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class MultiParameterConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return Tuple.Create(values[0], values[1]);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
