using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class AndConverter : IMultiValueConverter
{
    public bool TreatNullAsTrue { get; set; } = false;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length == 0) return false;

        foreach (var v in values)
        {
            if (v == null) { if (!TreatNullAsTrue) return false; else continue; }

            if (v is bool b)
            {
                if (!b) return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
