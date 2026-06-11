using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	// values[0]=IsEmName, values[1]=WireName, values[2]=EmName
	public class BoolPickNameConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool isEm = values.Length > 0 && values[0] is bool b && b;
			var wire = values.Length > 1 ? values[1] : null;
			var em = values.Length > 2 ? values[2] : null;

			// 요구사항: IsEmName == true => WireName, false => EmName
			return (isEm ? wire : em) ?? null!;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}
