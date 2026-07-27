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
	/// \if KO
	/// <para>입력 데이터 표현 형식을 지정합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Specifies an input-data representation format.</para>
	/// \endif
	/// </summary>
	public enum InputFormat
	{
		/// <summary>
		/// \if KO
		/// <para>ASCII 텍스트 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>ASCII text format.</para>
		/// \endif
		/// </summary>
		ASCII,
		/// <summary>
		/// \if KO
		/// <para>16진 바이트 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Hexadecimal byte format.</para>
		/// \endif
		/// </summary>
		HEX
	}

	/// <summary>
	/// \if KO
	/// <para>입력 형식에 맞는 사용 예시 문자열을 제공합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Provides an example string appropriate for an input format.</para>
	/// \endif
	/// </summary>
	public class FormatToExampleConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>ASCII 또는 HEX 입력 형식에 대응하는 예시를 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Returns an example for ASCII or hexadecimal input format.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 입력 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source input format.</para>
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
		/// <para>형식 예시이며 입력이 지원되지 않으면 빈 문자열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>A format example, or an empty string for unsupported input.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is InputFormat fmt)
			{
				return fmt == InputFormat.ASCII
					? "예시: \\s123\\e\\n\\,실제텍스트"
					: "예시: 02 32 33 34 03,32";
			}
			return "";
		}

		/// <summary>
		/// \if KO
		/// <para>역변환은 지원하지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>Reverse conversion is not supported.</para>
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
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}
