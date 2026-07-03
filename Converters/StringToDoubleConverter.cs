using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// @brief A simple converter that transforms a <c>string</c> to a <c>double</c> and vice versa.
	/// @details
	/// - <b>Convert:</b> string → double<br/>
	/// - <b>ConvertBack:</b> double → string (supports custom formatting)<br/>
	/// - By default, parsing uses <see cref="CultureInfo.InvariantCulture"/>. 
	///   Set <see cref="UseInvariantCulture"/> to false to use the binding's provided culture.
	/// </summary>
	public sealed class StringToDoubleConverter : IValueConverter
	{
		/// <summary>
		/// The value to return when parsing fails.
		/// </summary>
		public double Fallback { get; set; } = 0.0;

		/// <summary>
		/// The format string to use when converting a double to string in <see cref="ConvertBack"/>.
		/// If empty or null, "G" (general format) is used.
		/// Examples: "0.###", "0.000"
		/// </summary>
		public string? Format { get; set; }

		/// <summary>
		/// Determines whether to always use <see cref="CultureInfo.InvariantCulture"/>.
		/// If true, a '.' is always used as the decimal separator.
		/// </summary>
		public bool UseInvariantCulture { get; set; } = true;

		/// <summary>
		/// Converts a string to a <c>double</c>.
		/// </summary>
		/// <Param name="value">The input string value.</Param>
		/// <Param name="targetType">The target type (ignored).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture provided by the binding.</Param>
		/// <returns>
		/// The parsed double value, or <see cref="Fallback"/> if parsing fails.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var ci = UseInvariantCulture ? CultureInfo.InvariantCulture : (culture ?? CultureInfo.CurrentCulture);

			if (value is string s)
			{
				if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowLeadingSign, ci, out var d))
					return d;

				// Common culture confusion fix: replace ',' with '.' if using invariant culture
				if (UseInvariantCulture && double.TryParse(s.Replace(',', '.'), NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out d))
					return d;
			}
			else if (value is IConvertible c)
			{
				try { return System.Convert.ToDouble(c, ci); }
				catch { /* ignored */ }
			}

			return Fallback;
		}

		/// <summary>
		/// Converts a <c>double</c> to a formatted string.
		/// </summary>
		/// <Param name="value">The double value to convert.</Param>
		/// <Param name="targetType">The target type (ignored).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">The culture provided by the binding.</Param>
		/// <returns>
		/// The formatted string representation of the double value.
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var ci = UseInvariantCulture ? CultureInfo.InvariantCulture : (culture ?? CultureInfo.CurrentCulture);

			double d = value switch
			{
				double v => v,
				float v => v,
				decimal v => (double)v,
				string s when double.TryParse(s, NumberStyles.Float | NumberStyles.AllowLeadingSign, ci, out var vv) => vv,
				_ => Fallback
			};

			var fmt = string.IsNullOrWhiteSpace(Format) ? "G" : Format!;
			return d.ToString(fmt, ci);
		}
	}
}
