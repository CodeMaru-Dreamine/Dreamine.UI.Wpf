using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// Converts a value to <see cref="Visibility"/> based on its nullability.
	/// - If the value is <c>null</c>, returns <c>Collapsed</c>.
	/// - If not null, returns <c>Visible</c>.
	/// Supports optional inversion.
	/// </summary>
	public class NullToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// If set to true, the result is inverted.
		/// - <c>null</c> → <c>Visible</c>
		/// - not <c>null</c> → <c>Collapsed</c>
		/// </summary>
		public bool Inverse { get; set; } = false;

		/// <summary>
		/// Converts a value to <see cref="Visibility"/> based on whether it is null.
		/// </summary>
		/// <Param name="value">The bound value to check for null.</Param>
		/// <Param name="targetType">The target binding type (ignored).</Param>
		/// <Param name="parameter">Optional parameter (ignored).</Param>
		/// <Param name="culture">Culture information.</Param>
		/// <returns>
		/// <see cref="Visibility.Visible"/> if value is not null (or null if Inverse=true); otherwise, <see cref="Visibility.Collapsed"/>.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool visible = value != null;
			if (Inverse)
				visible = !visible;

			return visible ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>
		/// Not supported. Reverse conversion is not implemented.
		/// </summary>
		/// <Param name="value">The target value (unused).</Param>
		/// <Param name="targetType">The target type (unused).</Param>
		/// <Param name="parameter">Optional parameter (unused).</Param>
		/// <Param name="culture">Culture information (unused).</Param>
		/// <returns>This method does not return.</returns>
		/// <exception cref="NotSupportedException">Always thrown.</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("NullToVisibilityConverter does not support ConvertBack.");
		}
	}
}
