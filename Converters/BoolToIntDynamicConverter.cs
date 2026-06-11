using System.Globalization;
using System.Windows.Data;
using VsLibrary.Base.Helper.Utils;

namespace Dreamine.UI.Wpf.Converters;

public class BoolToIntDynamicConverter : IValueConverter
{
    public bool IsInverse { get; set; }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return 0;

        bool bValue = DataConverter.ToBoolean(value);

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
