using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts a <see cref="double"/> value to a formatted string with three decimal places, and vice versa.
	/// Useful for numeric input/display bindings in WPF UIs.
	/// </summary>
	public class DoubleToStringConverter : IValueConverter
	{
		/// <summary>
		/// Converts a <see cref="double"/> to a string formatted with three decimal places.
		/// </summary>
		/// <Param name="value">The source value to convert, expected to be a double.</Param>
		/// <Param name="targetType">The type of the target property (expected to be string).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use for formatting.</Param>
		/// <returns>A string formatted as "0.000". If input is invalid, returns "0.000".</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double doubleValue)
			{
				return doubleValue.ToString("0.000", culture);
			}
			return "0.000";
		}

		/// <summary>
		/// Converts a string back to a <see cref="double"/> value.
		/// </summary>
		/// <Param name="value">The source string to convert back.</Param>
		/// <Param name="targetType">The type to convert to (expected to be double).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use for parsing.</Param>
		/// <returns>The parsed double value, or 0.0 if parsing fails.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string stringValue && double.TryParse(stringValue, out double doubleValue))
			{
				return doubleValue;
			}
			return 0.0;
		}
	}
}
