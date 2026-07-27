using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>LED의 바깥 지름과 내부 비율로 내부 원의 지름을 계산합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Calculates an LED's inner-circle diameter from its outer diameter and inner scale.</para>
	/// \endif
	/// </summary>
	public sealed class LedInnerDiameterConverter : IMultiValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>내부 비율을 0~1로 제한한 뒤 바깥 지름과 곱합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Clamps the inner scale to zero through one and multiplies it by the outer diameter.</para>
		/// \endif
		/// </summary>
		/// <param name="values">
		/// \if KO
		/// <para>바깥 지름과 내부 비율 순서의 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Values ordered as outer diameter and inner scale.</para>
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
		/// <para>계산된 내부 지름이며 입력이 부족하면 0입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The calculated inner diameter, or zero when input is insufficient.</para>
		/// \endif
		/// </returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null || values.Length < 2) return 0.0;
			double diameter = (values[0] is double d) ? d : 0.0;
			double innerScale = (values[1] is double s) ? s : 0.45;
			innerScale = (innerScale < 0) ? 0 : (innerScale > 1 ? 1 : innerScale);
			return diameter * innerScale;
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
