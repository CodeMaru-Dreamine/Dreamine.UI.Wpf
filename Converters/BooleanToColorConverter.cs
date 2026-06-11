using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Dreamine.UI.Wpf.Converters
{
	public class BooleanToColorConverter : IValueConverter
	{
		public Color OnColor { get; set; } = Colors.Orchid;
		public Color OffColor { get; set; } = Colors.Yellow;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (value is bool b && b) ? OnColor : OffColor;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}
}
