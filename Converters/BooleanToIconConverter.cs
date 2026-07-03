using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts a boolean value to an icon name string for UI representation.
	/// Useful for dynamic icon switching in data-bound controls (e.g., check status).
	/// </summary>
	public class BooleanToIconConverter : IValueConverter
	{
		/// <summary>
		/// Converts a boolean value to an icon name string.
		/// </summary>
		/// <Param name="value">The source boolean value from the binding.</Param>
		/// <Param name="targetType">The target type of the binding (expected to be string).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture info for the conversion.</Param>
		/// <returns>
		/// "CheckCircle" if true; "Circle" if false or value is null/invalid.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool isChecked)
			{
				return isChecked ? "CheckCircle" : "Circle";
			}
			return "Circle";
		}

		/// <summary>
		/// ConvertBack is not implemented for this converter.
		/// </summary>
		/// <Param name="value">The target value to convert back (unused).</Param>
		/// <Param name="targetType">The type to convert to (unused).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture to use in the converter (unused).</Param>
		/// <exception cref="NotImplementedException">
		/// Always thrown because reverse conversion is not supported.
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
