using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	public class DoubleEqualsConverter : IValueConverter
	{
		/// <summary>표시 포맷. 예: "0.000"</summary>
		public string Format { get; set; } = "0.000";

		/// <summary>허용 최소값(없으면 무제한)</summary>
		public double? Min { get; set; }

		/// <summary>허용 최대값(없으면 무제한)</summary>
		public double? Max { get; set; }

		/// <summary>빈 문자열 허용 여부. true면 빈 문자열에서 원본 갱신을 하지 않습니다.</summary>
		public bool AllowEmpty { get; set; } = true;

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

	public class DoubleEqualsBoolConverter : IValueConverter
	{
		public double Epsilon { get; set; } = 1e-6;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double v && TryParseParam(parameter, out var p))
				return Math.Abs(v - p) < Epsilon;
			return false;
		}

		// 체크될 때만 MoveAmount 업데이트, 해제될 때는 원본 유지
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool b && b && TryParseParam(parameter, out var p))
				return p;
			return Binding.DoNothing;
		}

		private static bool TryParseParam(object parameter, out double result)
		{
			var s = parameter?.ToString() ?? "";
			// 파라미터는 일반적으로 상수 문자열이므로 InvariantCulture로 파싱
			return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
		}
	}


}
