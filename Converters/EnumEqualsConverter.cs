using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	public sealed class EnumEqualsConverter : IValueConverter
	{
		/// <summary>enum → bool</summary>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null) return false;
			return value.Equals(parameter);
		}

		/// <summary>bool → enum</summary>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null) return Binding.DoNothing;
			// true일 때만 enum으로 반영, false면 변경하지 않음
			return value is bool b && b ? parameter : Binding.DoNothing;
		}
	}
}
