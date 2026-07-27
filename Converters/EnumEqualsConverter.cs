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
	/// <para>열거형 값과 변환기 매개변수의 같음 상태를 양방향으로 연결합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Binds equality between an enumeration value and converter parameter in both directions.</para>
	/// \endif
	/// </summary>
	public sealed class EnumEqualsConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>원본 값과 매개변수가 같은지 확인합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Determines whether the source value equals the parameter.</para>
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
		/// <para>비교할 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The value to compare.</para>
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
		/// <para>두 값이 null이 아니고 같으면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when both values are non-null and equal.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null) return false;
			return value.Equals(parameter);
		}

		/// <summary>
		/// \if KO
		/// <para>대상 값이 참일 때 매개변수를 원본 값으로 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Returns the parameter to the source when the target value is true.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>대상 선택 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target selection value.</para>
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
		/// <para>원본으로 반환할 열거형 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The enumeration value returned to the source.</para>
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
		/// <para>선택된 열거형 값 또는 <see cref="Binding.DoNothing"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The selected enumeration value or <see cref="Binding.DoNothing"/>.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null) return Binding.DoNothing;
			// true일 때만 enum으로 반영, false면 변경하지 않음
			return value is bool b && b ? parameter : Binding.DoNothing;
		}
	}
}
