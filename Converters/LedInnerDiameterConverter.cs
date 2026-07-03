using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// @brief 내부 원의 지름을 계산합니다.
	/// @details MultiBinding 순서: 0: Diameter, 1: InnerScale
	/// </summary>
	public sealed class LedInnerDiameterConverter : IMultiValueConverter
	{
		/// <inheritdoc />
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null || values.Length < 2) return 0.0;
			double diameter = (values[0] is double d) ? d : 0.0;
			double innerScale = (values[1] is double s) ? s : 0.45;
			innerScale = (innerScale < 0) ? 0 : (innerScale > 1 ? 1 : innerScale);
			return diameter * innerScale;
		}

		/// <inheritdoc />
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
			throw new NotSupportedException();
	}

}
