using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts a <see cref="long"/> (Int64) value to an <see cref="int"/> (Int32) value for WPF binding scenarios.
	/// Handles negative integers by returning 0, and passes through other types unchanged.
	/// </summary>
	public class Int64ToInt32Converter : IValueConverter
	{
		/// <summary>
		/// Converts an Int64 or Int32 value to an Int32 value.
		/// </summary>
		/// <Param name="value">The input value, expected to be of type <see cref="long"/> or <see cref="int"/>.</Param>
		/// <Param name="targetType">The target binding type (unused).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture info (unused).</Param>
		/// <returns>
		/// If the value is a long, returns its int cast.<br/>
		/// If the value is an int and negative, returns 0.<br/>
		/// Otherwise, returns the original value unchanged.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is long longValue)
			{
				return (int)longValue;
			}
			else if (value is int intValue)
			{
				int rtn = intValue < 0 ? 0 : intValue;
				return rtn;
			}
			return value;
		}

		/// <summary>
		/// Passes the input value back without modification.
		/// No transformation is applied during reverse conversion.
		/// </summary>
		/// <Param name="value">The value to convert back.</Param>
		/// <Param name="targetType">The type to convert to (unused).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture info (unused).</Param>
		/// <returns>The original value as-is.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
