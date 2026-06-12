using System;
using System.Globalization;
using System.Windows.Data;
using Dreamine.UI.Wpf.Controls;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// @brief LED 위치 계산 컨버터.
	/// @details
	/// ConverterParameter: "OuterLeft" | "OuterTop" | "InnerLeft" | "InnerTop"
	/// MultiBinding 순서:
	/// 0: LedCorner (Corner)
	/// 1: double (ActualWidth 또는 ActualHeight) - Parameter에 따라 의미 달라짐
	/// 2: double (Diameter)
	/// 3: double (EdgeOffset)
	/// 4: double (InnerScale) - Inner* 모드에서만 사용
	/// </summary>
	public sealed class LedPositionConverter : IMultiValueConverter
	{
		/// <inheritdoc />
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			string mode = (parameter as string) ?? "OuterLeft";
			if (values == null || values.Length < 4) return 0.0;

			var corner = values[0] is LedCorner c ? c : LedCorner.TopRight;
			double extent = ToDouble(values[1]); // width or height
			double diameter = ToDouble(values[2]);
			double edgeOffset = ToDouble(values[3]);
			double innerScale = (values.Length > 4) ? ToDouble(values[4]) : 0.45;

			double outerLeft = (corner == LedCorner.TopRight || corner == LedCorner.BottomRight)
				? extent - diameter - edgeOffset
				: edgeOffset;

			double outerTop = (corner == LedCorner.BottomLeft || corner == LedCorner.BottomRight)
				? extent - diameter - edgeOffset
				: edgeOffset;

			if (mode.Equals("OuterLeft", StringComparison.OrdinalIgnoreCase)) return outerLeft;
			if (mode.Equals("OuterTop", StringComparison.OrdinalIgnoreCase)) return outerTop;

			double innerDiameter = diameter * Clamp01(innerScale);
			double offset = (diameter - innerDiameter) / 2.0;

			if (mode.Equals("InnerLeft", StringComparison.OrdinalIgnoreCase)) return outerLeft + offset;
			if (mode.Equals("InnerTop", StringComparison.OrdinalIgnoreCase)) return outerTop + offset;

			return 0.0;
		}

		/// <inheritdoc />
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
			throw new NotSupportedException();

		private static double ToDouble(object v)
		{
			if (v is double d) return d;
			if (v is float f) return f;
			if (v is int i) return i;
			if (v is string s && double.TryParse(s, out var r)) return r;
			return 0.0;
		}

		private static double Clamp01(double v) => (v < 0) ? 0 : (v > 1 ? 1 : v);
	}
}