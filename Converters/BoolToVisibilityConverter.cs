using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>부울 의미의 값과 WPF <see cref="Visibility"/> 값을 양방향 변환하며 반전 및 Hidden 옵션을 지원합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts Boolean-like values to and from WPF <see cref="Visibility"/> values with inversion and Hidden options.</para>
	/// \endif
	/// </summary>
	/// <remarks>
	/// \if KO
	/// <para>변환기 속성 또는 "Inverse", "Hidden", "UseHidden", "Collapsed" 토큰을 포함하는 매개변수로 동작을 구성할 수 있습니다.</para>
	/// \endif
	/// \if EN
	/// <para>Behavior can be configured through converter properties or parameter tokens including "Inverse", "Hidden", "UseHidden", and "Collapsed".</para>
	/// \endif
	/// </remarks>
	public sealed class BoolToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>부울 의미를 반전할지 여부를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets whether the Boolean meaning is inverted.</para>
		/// \endif
		/// </summary>
		public bool Inverse { get; set; } = false;

		/// <summary>
		/// \if KO
		/// <para>거짓 상태에서 <see cref="Visibility.Collapsed"/> 대신 <see cref="Visibility.Hidden"/>을 반환할지 여부를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets whether false returns <see cref="Visibility.Hidden"/> instead of <see cref="Visibility.Collapsed"/>.</para>
		/// \endif
		/// </summary>
		public bool UseHidden { get; set; } = false;

		/// <summary>
		/// \if KO
		/// <para>원본 값을 안전하게 부울로 해석하여 구성된 가시성 상태로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Safely interprets the source as a Boolean and converts it to the configured visibility state.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>부울로 해석할 원본 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source value to interpret as Boolean.</para>
		/// \endif
		/// </param>
		/// <param name="targetType">
		/// \if KO
		/// <para>대상 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target type.</para>
		/// \endif
		/// </param>
		/// <param name="parameter">
		/// \if KO
		/// <para>선택적 반전·숨김 옵션입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Optional inversion and hiding options.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para><see cref="Visibility.Visible"/>, <see cref="Visibility.Hidden"/> 또는 <see cref="Visibility.Collapsed"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see cref="Visibility.Visible"/>, <see cref="Visibility.Hidden"/>, or <see cref="Visibility.Collapsed"/>.</para>
		/// \endif
		/// </returns>
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
		/// \if KO
		/// <para>가시성 값이 보임 상태인지 확인하고 선택적으로 결과를 반전합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Determines whether a visibility value is visible and optionally inverts the result.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>대상 가시성 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target visibility value.</para>
		/// \endif
		/// </param>
		/// <param name="targetType">
		/// \if KO
		/// <para>원본 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source type.</para>
		/// \endif
		/// </param>
		/// <param name="parameter">
		/// \if KO
		/// <para>선택적 반전 옵션입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Optional inversion options.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>보임 상태의 부울 표현이며 구성에 따라 반전됩니다.</para>
		/// \endif
		/// \if EN
		/// <para>The Boolean representation of visible state, optionally inverted.</para>
		/// \endif
		/// </returns>
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
		/// \if KO
		/// <para>부울, 문자열, 정수 계열 입력을 안전하게 부울 값으로 정규화합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Safely normalizes Boolean, string, and integral inputs to a Boolean value.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>정규화할 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The value to normalize.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>해석된 논리값이며 해석할 수 없으면 <see langword="false"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The interpreted logical value, or <see langword="false"/> when conversion fails.</para>
		/// \endif
		/// </returns>
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
		/// \if KO
		/// <para>변환기 매개변수의 부울, 가시성 또는 문자열 토큰을 반전·숨김 옵션에 적용합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Applies Boolean, visibility, or string tokens from the converter parameter to inversion and hiding options.</para>
		/// \endif
		/// </summary>
		/// <param name="parameter">
		/// \if KO
		/// <para>해석할 변환기 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The converter parameter to parse.</para>
		/// \endif
		/// </param>
		/// <param name="inverse">
		/// \if KO
		/// <para>갱신할 반전 옵션입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The inversion option to update.</para>
		/// \endif
		/// </param>
		/// <param name="useHidden">
		/// \if KO
		/// <para>갱신할 Hidden 사용 옵션입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The Hidden-state option to update.</para>
		/// \endif
		/// </param>
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
