using System;
using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para><see cref="TimeSpan"/>과 시간·분 편집 문자열 사이를 양방향 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts between <see cref="TimeSpan"/> values and editable hour-minute text.</para>
	/// \endif
	/// </summary>
	/// <remarks>
	/// \if KO
	/// <para>H:MM, 짧은 시간 숫자, 또는 마지막 두 자리를 분으로 보는 HMM/HHMM 입력을 허용합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Accepts H:MM, short hour-only numbers, or HMM/HHMM input whose final two digits represent minutes.</para>
	/// \endif
	/// </remarks>
	public sealed class TimeSpanHmEditConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>시간 간격을 총 시간과 두 자리 분으로 구성된 문자열로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a time span to text containing total hours and two-digit minutes.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 시간 간격입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source time span.</para>
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
		/// <para>H:MM 문자열이며 입력이 시간 간격이 아니면 "0:00"입니다.</para>
		/// \endif
		/// \if EN
		/// <para>H:MM text, or "0:00" when the input is not a time span.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TimeSpan ts)
				return $"{(int)ts.TotalHours}:{ts.Minutes:00}";
			return "0:00";
		}

		/// <summary>
		/// \if KO
		/// <para>지원되는 시간·분 문자열을 음수가 아닌 시간 간격으로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts supported hour-minute text to a nonnegative time span.</para>
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
		/// <para>사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>해석된 시간 간격이며 유효하지 않으면 <see cref="TimeSpan.Zero"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The parsed time span, or <see cref="TimeSpan.Zero"/> when invalid.</para>
		/// \endif
		/// </returns>
		/// <exception cref="OverflowException">
		/// \if KO
		/// <para>해석된 시간 값이 <see cref="TimeSpan"/> 범위를 벗어나면 발생할 수 있습니다.</para>
		/// \endif
		/// \if EN
		/// <para>May be thrown when the parsed hour value exceeds the <see cref="TimeSpan"/> range.</para>
		/// \endif
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is not string s || string.IsNullOrWhiteSpace(s))
				return TimeSpan.Zero;

			s = s.Trim();

			// 1) "H:MM" 정식 표기
			if (s.Contains(':'))
			{
				var parts = s.Split(':');
				if (parts.Length == 2 &&
					int.TryParse(parts[0], out var h) &&
					int.TryParse(parts[1], out var m) &&
					m >= 0 && m < 60)
					return TimeSpan.FromHours(Math.Max(0, h)) + TimeSpan.FromMinutes(m);
				return TimeSpan.Zero;
			}

			// 2) 순수 숫자: H, HMM, HHMM 해석
			if (int.TryParse(s, out var num))
			{
				if (num < 0) num = 0;

				if (s.Length <= 2) // "H" 또는 "HH" → 시간만
				{
					return TimeSpan.FromHours(num);
				}
				else
				{
					// 마지막 두 자리는 분으로 간주
					var minutes = num % 100;
					var hours = num / 100;
					if (minutes >= 60) minutes = minutes % 60; // 비정상 값 보정
					return TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes);
				}
			}

			return TimeSpan.Zero;
		}
	}
}
