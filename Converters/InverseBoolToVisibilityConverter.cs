using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts a boolean value to a <see cref="Visibility"/> enum (inverse of <see cref="BoolToVisibilityConverter"/>).
	/// Returns <c>Collapsed</c> when true, and <c>Visible</c> when false or null.
	/// </summary>
	public class InverseBoolToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Converts a boolean value to <see cref="Visibility"/> (inverse).
		/// </summary>
		/// <Param name="value">The source boolean value from the binding.</Param>
		/// <Param name="targetType">The type of the binding target property (expected to be <see cref="Visibility"/>).</Param>
		/// <Param name="parameter">Optional converter parameter (unused).</Param>
		/// <Param name="culture">The culture info used in the converter.</Param>
		/// <returns>
		/// <see cref="Visibility.Collapsed"/> if true; otherwise, <see cref="Visibility.Visible"/>.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (value is bool b && b) ? Visibility.Collapsed : Visibility.Visible;

		/// <summary>
		/// ConvertBack is not supported and will always throw <see cref="NotSupportedException"/>.
		/// </summary>
		/// <Param name="value">The target value to convert back (unused).</Param>
		/// <Param name="targetType">The type to convert to (unused).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use in the converter (unused).</Param>
		/// <returns>This method does not return a value.</returns>
		/// <exception cref="NotSupportedException">Always thrown because reverse conversion is not supported.</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}

}
