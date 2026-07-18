using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>부울 의미의 값을 쉼표로 구분된 두 변환 결과 중 하나로 매핑합니다.</para>
/// \endif
/// \if EN
/// <para>Maps a Boolean-like value to one of two comma-separated conversion results.</para>
/// \endif
/// </summary>
public class BoolToIntDynamicConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>참과 거짓의 결과 선택을 반전할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether selection of true and false results is inverted.</para>
    /// \endif
    /// </summary>
    public bool IsInverse { get; set; }
    /// <summary>
    /// \if KO
    /// <para>부울·문자열·정수 입력을 논리값으로 해석하여 매개변수의 두 항목 중 하나를 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Interprets a Boolean, string, or integer input as a logical value and returns one of two parameter entries.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source value.</para>
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
    /// <para>쉼표로 구분된 참·거짓 결과입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Comma-separated true and false results.</para>
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
    /// <para>선택된 문자열이며 입력이나 매개변수가 null이면 0입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The selected string, or zero when the input or parameter is null.</para>
    /// \endif
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// \if KO
    /// <para>매개변수에 쉼표로 구분된 항목이 두 개 미만이면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when the parameter contains fewer than two comma-separated entries.</para>
    /// \endif
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return 0;

        bool bValue = value is bool b ? b
            : value is string s ? (s == "1" || string.Equals(s, "true", StringComparison.OrdinalIgnoreCase))
            : value is int i ? (i != 0)
            : false;

        string[] strParam = parameter.ToString()!.Split(",");

        if (!IsInverse)
            return bValue ? strParam[0] : strParam[1];

        return bValue ? strParam[1] : strParam[0];
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
