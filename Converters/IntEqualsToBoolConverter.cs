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
	/// <para>정수 값과 문자열 매개변수의 같음 상태를 양방향으로 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts equality between an integer value and a string parameter in both directions.</para>
	/// \endif
	/// </summary>
	public sealed class IntEqualsToBoolConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>원본 정수가 매개변수로 해석한 정수와 같은지 확인합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Determines whether the source integer equals the integer parsed from the parameter.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 정수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source integer.</para>
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
		/// <para>비교할 정수 문자열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The integer text to compare.</para>
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
		/// <para>값이 같으면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when the values are equal.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int v && parameter is string s && int.TryParse(s, out var p))
				return v == p;
			return false;
		}

		/// <summary>
		/// \if KO
		/// <para>선택 상태가 참일 때 매개변수 정수를 원본으로 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Returns the parameter integer to the source when selection is true.</para>
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
		/// <para>원본으로 반환할 정수 문자열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The integer text to return to the source.</para>
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
		/// <para>해석된 정수 또는 <see cref="Binding.DoNothing"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The parsed integer or <see cref="Binding.DoNothing"/>.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool b && b && parameter is string s && int.TryParse(s, out var p))
				return p;
			// 체크 해제 시 기존 값 유지
			return Binding.DoNothing;
		}
	}
}
