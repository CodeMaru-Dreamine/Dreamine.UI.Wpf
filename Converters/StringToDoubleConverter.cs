using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>문자열과 <see cref="double"/> 값 사이를 선택한 문화권과 형식으로 양방향 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts between strings and <see cref="double"/> values using a selected culture and format.</para>
	/// \endif
	/// </summary>
	public sealed class StringToDoubleConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>숫자 해석에 실패할 때 반환할 값을 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the value returned when numeric parsing fails.</para>
		/// \endif
		/// </summary>
		public double Fallback { get; set; } = 0.0;

		/// <summary>
		/// \if KO
		/// <para>역변환 문자열에 사용할 선택적 숫자 형식을 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the optional numeric format used for reverse-conversion text.</para>
		/// \endif
		/// </summary>
		public string? Format { get; set; }

		/// <summary>
		/// \if KO
		/// <para>바인딩 문화권 대신 불변 문화권을 사용할지 여부를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets whether invariant culture is used instead of the binding culture.</para>
		/// \endif
		/// </summary>
		public bool UseInvariantCulture { get; set; } = true;

		/// <summary>
		/// \if KO
		/// <para>문자열 또는 변환 가능한 원본 값을 실수로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts text or another convertible source value to a floating-point number.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>변환할 원본 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source value to convert.</para>
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
		/// <para>불변 문화권을 사용하지 않을 때의 변환 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The conversion culture used when invariant culture is disabled.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>해석된 실수이며 실패하면 <see cref="Fallback"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The parsed value, or <see cref="Fallback"/> on failure.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var ci = UseInvariantCulture ? CultureInfo.InvariantCulture : (culture ?? CultureInfo.CurrentCulture);

			if (value is string s)
			{
				if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowLeadingSign, ci, out var d))
					return d;

				// Common culture confusion fix: replace ',' with '.' if using invariant culture
				if (UseInvariantCulture && double.TryParse(s.Replace(',', '.'), NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out d))
					return d;
			}
			else if (value is IConvertible c)
			{
				try { return System.Convert.ToDouble(c, ci); }
				catch { /* ignored */ }
			}

			return Fallback;
		}

		/// <summary>
		/// \if KO
		/// <para>대상 숫자 값을 구성된 형식의 문자열로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a target numeric value to text using the configured format.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>형식화할 대상 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target value to format.</para>
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
		/// <para>불변 문화권을 사용하지 않을 때의 변환 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The conversion culture used when invariant culture is disabled.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>형식화된 숫자 문자열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The formatted numeric text.</para>
		/// \endif
		/// </returns>
		/// <exception cref="FormatException">
		/// \if KO
		/// <para><see cref="Format"/>이 유효한 숫자 형식이 아니면 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Thrown when <see cref="Format"/> is not a valid numeric format.</para>
		/// \endif
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var ci = UseInvariantCulture ? CultureInfo.InvariantCulture : (culture ?? CultureInfo.CurrentCulture);

			double d = value switch
			{
				double v => v,
				float v => v,
				decimal v => (double)v,
				string s when double.TryParse(s, NumberStyles.Float | NumberStyles.AllowLeadingSign, ci, out var vv) => vv,
				_ => Fallback
			};

			var fmt = string.IsNullOrWhiteSpace(Format) ? "G" : Format!;
			return d.ToString(fmt, ci);
		}
	}
}
