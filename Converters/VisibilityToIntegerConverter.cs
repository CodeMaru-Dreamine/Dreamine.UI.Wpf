using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>WPF 가시성 값을 0 또는 1 정수로 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Converts a WPF visibility value to zero or one.</para>
/// \endif
/// </summary>
public class VisibilityToIntegerConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>정수 결과의 의미를 반전할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether the integer result meaning is inverted.</para>
    /// \endif
    /// </summary>
    public bool IsInversed { get; set; }
    /// <summary>
    /// \if KO
    /// <para>가시성의 Visible 상태와 반전 설정을 XOR 방식으로 평가하여 0 또는 1을 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Evaluates visible state and inversion with XOR logic and returns zero or one.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>원본 가시성 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source visibility value.</para>
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
    /// <para>조건을 만족하면 1, 아니면 0입니다.</para>
    /// \endif
    /// \if EN
    /// <para>One when the condition is met; otherwise, zero.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible ^ !IsInversed ? 1 : 0;
        }

        return 0;
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
