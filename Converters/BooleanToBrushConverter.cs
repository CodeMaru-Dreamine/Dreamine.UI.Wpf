using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts a boolean value to a Brush based on its State.
	/// Commonly used in UI elements to indicate status (e.g., enabled/disabled, active/inactive).
	/// </summary>
	public class BooleanToBrushConverter : IValueConverter
	{
		/// <summary>
		/// Gets or sets the Brush to use when the boolean value is true.
		/// </summary>
		public Brush OnBrush { get; set; } = Brushes.Lime;

		/// <summary>
		/// Gets or sets the Brush to use when the boolean value is false.
		/// </summary>
		public Brush OffBrush { get; set; } = Brushes.Gray;

		/// <summary>
		/// Converts a boolean value to the corresponding Brush.
		/// </summary>
		/// <Param name="value">The boolean value from the source binding.</Param>
		/// <Param name="targetType">The target type (expected to be Brush).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use in the converter.</Param>
		/// <returns>OnBrush if true; otherwise, OffBrush.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = false;
			if (value is bool b)
				flag = b;
			return flag ? OnBrush : OffBrush;
		}

		/// <summary>
		/// ConvertBack is not supported for this converter.
		/// </summary>
		/// <Param name="value">The target value to convert back (unused).</Param>
		/// <Param name="targetType">The type to convert to (unused).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use in the converter (unused).</Param>
		/// <returns>Binding.DoNothing to indicate conversion is not supported.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}

	public class BrushOrUnsetConverter : IValueConverter
	{
		// ON일 때 쓸 색 (기본값: 기존 민트)
		public Brush? OnBrush { get; set; } = new BrushConverter().ConvertFromString("#A2DED0") as Brush ?? Brushes.Transparent;

		// OFF일 때 쓸 색 (null이면 기본색으로 복귀 = UnsetValue)
		public Brush? OffBrush { get; set; } = null;

		// false면 OffBrush를 사용, true면 기본색 복귀(UnsetValue)
		public bool UseUnsetWhenFalse { get; set; } = true;

		public object Convert(object v, Type t, object p, CultureInfo c)
		{
			bool isOn = v is bool b && b;

			if (isOn)
				return OnBrush ?? DependencyProperty.UnsetValue;

			if (!UseUnsetWhenFalse && OffBrush != null)
				return OffBrush;

			// 기본색(스타일/템플릿 기본값)로 복귀
			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object v, Type t, object p, CultureInfo c) => Binding.DoNothing;
	}
}
