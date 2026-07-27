using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>숫자 원본이 숫자 매개변수보다 큰지에 따라 가시성을 결정합니다.</para>
/// \endif
/// \if EN
/// <para>Determines visibility according to whether a numeric source is greater than a numeric parameter.</para>
/// \endif
/// </summary>
public class ValueComparisionToVisibilityConverter : IValueConverter
{
    //private bool _inverse = false;
    
    /// <summary>
    /// \if KO
    /// <para>원본이 null이면 Hidden을, 그렇지 않고 매개변수보다 크면 Visible을 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Returns Hidden for null input; otherwise, returns Visible when the source exceeds the parameter.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>비교할 원본 숫자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source number to compare.</para>
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
    /// <para>The numeric threshold.</para>
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
    /// <para>Visible 또는 Hidden입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Visible or Hidden.</para>
    /// \endif
    /// </returns>
    /// <exception cref="FormatException">
    /// \if KO
    /// <para>입력이나 매개변수를 실수로 변환할 수 없으면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when the input or parameter cannot be converted to a floating-point value.</para>
    /// \endif
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// \if KO
    /// <para>입력이나 매개변수가 숫자 변환을 지원하지 않으면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when the input or parameter does not support numeric conversion.</para>
    /// \endif
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Visibility.Hidden;

        bool result = System.Convert.ToDouble(value) > System.Convert.ToDouble(parameter);

		return result ? Visibility.Visible : Visibility.Hidden;

		//if (!_inverse)
		//    return result ? Visibility.Visible : Visibility.Hidden;

		// return !result ? Visibility.Visible : Visibility.Hidden;
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

