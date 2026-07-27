using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>여러 부울 입력에 논리 AND 연산을 적용합니다.</para>
/// \endif
/// \if EN
/// <para>Applies a logical AND operation to multiple Boolean inputs.</para>
/// \endif
/// </summary>
public class AndConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para><see langword="null"/> 입력을 참으로 처리할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether <see langword="null"/> inputs are treated as true.</para>
    /// \endif
    /// </summary>
    public bool TreatNullAsTrue { get; set; } = false;

    /// <summary>
    /// \if KO
    /// <para>모든 입력이 참인지 확인합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Determines whether all input values are true.</para>
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
    /// <para>바인딩 대상 형식입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The binding target type.</para>
    /// \endif
    /// </param>
    /// <param name="parameter">
    /// \if KO
    /// <para>사용하지 않는 변환기 매개변수입니다.</para>
    /// \endif
    /// \if EN
    /// <para>An unused converter parameter.</para>
    /// \endif
    /// </param>
    /// <param name="culture">
    /// \if KO
    /// <para>사용하지 않는 변환 문화권입니다.</para>
    /// \endif
    /// \if EN
    /// <para>An unused conversion culture.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>구성된 null 정책을 적용한 뒤 모든 값이 참이면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when every value is true after applying the configured null policy.</para>
    /// \endif
    /// </returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length == 0) return false;

        foreach (var v in values)
        {
            if (v == null) { if (!TreatNullAsTrue) return false; else continue; }

            if (v is bool b)
            {
                if (!b) return false;
            }
            else
            {
                return false;
            }
        }
        return true;
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
        => throw new NotSupportedException();
}
