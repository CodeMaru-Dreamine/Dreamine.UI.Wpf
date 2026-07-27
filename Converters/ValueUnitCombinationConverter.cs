using System.Globalization;
using System.Windows.Data;
using Dreamine.UI.Wpf.Localization;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>값 문자열과 선택적 단위 문자열을 하나의 표시 레이블로 결합합니다.</para>
/// \endif
/// \if EN
/// <para>Combines value text and optional unit text into one display label.</para>
/// \endif
/// </summary>
public class ValueUnitCombinationConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>첫 번째 값을 표시하고 두 번째 단위가 있으면 괄호 안에 추가합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Displays the first value and appends the second unit in parentheses when present.</para>
    /// \endif
    /// </summary>
    /// <param name="values">
    /// \if KO
    /// <para>값과 단위 순서의 입력입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Inputs ordered as value and unit.</para>
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
    /// <para>"값 (단위)" 또는 값 문자열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>"value (unit)" or the value text.</para>
    /// \endif
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// \if KO
    /// <para>입력 값이 두 개 미만이면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when fewer than two values are supplied.</para>
    /// \endif
    /// </exception>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var value = values[0]?.ToString() ?? string.Empty;
        var unit = values[1]?.ToString();

        var label = value;

        if (string.IsNullOrEmpty(unit))
            return label;

        return $"{label} ({unit})";
    }

    /// <summary>
    /// \if KO
    /// <para>다중 바인딩 역변환은 구현되어 있지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Multibinding reverse conversion is not implemented.</para>
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
    /// <exception cref="NotImplementedException">
    /// \if KO
    /// <para>호출할 때 항상 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Always thrown when called.</para>
    /// \endif
    /// </exception>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
