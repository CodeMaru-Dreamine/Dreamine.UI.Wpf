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
	/// DateTime to/from TimeSpan converter.
	/// DatePicker와 TimeSpinner를 동시에 ExpiredDate에 바인딩하기 위해 사용.
	/// </summary>
	public class DateTimeToTimeSpanConverter : IValueConverter
	{
		/// <summary> DateTime → TimeSpan 변환 </summary>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime dt)
				return dt.TimeOfDay;

			return TimeSpan.Zero;
		}

		/// <summary> TimeSpan → DateTime 변환 </summary>
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
