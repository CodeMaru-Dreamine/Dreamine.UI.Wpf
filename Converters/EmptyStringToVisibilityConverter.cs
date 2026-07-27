using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>문자열이 비어 있는지에 따라 WPF 가시성을 결정합니다.</para>
/// \endif
/// \if EN
/// <para>Determines WPF visibility according to whether a string is empty.</para>
/// \endif
/// </summary>
public class EmptyStringToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>비어 있음 판정을 반전할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether the empty-state test is inverted.</para>
    /// \endif
    /// </summary>
    public bool Invert { get; set; } = false;
    /// <summary>
    /// \if KO
    /// <para>보이지 않는 상태에 Hidden을 사용할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether Hidden is used for the nonvisible state.</para>
    /// \endif
    /// </summary>
    public bool UseHidden { get; set; } = false;

    /// <summary>
    /// \if KO
    /// <para>문자열의 공백 여부를 구성에 따라 가시성 값으로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts string whitespace state to a visibility value according to configuration.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>검사할 문자열 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The string value to inspect.</para>
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
    /// <para>사용하지 않는 매개변수입니다.</para>
    /// \endif
    /// \if EN
    /// <para>An unused parameter.</para>
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
    /// <para>Visible, Hidden 또는 Collapsed입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Visible, Hidden, or Collapsed.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isNullOrEmpty = string.IsNullOrWhiteSpace(value as string);

        if (Invert)
            isNullOrEmpty = !isNullOrEmpty;

        return !isNullOrEmpty ? Visibility.Visible : (UseHidden ? Visibility.Hidden : Visibility.Collapsed);
    }

    /// <summary>
    /// \if KO
    /// <para>역변환은 구현되어 있지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Reverse conversion is not implemented.</para>
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
    /// <para>정상적으로 반환되지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>This method does not return normally.</para>
    /// \endif
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// \if KO
    /// <para>호출할 때 항상 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Always thrown when called.</para>
    /// \endif
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
