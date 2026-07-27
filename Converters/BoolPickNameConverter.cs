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
	/// <para>부울 선택 값에 따라 와이어 이름 또는 EM 이름을 선택합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Selects a wire name or EM name according to a Boolean selector.</para>
	/// \endif
	/// </summary>
	public class BoolPickNameConverter : IMultiValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>첫 번째 값이 참이면 두 번째 값을, 그렇지 않으면 세 번째 값을 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Returns the second value when the first value is true; otherwise, returns the third value.</para>
		/// \endif
		/// </summary>
		/// <param name="values">
		/// \if KO
		/// <para>선택 값, 와이어 이름, EM 이름 순서의 값 배열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Values ordered as selector, wire name, and EM name.</para>
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
		/// <para>선택된 이름이며 값이 없으면 런타임상 <see langword="null"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The selected name, or runtime <see langword="null"/> when absent.</para>
		/// \endif
		/// </returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool isEm = values.Length > 0 && values[0] is bool b && b;
			var wire = values.Length > 1 ? values[1] : null;
			var em = values.Length > 2 ? values[2] : null;

			// 요구사항: IsEmName == true => WireName, false => EmName
			return (isEm ? wire : em) ?? null!;
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
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}
