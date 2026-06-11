using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \brief Converts between <see cref="bool"/> and <see cref="Visibility"/> with optional inversion and Hidden support.
	/// \details
	/// This converter is designed to be compatible with common "BoolToVisibilityConverter" variants used in projects.
	/// It supports:
	/// - Inverse conversion (true->Collapsed/Hidden, false->Visible)
	/// - Hidden instead of Collapsed when false
	/// - ConvertBack (Visibility -> bool)
	///
	/// Usage patterns:
	/// 1) Property-based options:
	///    <code>
	///    &lt;converters:BoolToVisibilityConverter Inverse="True" UseHidden="True"/&gt;
	///    </code>
	/// 2) Parameter-based options:
	///    <code>
	///    Visibility="{Binding IsOn, Converter={StaticResource BoolToVis}, ConverterParameter=Inverse}"
	///    Visibility="{Binding IsOn, Converter={StaticResource BoolToVis}, ConverterParameter=Hidden}"
	///    Visibility="{Binding IsOn, Converter={StaticResource BoolToVis}, ConverterParameter=Inverse,Hidden}"
	///    </code>
	/// </details>
	/// </summary>
	public sealed class BoolToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// \brief When true, inverts the meaning of the boolean value.
		/// \details
		/// If <see cref="Inverse"/> is true:
		/// - true  -> falseVisibility (Collapsed/Hidden)
		/// - false -> Visible
		/// </details>
		/// </summary>
		public bool Inverse { get; set; } = false;

		/// <summary>
		/// \brief When true, returns <see cref="Visibility.Hidden"/> instead of <see cref="Visibility.Collapsed"/> for false.
		/// </summary>
		public bool UseHidden { get; set; } = false;

		/// <summary>
		/// \brief Converts a boolean value to <see cref="Visibility"/>.
		/// \param value The source value from the binding (expected: bool or nullable bool).
		/// \param targetType The target type (expected: <see cref="Visibility"/>).
		/// \param parameter Optional parameter to override behavior (e.g., "Inverse", "Hidden", "Inverse,Hidden").
		/// \param culture The culture used in conversion.
		/// \return <see cref="Visibility.Visible"/> when true; otherwise <see cref="Visibility.Collapsed"/> or <see cref="Visibility.Hidden"/>.
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// \brief Read options from instance properties first.
			bool inverse = Inverse;
			bool useHidden = UseHidden;

			// \brief Allow ConverterParameter to override / augment options.
			ApplyOptionsFromParameter(parameter, ref inverse, ref useHidden);

			// \brief Normalize input to bool.
			bool val = ToBoolSafe(value);

			if (inverse)
				val = !val;

			return val
				? Visibility.Visible
				: (useHidden ? Visibility.Hidden : Visibility.Collapsed);
		}

		/// <summary>
		/// \brief Converts <see cref="Visibility"/> back to a boolean value.
		/// \details
		/// Visible -> true, otherwise false. Applies <see cref="Inverse"/> and optional parameter override.
		/// \param value The value from the target (expected: <see cref="Visibility"/>).
		/// \param targetType The target type (expected: bool).
		/// \param parameter Optional parameter to override behavior (e.g., "Inverse").
		/// \param culture The culture used in conversion.
		/// \return Boolean representation of the visibility state.
		/// </summary>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool inverse = Inverse;
			bool useHidden = UseHidden; // \brief Not used directly in ConvertBack, but parsed for symmetry.
			ApplyOptionsFromParameter(parameter, ref inverse, ref useHidden);

			Visibility vis = value is Visibility v ? v : Visibility.Collapsed;

			bool result = (vis == Visibility.Visible);
			return inverse ? !result : result;
		}

		/// <summary>
		/// \brief Safely converts various input types to bool.
		/// \details
		/// Supports: bool, bool?, string ("true"/"false"), numeric (0/1), null -> false.
		/// This implementation avoids nullable pattern matching issues (CS8116).
		/// </details>
		/// <param name="value">Input value.</param>
		/// <returns>Normalized boolean value.</returns>
		private static bool ToBoolSafe(object? value)
		{
			if (value == null)
				return false;

			// \brief Direct bool (covers boxed bool)
			if (value is bool b)
				return b;

			// \brief Nullable<bool> comes in boxed as bool when HasValue == true,
			//        so no separate "bool?" pattern is required.
			//        (This avoids CS8116 entirely.)

			// \brief String handling
			if (value is string s)
			{
				if (bool.TryParse(s, out bool parsedBool))
					return parsedBool;

				if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsedInt))
					return parsedInt != 0;
			}

			// \brief Numeric handling
			if (value is byte by) return by != 0;
			if (value is short sh) return sh != 0;
			if (value is int it) return it != 0;
			if (value is long lo) return lo != 0;

			// \brief Last-resort conversion
			try
			{
				return System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// \brief Applies converter options from the ConverterParameter.
		/// \details
		/// Supported parameter forms:
		/// - string: "Inverse", "Hidden", "UseHidden", "Inverse,Hidden"
		/// - bool: treated as Inverse override
		/// - Visibility.Hidden: treated as UseHidden=true
		/// \param parameter ConverterParameter object.
		/// \param inverse Inverse option (by ref).
		/// \param useHidden UseHidden option (by ref).
		/// </summary>
		private static void ApplyOptionsFromParameter(object parameter, ref bool inverse, ref bool useHidden)
		{
			if (parameter == null)
				return;

			// \brief bool parameter: treat as inverse override.
			if (parameter is bool b)
			{
				inverse = b;
				return;
			}

			// \brief Visibility parameter: Hidden implies UseHidden.
			if (parameter is Visibility vis)
			{
				useHidden = (vis == Visibility.Hidden);
				return;
			}

			// \brief string parameter: parse tokens.
			if (parameter is string s)
			{
				var tokens = s
					.Split(new[] { ',', ';', '|', ' ' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(t => t.Trim())
					.Where(t => !string.IsNullOrWhiteSpace(t))
					.ToArray();

				foreach (var token in tokens)
				{
					if (token.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
						inverse = true;

					if (token.Equals("Hidden", StringComparison.OrdinalIgnoreCase) ||
						token.Equals("UseHidden", StringComparison.OrdinalIgnoreCase))
						useHidden = true;

					if (token.Equals("Collapsed", StringComparison.OrdinalIgnoreCase))
						useHidden = false;
				}
			}
		}
	}
}
