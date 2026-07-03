using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public sealed class BooleanNegationConverter : IValueConverter
{
	public static BooleanNegationConverter Instance { get;  } = new();
    public object Convert(object value, Type t, object p, CultureInfo c) => value is bool b ? !b : value;
    public object ConvertBack(object value, Type t, object p, CultureInfo c) => value is bool b ? !b : value;
}
