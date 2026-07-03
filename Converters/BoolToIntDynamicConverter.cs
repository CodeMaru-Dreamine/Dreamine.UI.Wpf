using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class BoolToIntDynamicConverter : IValueConverter
{
    public bool IsInverse { get; set; }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return 0;

        bool bValue = value is bool b ? b
            : value is string s ? (s == "1" || string.Equals(s, "true", StringComparison.OrdinalIgnoreCase))
            : value is int i ? (i != 0)
            : false;

        string[] strParam = parameter.ToString()!.Split(",");

        if (!IsInverse)
            return bValue ? strParam[0] : strParam[1];

        return bValue ? strParam[1] : strParam[0];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
