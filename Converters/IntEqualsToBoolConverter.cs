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
	/// 정수 값이 ConverterParameter와 같으면 true를 반환하는 컨버터.
	/// 라디오버튼 IsChecked ↔ int 속성 바인딩에 사용.
	/// </summary>
	public sealed class IntEqualsToBoolConverter : IValueConverter
	{
		/// <inheritdoc/>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int v && parameter is string s && int.TryParse(s, out var p))
				return v == p;
			return false;
		}

		/// <inheritdoc/>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool b && b && parameter is string s && int.TryParse(s, out var p))
				return p;
			// 체크 해제 시 기존 값 유지
			return Binding.DoNothing;
		}
	}
}
