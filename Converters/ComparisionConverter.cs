using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>값의 문자열 표현을 매개변수와 비교하고 선택적으로 가시성으로 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Compares a value's string representation with a parameter and optionally converts the result to visibility.</para>
/// \endif
/// </summary>
public class ComparisionConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>비교 결과를 <see cref="Visibility"/>로 반환할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether the comparison result is returned as <see cref="Visibility"/>.</para>
    /// \endif
    /// </summary>
    public bool ToVisibility { get; set; }
    /// <summary>
    /// \if KO
    /// <para>비교 결과를 반전할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether the comparison result is inverted.</para>
    /// \endif
    /// </summary>
    public bool Inverse { get; set; }

    /// <summary>
    /// \if KO
    /// <para>입력과 매개변수의 문자열 표현이 같은지 비교합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Compares the string representations of the input and parameter.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>비교할 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source value to compare.</para>
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
    /// <para>비교 기준입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The comparison operand.</para>
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
    /// <para>부울 비교 결과 또는 대응하는 가시성 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The Boolean comparison result or corresponding visibility value.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        var isEqual = value.ToString() == parameter.ToString();

        if (Inverse)
        {
            isEqual = !isEqual;
        }

        if (ToVisibility)
        {
            return isEqual ? Visibility.Visible : Visibility.Collapsed;
        }

        return isEqual;
    }

    /// <summary>
    /// \if KO
    /// <para>대상 값이 참일 때 비교 매개변수를 원본으로 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Returns the comparison parameter to the source when the target value is true.</para>
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
    /// <para>원본으로 반환할 비교 기준입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The comparison operand returned to the source.</para>
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
    /// <para>대상이 참이면 매개변수이고, 아니면 <see cref="Binding.DoNothing"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The parameter when the target is true; otherwise, <see cref="Binding.DoNothing"/>.</para>
    /// \endif
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.Equals(true) == true ? parameter : Binding.DoNothing;
    }
}
