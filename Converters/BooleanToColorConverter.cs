using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>부울 값을 구성된 켜짐 또는 꺼짐 색으로 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts a Boolean value to configured on or off colors.</para>
	/// \endif
	/// </summary>
	public class BooleanToColorConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>참일 때 반환할 색을 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the color returned for true.</para>
		/// \endif
		/// </summary>
		public Color OnColor { get; set; } = Colors.Orchid;
		/// <summary>
		/// \if KO
		/// <para>거짓일 때 반환할 색을 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the color returned for false.</para>
		/// \endif
		/// </summary>
		public Color OffColor { get; set; } = Colors.Yellow;

		/// <summary>
		/// \if KO
		/// <para>부울 원본 값을 상태 색으로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a Boolean source value to its state color.</para>
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
		/// <para>참이면 <see cref="OnColor"/>, 아니면 <see cref="OffColor"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see cref="OnColor"/> for true; otherwise, <see cref="OffColor"/>.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (value is bool b && b) ? OnColor : OffColor;

		/// <summary>
		/// \if KO
		/// <para>역변환을 수행하지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>Does not perform reverse conversion.</para>
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
		/// <para>항상 <see cref="Binding.DoNothing"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Always <see cref="Binding.DoNothing"/>.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}
}
