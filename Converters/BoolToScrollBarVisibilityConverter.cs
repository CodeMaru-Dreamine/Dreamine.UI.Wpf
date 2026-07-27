using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>부울 값과 스크롤 막대 표시 방식 사이를 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Converts between Boolean values and scroll-bar visibility modes.</para>
/// \endif
/// </summary>
public class BoolToScrollBarVisibilityConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>참을 자동 표시로, 나머지를 비활성화로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts true to automatic visibility and all other values to disabled.</para>
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
    /// <para><see cref="ScrollBarVisibility.Auto"/> 또는 <see cref="ScrollBarVisibility.Disabled"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see cref="ScrollBarVisibility.Auto"/> or <see cref="ScrollBarVisibility.Disabled"/>.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isVisible)
        {
            return isVisible ? ScrollBarVisibility.Auto : ScrollBarVisibility.Disabled;
        }

        return ScrollBarVisibility.Disabled;
    }

    /// <summary>
    /// \if KO
    /// <para>자동 표시 방식을 참으로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts automatic visibility to true.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>대상 표시 방식입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target visibility mode.</para>
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
    /// <para>자동 표시이면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> for automatic visibility.</para>
    /// \endif
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ScrollBarVisibility visibility)
        {
            return visibility == ScrollBarVisibility.Auto;
        }

        return false;
    }
}
