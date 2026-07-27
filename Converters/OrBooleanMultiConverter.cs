using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;
/// <summary>
/// \if KO
/// <para>여러 부울 값에 논리 OR 연산을 적용합니다.</para>
/// \endif
/// \if EN
/// <para>Applies logical OR to multiple Boolean values.</para>
/// \endif
/// </summary>
public class OrBooleanMultiConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>입력 중 하나라도 부울 참이면 참을 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Returns true when any input is Boolean true.</para>
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
    /// <para>하나라도 참이면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when any input is true.</para>
    /// \endif
    /// </returns>
    /// <exception cref="NullReferenceException">
    /// \if KO
    /// <para><paramref name="values"/>가 null이면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="values"/> is null.</para>
    /// \endif
    /// </exception>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        foreach (var value in values)
        {
            if (value is bool b && b)
                return true;
        }
        return false;
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
