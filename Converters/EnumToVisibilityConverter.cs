using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>열거형 이름이 매개변수 목록에 포함되는지에 따라 가시성을 결정합니다.</para>
/// \endif
/// \if EN
/// <para>Determines visibility according to whether an enumeration name appears in a parameter list.</para>
/// \endif
/// </summary>
public class EnumToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>포함 여부 결과를 반전할지 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether the membership result is inverted.</para>
    /// \endif
    /// </summary>
    public bool IsInverse { get; set; }

    /// <summary>
    /// \if KO
    /// <para>원본 이름을 쉼표로 구분된 대상 이름들과 대소문자 구분 없이 비교합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Compares the source name case-insensitively with comma-separated target names.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>원본 열거형 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source enumeration value.</para>
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
    /// <para>쉼표로 구분된 대상 이름입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Comma-separated target names.</para>
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
    /// <para>조건을 만족하면 Visible, 아니면 Collapsed입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Visible when the condition is met; otherwise, Collapsed.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return Visibility.Collapsed;

        string enumValue = value.ToString() ?? string.Empty;
        string[] targetValues = parameter.ToString()?
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];

        bool result = targetValues.Contains(enumValue, StringComparer.OrdinalIgnoreCase)
            ? true
            : false;

        result = IsInverse ? !result : result;

        return result ? Visibility.Visible : Visibility.Collapsed;
    }
    /// <summary>
    /// \if KO
    /// <para>역변환을 수행하지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Does not perform reverse conversion.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>대상 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target value.</para>
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
    /// <para>변환기 매개변수입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The converter parameter.</para>
    /// \endif
    /// </param>
    /// <param name="culture">
    /// \if KO
    /// <para>변환 문화권입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The conversion culture.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>항상 <see cref="Binding.DoNothing"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Always <see cref="Binding.DoNothing"/>.</para>
    /// \endif
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
