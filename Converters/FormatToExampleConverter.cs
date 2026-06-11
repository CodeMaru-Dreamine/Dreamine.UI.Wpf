using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	public enum eInputFormat { ASCII, HEX }

	public class FormatToExampleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is eInputFormat fmt)
			{
				return fmt == eInputFormat.ASCII
					? "예시: \\s123\\e\\n\\,실제텍스트"
					: "예시: 02 32 33 34 03,32";
			}
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}
