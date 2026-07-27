using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para><see cref="double"/> 값과 소수점 이하 세 자리 문자열 사이를 양방향 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts between <see cref="double"/> values and strings formatted to three decimal places.</para>
	/// \endif
	/// </summary>
	public class DoubleToStringConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>실수 값을 지정 문화권의 소수점 이하 세 자리 문자열로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a floating-point value to a culture-aware string with three decimal places.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 실수 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source floating-point value.</para>
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
		/// <para>사용하지 않는 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused parameter.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>형식화에 사용할 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The culture used for formatting.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>형식화된 문자열이며 입력이 실수가 아니면 "0.000"입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The formatted text, or "0.000" when the input is not a double.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double doubleValue)
			{
				return doubleValue.ToString("0.000", culture);
			}
			return "0.000";
		}

		/// <summary>
		/// \if KO
		/// <para>문자열을 현재 런타임 기본 해석 규칙으로 실수로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts text to a floating-point value using the runtime's default parsing rules.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>해석할 대상 문자열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target text to parse.</para>
		/// \endif
		/// </param>
		/// <param name="targetType">
		/// \if KO
		/// <para>원본 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source type.</para>
		/// \endif
		/// </param>
		/// <param name="parameter">
		/// \if KO
		/// <para>사용하지 않는 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused parameter.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>현재 구현에서는 사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>A culture not used by the current implementation.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>해석된 실수이며 실패하면 0입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The parsed value, or zero when parsing fails.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string stringValue && double.TryParse(stringValue, out double doubleValue))
			{
				return doubleValue;
			}
			return 0.0;
		}
	}
}
