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
	/// <para>실수 값을 편집용 문자열로 형식화하고 입력 문자열을 선택적 범위 안의 실수로 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Formats floating-point values as editable text and converts text back to an optionally bounded value.</para>
	/// \endif
	/// </summary>
	public class DoubleEqualsConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>실수 표시 형식을 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the numeric display format.</para>
		/// \endif
		/// </summary>
		public string Format { get; set; } = "0.000";

		/// <summary>
		/// \if KO
		/// <para>선택적인 최소 허용값을 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the optional minimum permitted value.</para>
		/// \endif
		/// </summary>
		public double? Min { get; set; }

		/// <summary>
		/// \if KO
		/// <para>선택적인 최대 허용값을 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the optional maximum permitted value.</para>
		/// \endif
		/// </summary>
		public double? Max { get; set; }

		/// <summary>
		/// \if KO
		/// <para>빈 문자열을 허용하고 원본 갱신을 건너뛸지 여부를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets whether empty text is allowed and leaves the source unchanged.</para>
		/// \endif
		/// </summary>
		public bool AllowEmpty { get; set; } = true;

		/// <summary>
		/// \if KO
		/// <para>입력 숫자를 구성된 형식과 문화권을 사용하여 문자열로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts an input number to text using the configured format and culture.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>형식화할 원본 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source value to format.</para>
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
		/// <para>숫자 형식에 사용할 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The culture used for numeric formatting.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>형식화된 문자열이며 유효하지 않은 값은 빈 문자열 또는 형식화된 0입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Formatted text; invalid values yield empty text or formatted zero.</para>
		/// \endif
		/// </returns>
		/// <exception cref="FormatException">
		/// \if KO
		/// <para><see cref="Format"/>이 숫자 형식으로 유효하지 않으면 일부 입력에서 발생할 수 있습니다.</para>
		/// \endif
		/// \if EN
		/// <para>May be thrown for some inputs when <see cref="Format"/> is not a valid numeric format.</para>
		/// \endif
		/// </exception>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null) return AllowEmpty ? string.Empty : (0d).ToString(Format, culture);
			if (value is double d)
			{
				if (double.IsNaN(d) || double.IsInfinity(d))
					return AllowEmpty ? string.Empty : (0d).ToString(Format, culture);
				return d.ToString(Format, culture);
			}
			// 그 외 수치형
			if (value is IFormattable f)
				return f.ToString(Format, culture);

			// 마지막 보루: 변환 시도
			try
			{
				var dv = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
				return dv.ToString(Format, culture);
			}
			catch
			{
				return AllowEmpty ? string.Empty : (0d).ToString(Format, culture);
			}
		}

		/// <summary>
		/// \if KO
		/// <para>편집 문자열을 문화권 기반 실수로 해석하고 구성된 최소·최대 범위로 제한합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Parses editable text as a culture-aware floating-point value and clamps it to configured bounds.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>해석할 대상 문자열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target text to parse.</para>
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
		/// <para>숫자 해석에 사용할 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The culture used for numeric parsing.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>범위가 적용된 실수이며 입력 중간 상태나 오류에서는 <see cref="Binding.DoNothing"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The bounded value, or <see cref="Binding.DoNothing"/> for incomplete or invalid input.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var s = value as string ?? string.Empty;
			s = s.Trim();

			// 빈 문자열 → 원본 유지(또는 0으로 초기화하고 싶으면 여기서 0d 반환)
			if (string.IsNullOrEmpty(s))
				return AllowEmpty ? Binding.DoNothing : 0d;

			// 사용자가 아직 타이핑 중인 중간 형태면 원본 보존
			var dec = culture.NumberFormat.NumberDecimalSeparator;
			if (s == "-" || s.EndsWith(dec, StringComparison.Ordinal))
				return Binding.DoNothing;

			// 문화권 기반 파싱
			if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, culture, out var d))
			{
				if (Min.HasValue && d < Min.Value) d = Min.Value;
				if (Max.HasValue && d > Max.Value) d = Max.Value;
				return d;
			}

			// 파싱 실패 → 원본 값 보존
			return Binding.DoNothing;
		}
	}

	/// <summary>
	/// \if KO
	/// <para>실수 값이 매개변수와 허용 오차 이내에서 같은지 비교하고 선택된 값을 역변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Compares a floating-point value with a parameter within tolerance and converts a selected value back.</para>
	/// \endif
	/// </summary>
	public class DoubleEqualsBoolConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>같음 비교에 사용할 절대 허용 오차를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the absolute tolerance used for equality comparison.</para>
		/// \endif
		/// </summary>
		public double Epsilon { get; set; } = 1e-6;

		/// <summary>
		/// \if KO
		/// <para>실수 원본과 숫자 매개변수의 차이가 허용 오차보다 작은지 확인합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Determines whether the difference between a source value and numeric parameter is below the tolerance.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>비교할 실수 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The floating-point value to compare.</para>
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
		/// <para>비교할 숫자 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The numeric comparison parameter.</para>
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
		/// <para>두 값의 차이가 허용 오차보다 작으면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when the difference is below the tolerance.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double v && TryParseParam(parameter, out var p))
				return Math.Abs(v - p) < Epsilon;
			return false;
		}

		/// <summary>
		/// \if KO
		/// <para>대상 값이 참일 때만 숫자 매개변수를 원본 값으로 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Returns the numeric parameter to the source only when the target value is true.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>선택 상태를 나타내는 대상 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target value representing selection state.</para>
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
		/// <para>원본으로 반환할 숫자입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The numeric value to return to the source.</para>
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
		/// <para>선택된 숫자이거나 <see cref="Binding.DoNothing"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The selected numeric value or <see cref="Binding.DoNothing"/>.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool b && b && TryParseParam(parameter, out var p))
				return p;
			return Binding.DoNothing;
		}

		/// <summary>
		/// \if KO
		/// <para>변환기 매개변수를 불변 문화권 실수로 해석합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Parses a converter parameter as an invariant-culture floating-point value.</para>
		/// \endif
		/// </summary>
		/// <param name="parameter">
		/// \if KO
		/// <para>해석할 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The parameter to parse.</para>
		/// \endif
		/// </param>
		/// <param name="result">
		/// \if KO
		/// <para>해석된 실수를 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Receives the parsed value.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>해석에 성공하면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when parsing succeeds.</para>
		/// \endif
		/// </returns>
		private static bool TryParseParam(object parameter, out double result)
		{
			var s = parameter?.ToString() ?? "";
			// 파라미터는 일반적으로 상수 문자열이므로 InvariantCulture로 파싱
			return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
		}
	}


}
