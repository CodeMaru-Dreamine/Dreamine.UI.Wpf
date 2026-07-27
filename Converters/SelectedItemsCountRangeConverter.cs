using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>선택 항목 컬렉션의 개수가 지정한 최소·최대 범위에 있는지 확인합니다.</para>
/// \endif
/// \if EN
/// <para>Determines whether a selected-items collection count lies within specified minimum and maximum bounds.</para>
/// \endif
/// </summary>
public class SelectedItemsCountRangeConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>컬렉션 개수를 "최소-최대" 또는 "최소" 매개변수 범위와 비교합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Compares a collection count with a "minimum-maximum" or "minimum" parameter range.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>선택 항목 컬렉션입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The selected-items collection.</para>
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
    /// <para>최소 또는 최소-최대 범위 문자열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A minimum or minimum-maximum range string.</para>
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
    /// <para>개수가 유효한 범위에 있으면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when the count is within the valid range.</para>
    /// \endif
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// \if KO
    /// <para>범위 문자열을 분리한 결과가 비어 있으면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when splitting the range string yields no entries.</para>
    /// \endif
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var count = value is ICollection collection ? collection.Count : 0;

        if (parameter is string paramString && !string.IsNullOrEmpty(paramString))
        {
            bool bCheck = false;

            var parts = paramString.Split('-');
            if (int.TryParse(parts[0], out int min))
            {
                bCheck = count >= min;
            }
            if (parts.Length == 2 && int.TryParse(parts[1], out int max))
            {
                bCheck = bCheck && count <= max;
            }

            return bCheck;
        }

        return false;
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
        => Binding.DoNothing;
}
