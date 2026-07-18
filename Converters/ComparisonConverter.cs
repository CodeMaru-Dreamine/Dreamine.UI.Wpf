using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>값과 매개변수의 문자열 표현을 같음 비교합니다.</para>
/// \endif
/// \if EN
/// <para>Compares the string representations of a value and parameter for equality.</para>
/// \endif
/// </summary>
public class ComparisonConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>비교 결과를 반전할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether the comparison result is inverted.</para>
    /// \endif
    /// </summary>
    public bool IsInverse { get; set; } = false;
    /// <summary>
    /// \if KO
    /// <para>입력과 매개변수의 문자열 표현이 같은지 확인합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Determines whether the string representations of the input and parameter are equal.</para>
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
    /// <para>같음 비교 결과이며 구성에 따라 반전됩니다.</para>
    /// \endif
    /// \if EN
    /// <para>The equality result, optionally inverted.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        bool result = value.ToString() == parameter.ToString();
        return IsInverse ? !result : result;
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
