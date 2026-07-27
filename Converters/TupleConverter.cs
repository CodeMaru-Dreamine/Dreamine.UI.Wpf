using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>두 바인딩 값을 <see cref="Tuple{T1,T2}"/>로 결합하고 다시 분리합니다.</para>
/// \endif
/// \if EN
/// <para>Combines two binding values into a <see cref="Tuple{T1,T2}"/> and splits it back.</para>
/// \endif
/// </summary>
public class TupleConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>첫 두 입력 값으로 튜플을 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates a tuple from the first two input values.</para>
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
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return Tuple.Create(values[0], values[1]);
    }

    /// <summary>
    /// \if KO
    /// <para>대상 튜플을 두 원본 값으로 분리합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Splits the target tuple into two source values.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>분리할 대상 튜플입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target tuple to split.</para>
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
    /// <para>튜플의 첫 번째와 두 번째 항목 배열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>An array containing the tuple's first and second items.</para>
    /// \endif
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// \if KO
    /// <para><paramref name="value"/>가 두 개의 object를 담은 튜플이 아니면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="value"/> is not a tuple containing two objects.</para>
    /// \endif
    /// </exception>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        var tuple = (Tuple<object, object>)value;
        return [tuple.Item1, tuple.Item2];
    }
}
