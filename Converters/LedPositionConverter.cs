using System;
using System.Globalization;
using System.Windows.Data;
using Dreamine.UI.Wpf.Controls;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>선택한 모서리, 컨테이너 크기, 지름 및 여백으로 LED의 바깥쪽 또는 안쪽 좌표를 계산합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Calculates an LED's outer or inner coordinate from a corner, container extent, diameter, and edge offset.</para>
	/// \endif
	/// </summary>
	/// <remarks>
	/// \if KO
	/// <para>매개변수는 OuterLeft, OuterTop, InnerLeft 또는 InnerTop입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The parameter is OuterLeft, OuterTop, InnerLeft, or InnerTop.</para>
	/// \endif
	/// </remarks>
	public sealed class LedPositionConverter : IMultiValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>요청한 좌표 모드에 맞는 LED 위치를 계산합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Calculates the LED position for the requested coordinate mode.</para>
		/// \endif
		/// </summary>
		/// <param name="values">
		/// \if KO
		/// <para>모서리, 범위, 지름, 가장자리 여백과 선택적 내부 비율 순서의 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Values ordered as corner, extent, diameter, edge offset, and optional inner scale.</para>
		/// \endif
		/// </param>
		/// <param name="targetType">
		/// \if KO
		/// <para>대상 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target type.</para>
		/// \endif
		/// </param>
		/// <param name="parameter">
		/// \if KO
		/// <para>계산할 좌표 모드입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The coordinate mode to calculate.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>계산된 좌표이며 모드나 입력이 유효하지 않으면 0입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The calculated coordinate, or zero for invalid mode or input.</para>
		/// \endif
		/// </returns>
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

		/// <summary>
		/// \if KO
		/// <para>다중 바인딩 역변환은 지원하지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>Multibinding reverse conversion is not supported.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>대상 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target value.</para>
		/// \endif
		/// </param>
		/// <param name="targetTypes">
		/// \if KO
		/// <para>원본 형식 배열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source-type array.</para>
		/// \endif
		/// </param>
		/// <param name="parameter">
		/// \if KO
		/// <para>변환기 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The converter parameter.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>변환 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The conversion culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>정상적으로 반환되지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>This method does not return normally.</para>
		/// \endif
		/// </returns>
		/// <exception cref="NotSupportedException">
		/// \if KO
		/// <para>호출할 때 항상 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Always thrown when called.</para>
		/// \endif
		/// </exception>
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
			throw new NotSupportedException();

		/// <summary>
		/// \if KO
		/// <para>지원되는 숫자 또는 문자열 값을 실수로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a supported numeric or string value to a floating-point number.</para>
		/// \endif
		/// </summary>
		/// <param name="v">
		/// \if KO
		/// <para>변환할 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The value to convert.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>변환된 값이며 실패하면 0입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The converted value, or zero on failure.</para>
		/// \endif
		/// </returns>
		private static double ToDouble(object v)
		{
			if (v is double d) return d;
			if (v is float f) return f;
			if (v is int i) return i;
			if (v is string s && double.TryParse(s, out var r)) return r;
			return 0.0;
		}

		/// <summary>
		/// \if KO
		/// <para>값을 0 이상 1 이하로 제한합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Clamps a value to the inclusive zero-to-one range.</para>
		/// \endif
		/// </summary>
		/// <param name="v">
		/// \if KO
		/// <para>제한할 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The value to clamp.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>제한된 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The clamped value.</para>
		/// \endif
		/// </returns>
		private static double Clamp01(double v) => (v < 0) ? 0 : (v > 1 ? 1 : v);
	}
}
