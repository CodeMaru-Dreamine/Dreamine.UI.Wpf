using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class ValueComparisionToVisibilityConverter : IValueConverter
{
    //private bool _inverse = false;
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Visibility.Hidden;

        bool result = System.Convert.ToDouble(value) > System.Convert.ToDouble(parameter);

		return result ? Visibility.Visible : Visibility.Hidden;

		//if (!_inverse)
		//    return result ? Visibility.Visible : Visibility.Hidden;

		// return !result ? Visibility.Visible : Visibility.Hidden;
	}

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

