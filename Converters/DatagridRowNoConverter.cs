using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>0부터 시작하는 데이터 그리드 행 인덱스를 1부터 시작하는 행 번호 문자열로 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Converts a zero-based data-grid row index to a one-based row-number string.</para>
/// \endif
/// </summary>
public class DatagridRowNoConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>정수 인덱스에 1을 더해 문자열로 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Adds one to an integer index and returns it as text.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>0부터 시작하는 행 인덱스입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The zero-based row index.</para>
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
    /// <para>1부터 시작하는 행 번호이며 입력이 정수가 아니면 "1"입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The one-based row number, or "1" when the input is not an integer.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int index)
            return (index + 1).ToString();
        return "1";
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
