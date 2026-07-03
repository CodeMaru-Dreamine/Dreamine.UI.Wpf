using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts boolean or long values by inverting the logical State.
	/// - For <see cref="bool"/>: returns the negated value.
	/// - For <see cref="long"/>: returns true if 0, false otherwise.
	/// </summary>
	public class InverseBooleanConverter : IValueConverter
	{
		/// <summary>
		/// Converts a <see cref="bool"/> or <see cref="long"/> value to its inverse representation.
		/// </summary>
		/// <Param name="value">The value to convert. Accepts <see cref="bool"/> or <see cref="long"/> types.</Param>
		/// <Param name="targetType">The target type of the binding (unused).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">Culture information (unused).</Param>
		/// <returns>
		/// - If input is <see cref="long"/>: returns <c>true</c> if 0, otherwise <c>false</c>.<br/>
		/// - If input is <see cref="bool"/>: returns <c>!value</c>.<br/>
		/// - Otherwise: returns <c>false</c>.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is long intValue)
			{
				return intValue == 0;
			}
			else if (value is bool booleanValue)
			{
				return !booleanValue;
			}

			return false;
		}

		/// <summary>
		/// Converts a value back to its logical inverse, or to a numeric representation if targetType is numeric.
		/// </summary>
		/// <Param name="value">The value to convert back. Expected to be a <see cref="bool"/>.</Param>
		/// <Param name="targetType">The target type (e.g., <see cref="long"/>, <see cref="int"/>, or <see cref="bool"/>).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">Culture information (unused).</Param>
		/// <returns>
		/// - If targetType is numeric: returns 0 if value is <c>true</c>, 1 otherwise.<br/>
		/// - If targetType is <see cref="bool"/>: returns logical inverse.<br/>
		/// - Otherwise: returns 0.
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool booleanValue)
			{
				return !booleanValue;
			}
			else if (targetType == typeof(long) || targetType == typeof(int))
			{
				return (bool)value ? 0L : 1L;
			}

			return 0L;
		}
	}
}
