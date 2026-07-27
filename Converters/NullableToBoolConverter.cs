using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>값이 null인지 여부를 부울 값으로 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Converts whether a value is null to a Boolean value.</para>
/// \endif
/// </summary>
public class NullableToBoolConverter: IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>null 판정 결과를 반전할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether the null-test result is inverted.</para>
    /// \endif
    /// </summary>
    public bool Invert { get; set; } = false;

    /// <summary>
    /// \if KO
    /// <para>원본이 null인지 확인하고 선택적으로 결과를 반전합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Tests whether the source is null and optionally inverts the result.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>검사할 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source value to inspect.</para>
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
    /// <para>null 판정 결과이며 구성에 따라 반전됩니다.</para>
    /// \endif
    /// \if EN
    /// <para>The null-test result, optionally inverted.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool result = value == null;
        return Invert ? !result : result;
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
