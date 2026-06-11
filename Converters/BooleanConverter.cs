using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts between a Boolean value and a string ("1" or "0") representation.
	/// This is primarily used for UI bindings where boolean values are stored as string flags in the backend.
	/// </summary>
	public class BooleanConverter : IValueConverter
	{
		/// <summary>
		/// Converts a string input (typically "1" or "0") to a boolean value.
		/// </summary>
		/// <Param name="value">The value from the source binding, expected to be "1" or "0".</Param>
		/// <Param name="targetType">The type of the binding target property.</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use in the converter.</Param>
		/// <returns>True if the value is "1"; otherwise, false.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value?.ToString() == "1";
		}

		/// <summary>
		/// Converts a boolean value back to its string representation ("1" or "0").
		/// </summary>
		/// <Param name="value">The value produced by the target binding, expected to be a boolean.</Param>
		/// <Param name="targetType">The type to convert to.</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use in the converter.</Param>
		/// <returns>"1" if true; otherwise, "0".</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? "1" : "0";
		}
	}
}
