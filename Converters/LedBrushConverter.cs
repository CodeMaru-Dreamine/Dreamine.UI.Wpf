using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// @brief On/Off에 따라 OnBrush 또는 OffBrush를 선택하는 컨버터.
	/// @details MultiBinding 순서: 0: IsOn(bool), 1: OnBrush(Brush), 2: OffBrush(Brush)
	/// </summary>
	public sealed class LedBrushConverter : IMultiValueConverter
	{
		/// <inheritdoc />
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool isOn = (values != null && values.Length > 0 && values[0] is bool b) && b;
			var onBrush = (values != null && values.Length > 1) ? values[1] : null;
			var offBrush = (values != null && values.Length > 2) ? values[2] : null;
			return (isOn ? onBrush : offBrush) ?? null!;
		}

		/// <inheritdoc />
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
			throw new NotSupportedException();
	}
}
