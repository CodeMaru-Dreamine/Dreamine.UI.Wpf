using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>다중 바인딩의 첫 두 값을 튜플로 결합합니다.</para>
/// \endif
/// \if EN
/// <para>Combines the first two multibinding values into a tuple.</para>
/// \endif
/// </summary>
public class MultiParameterConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>첫 번째와 두 번째 입력으로 튜플을 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates a tuple from the first and second inputs.</para>
    /// \endif
    /// </summary>
    /// <param name="values">
    /// \if KO
    /// <para>두 개 이상의 입력 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Two or more input values.</para>
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
    /// <para>첫 두 값을 담은 튜플입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A tuple containing the first two values.</para>
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
    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return Tuple.Create(values[0], values[1]);
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
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
