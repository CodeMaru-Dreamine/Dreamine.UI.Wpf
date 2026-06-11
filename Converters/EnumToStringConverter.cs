using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class EnumToStringConverter : IValueConverter
{
    public bool IsUpper { get; set; }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return IsUpper ? value?.ToString()?.ToUpper()! : value?.ToString()!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str && Enum.IsDefined(targetType, str))
        {
            return Enum.Parse(targetType, str);
        }
        return Binding.DoNothing;
    }
}
