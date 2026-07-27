using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>숫자 입력이 지정한 화면 중단점 이상인지 확인합니다.</para>
/// \endif
/// \if EN
/// <para>Determines whether numeric input is at or above a specified screen breakpoint.</para>
/// \endif
/// </summary>
public class ScreenBreakpointConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>원본과 매개변수를 실수로 변환하여 크기를 비교합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts the source and parameter to floating-point values and compares their magnitudes.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>원본 화면 크기 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source screen-size value.</para>
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
    /// <para>비교할 중단점입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The breakpoint to compare.</para>
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
    /// <para>원본이 중단점 이상이면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when the source is at least the breakpoint.</para>
    /// \endif
    /// </returns>
    /// <exception cref="FormatException">
    /// \if KO
    /// <para>입력 또는 매개변수를 실수로 변환할 수 없으면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when the input or parameter cannot be converted to a floating-point value.</para>
    /// \endif
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// \if KO
    /// <para>입력 또는 매개변수가 실수 변환을 지원하지 않으면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when the input or parameter does not support floating-point conversion.</para>
    /// \endif
    /// </exception>
    /// <exception cref="OverflowException">
    /// \if KO
    /// <para>변환 결과가 실수 범위를 벗어나면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when conversion exceeds the floating-point range.</para>
    /// \endif
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        var input = System.Convert.ToDouble(value);
        var threshold = System.Convert.ToDouble(parameter);

        return input >= threshold;
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
