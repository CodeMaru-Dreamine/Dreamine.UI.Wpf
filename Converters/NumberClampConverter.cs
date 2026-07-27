using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>숫자를 세미콜론으로 지정한 선택적 정수 최소·최대 범위로 제한합니다.</para>
/// \endif
/// \if EN
/// <para>Clamps a number to optional integer minimum and maximum bounds specified with a semicolon.</para>
/// \endif
/// </summary>
public class NumberClampConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>비교 가능한 입력을 32비트 정수로 변환하고 지정 범위에 제한합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts comparable input to a 32-bit integer and clamps it to the specified range.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>제한할 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source value to clamp.</para>
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
    /// <para>"최소;최대" 형식의 선택적 범위입니다.</para>
    /// \endif
    /// \if EN
    /// <para>An optional range in "minimum;maximum" form.</para>
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
    /// <para>제한된 정수이며 입력이 비교 가능하지 않으면 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The clamped integer, or the original value when it is not comparable.</para>
    /// \endif
    /// </returns>
    /// <exception cref="FormatException">
    /// \if KO
    /// <para>원본 값을 정수로 변환할 수 없으면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when the source cannot be converted to an integer.</para>
    /// \endif
    /// </exception>
    /// <exception cref="OverflowException">
    /// \if KO
    /// <para>변환된 값이 32비트 정수 범위를 벗어나면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when the converted value exceeds the 32-bit integer range.</para>
    /// \endif
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IComparable comparableValue)
        {
            int? min = null;
            int? max = null;

            if (parameter is string str)
            {
                var parts = str.Split(';');
                if (parts.Length > 0 && int.TryParse(parts[0], out int minVal))
                    min = minVal;
                if (parts.Length > 1 && int.TryParse(parts[1], out int maxVal))
                    max = maxVal;
            }

            var numeric = System.Convert.ToInt32(value);

            if (min.HasValue && numeric < min.Value)
                return min.Value;

            if (max.HasValue && numeric > max.Value)
                return max.Value;

            return numeric;
        }
        return value;
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
