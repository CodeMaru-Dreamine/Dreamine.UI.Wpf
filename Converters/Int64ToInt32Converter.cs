using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>64비트 정수를 32비트 정수로 변환하고 음수 32비트 정수는 0으로 제한합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts 64-bit integers to 32-bit integers and clamps negative 32-bit integers to zero.</para>
	/// \endif
	/// </summary>
	public class Int64ToInt32Converter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>정수 입력을 32비트 결과로 변환하거나 원본을 그대로 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts integer input to a 32-bit result or returns the source unchanged.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source value.</para>
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
		/// <para>변환된 정수 또는 원본 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The converted integer or original value.</para>
		/// \endif
		/// </returns>
		/// <remarks>
		/// \if KO
		/// <para>64비트 값의 명시적 캐스트는 오버플로 검사 없이 하위 32비트를 사용합니다.</para>
		/// \endif
		/// \if EN
		/// <para>The explicit 64-bit cast uses the lower 32 bits without checked overflow.</para>
		/// \endif
		/// </remarks>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is long longValue)
			{
				return (int)longValue;
			}
			else if (value is int intValue)
			{
				int rtn = intValue < 0 ? 0 : intValue;
				return rtn;
			}
			return value;
		}

		/// <summary>
		/// \if KO
		/// <para>대상 값을 변경하지 않고 원본으로 전달합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Passes the target value back to the source unchanged.</para>
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
		/// <para>입력 값 그대로입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The input value unchanged.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
