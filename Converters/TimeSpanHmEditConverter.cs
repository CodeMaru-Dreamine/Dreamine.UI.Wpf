using System;
using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// @file TimeSpanHmEditConverter.cs
	/// @brief TimeSpan ↔ "H:MM" 양방향 변환기.
	/// @details
	///  - Convert: 100:05 형태(H:MM)로 출력.
	///  - ConvertBack 입력 허용:
	///     * "H:MM" 정식 표기
	///     * "HMM" 또는 "HHMM" (마지막 두 자리를 분으로 간주; 예: 123 → 1:23, 0600 → 6:00)
	///     * "H" 또는 "HHH" (순수 시간; 예: 6 → 6:00)
	///  - 잘못된 입력은 0:00 반환.
	/// </summary>
	public sealed class TimeSpanHmEditConverter : IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TimeSpan ts)
				return $"{(int)ts.TotalHours}:{ts.Minutes:00}";
			return "0:00";
		}

		/// <inheritdoc />
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
