using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>부울 또는 64비트 정수의 논리 상태를 반전합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Inverts the logical state represented by Boolean or 64-bit integer values.</para>
	/// \endif
	/// </summary>
	public class InverseBooleanConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>부울은 반전하고 64비트 정수는 0인지 여부를 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Negates Boolean input and tests 64-bit integer input for zero.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 부울 또는 64비트 정수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source Boolean or 64-bit integer.</para>
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
		/// <para>사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>반전된 논리 상태이며 지원하지 않는 입력은 거짓입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The inverted logical state, or false for unsupported input.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is long intValue)
			{
				return intValue == 0;
			}
			else if (value is bool booleanValue)
			{
				return !booleanValue;
			}

			return false;
		}

		/// <summary>
		/// \if KO
		/// <para>부울 값을 반전하거나 숫자 원본 형식에 대해 0 또는 1의 64비트 정수를 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Negates a Boolean value or returns a 64-bit zero-or-one value for numeric source types.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>대상 부울 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target Boolean value.</para>
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
		/// <para>사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>반전된 부울 또는 0/1 정수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The negated Boolean or a zero/one integer.</para>
		/// \endif
		/// </returns>
		/// <exception cref="InvalidCastException">
		/// \if KO
		/// <para>숫자 원본 형식인데 <paramref name="value"/>가 부울이 아니면 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Thrown when the source type is numeric but <paramref name="value"/> is not Boolean.</para>
		/// \endif
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool booleanValue)
			{
				return !booleanValue;
			}
			else if (targetType == typeof(long) || targetType == typeof(int))
			{
				return (bool)value ? 0L : 1L;
			}

			return 0L;
		}
	}
}
