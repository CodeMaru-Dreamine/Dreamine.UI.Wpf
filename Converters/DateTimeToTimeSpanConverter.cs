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
	/// <para><see cref="DateTime"/>의 시간 부분과 <see cref="TimeSpan"/> 사이를 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts between the time-of-day portion of a <see cref="DateTime"/> and a <see cref="TimeSpan"/>.</para>
	/// \endif
	/// </summary>
	public class DateTimeToTimeSpanConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>날짜/시간 값에서 시간 부분을 추출합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Extracts the time-of-day component from a date/time value.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 날짜/시간 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source date/time value.</para>
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
		/// <para>시간 부분이며 입력이 날짜/시간이 아니면 <see cref="TimeSpan.Zero"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The time-of-day value, or <see cref="TimeSpan.Zero"/> when the input is not a date/time.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime dt)
				return dt.TimeOfDay;

			return TimeSpan.Zero;
		}

		/// <summary>
		/// \if KO
		/// <para>오늘 날짜에 대상 시간 간격을 더해 날짜/시간 값으로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a target time span to a date/time by adding it to today's date.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>대상 시간 간격입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target time span.</para>
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
		/// <para>오늘 날짜와 지정 시간이며 입력이 시간 간격이 아니면 현재 시각입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Today's date at the specified time, or the current date and time when the input is not a time span.</para>
		/// \endif
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// \if KO
		/// <para>시간 간격을 오늘 날짜에 더한 결과가 <see cref="DateTime"/> 범위를 벗어나면 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Thrown when adding the time span to today's date exceeds the <see cref="DateTime"/> range.</para>
		/// \endif
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TimeSpan ts)
			{
				// 기존 ExpiredDate.Date 를 가져와서 시간만 교체하고 싶을 경우
				var today = DateTime.Today;
				return today.Add(ts);
			}

			return DateTime.Now;
		}
	}
}
