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
	/// <para>LED의 켜짐 상태에 따라 켜짐 또는 꺼짐 브러시를 선택합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Selects an on or off brush according to an LED's state.</para>
	/// \endif
	/// </summary>
	public sealed class LedBrushConverter : IMultiValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>첫 번째 부울 상태에 따라 두 번째 또는 세 번째 브러시 값을 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Returns the second or third brush value according to the first Boolean state.</para>
		/// \endif
		/// </summary>
		/// <param name="values">
		/// \if KO
		/// <para>켜짐 상태, 켜짐 브러시, 꺼짐 브러시 순서의 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Values ordered as on state, on brush, and off brush.</para>
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
		/// <para>선택된 브러시이며 없으면 런타임상 null입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The selected brush, or runtime null when absent.</para>
		/// \endif
		/// </returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool isOn = (values != null && values.Length > 0 && values[0] is bool b) && b;
			var onBrush = (values != null && values.Length > 1) ? values[1] : null;
			var offBrush = (values != null && values.Length > 2) ? values[2] : null;
			return (isOn ? onBrush : offBrush) ?? null!;
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
	}
}
