using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>실수 값과 매개변수를 허용 오차로 비교하여 선택 상태를 양방향 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Converts a floating-point value to and from selection state by comparing it with a parameter using tolerance.</para>
/// \endif
/// </summary>
public class DoubleToIsCheckedConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>원본 값과 매개변수의 차이가 0.0001 미만인지 확인합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Determines whether the difference between the source value and parameter is below 0.0001.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>비교할 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source value to compare.</para>
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
    /// <para>비교 기준 숫자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The numeric comparison parameter.</para>
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
    /// <para>두 수의 차이가 허용 오차 미만이면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when the values differ by less than the tolerance.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        if (double.TryParse(System.Convert.ToString(value, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, out double targetValue) &&
            double.TryParse(System.Convert.ToString(parameter, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, out double currentValue))
        {
            return Math.Abs(currentValue - targetValue) < 0.0001;
        }

        return false;
    }

    /// <summary>
    /// \if KO
    /// <para>선택 값이 참이면 숫자 매개변수를 원본으로 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Returns the numeric parameter to the source when selection is true.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>대상 선택 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target selection value.</para>
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
    /// <para>반환할 숫자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The numeric value to return.</para>
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
    /// <para>해석된 숫자 또는 <see cref="Binding.DoNothing"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The parsed number or <see cref="Binding.DoNothing"/>.</para>
    /// \endif
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// \if KO
    /// <para><paramref name="value"/>가 부울 값이 아니면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="value"/> is not a Boolean value.</para>
    /// \endif
    /// </exception>
    /// <exception cref="NullReferenceException">
    /// \if KO
    /// <para>선택된 상태에서 <paramref name="parameter"/>가 <see langword="null"/>이면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="parameter"/> is <see langword="null"/> while selected.</para>
    /// \endif
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value && double.TryParse(parameter.ToString(), CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }

        return Binding.DoNothing;
    }
}
