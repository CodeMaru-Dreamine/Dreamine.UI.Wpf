using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>제공된 모든 값이 부울 참인지 확인합니다.</para>
/// \endif
/// \if EN
/// <para>Determines whether every supplied value is Boolean true.</para>
/// \endif
/// </summary>
public class BothTrueMultiConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>모든 입력이 부울 참일 때 참을 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Returns true when every input is Boolean true.</para>
    /// \endif
    /// </summary>
    /// <param name="values">
    /// \if KO
    /// <para>검사할 값 배열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The values to inspect.</para>
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
    /// <para>모든 값이 참이면 <see langword="true"/>입니다. 빈 배열도 참입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when all values are true; an empty array also yields true.</para>
    /// \endif
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// \if KO
    /// <para><paramref name="values"/>가 <see langword="null"/>인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="values"/> is <see langword="null"/>.</para>
    /// \endif
    /// </exception>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values.All(value => value is bool b && b);
    }

    /// <summary>
    /// \if KO
    /// <para>다중 바인딩 역변환은 지원하지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Multibinding reverse conversion is not supported.</para>
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
    /// <param name="targetTypes">
    /// \if KO
    /// <para>원본 형식 배열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source-type array.</para>
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
    /// <exception cref="NotSupportedException">
    /// \if KO
    /// <para>호출할 때 항상 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Always thrown when called.</para>
    /// \endif
    /// </exception>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
