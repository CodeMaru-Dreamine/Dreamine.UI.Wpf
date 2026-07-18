using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>부울 값과 "1" 또는 "0" 문자열 플래그 사이를 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts between Boolean values and string flags represented by "1" or "0".</para>
	/// \endif
	/// </summary>
	public class BooleanConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>원본 값을 문자열로 변환하여 "1"인지 확인합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts the source value to text and determines whether it equals "1".</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 바인딩 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source binding value.</para>
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
		/// <para>문자열 표현이 "1"이면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when the string representation is "1".</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value?.ToString() == "1";
		}

		/// <summary>
		/// \if KO
		/// <para>부울 값을 "1" 또는 "0" 문자열로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a Boolean value back to a "1" or "0" string.</para>
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
		/// <para>참이면 "1", 거짓이면 "0"입니다.</para>
		/// \endif
		/// \if EN
		/// <para>"1" for true; otherwise, "0".</para>
		/// \endif
		/// </returns>
		/// <exception cref="InvalidCastException">
		/// \if KO
		/// <para><paramref name="value"/>가 부울 값이 아니면 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Thrown when <paramref name="value"/> is not a Boolean value.</para>
		/// \endif
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? "1" : "0";
		}
	}
}
