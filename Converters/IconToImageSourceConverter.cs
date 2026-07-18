using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>GDI <see cref="Icon"/>을 WPF <see cref="ImageSource"/>로 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Converts a GDI <see cref="Icon"/> to a WPF <see cref="ImageSource"/>.</para>
/// \endif
/// </summary>
public class IconToImageSourceConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>아이콘 핸들에서 WPF 비트맵 소스를 생성합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates a WPF bitmap source from an icon handle.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>변환할 <see cref="Icon"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The <see cref="Icon"/> to convert.</para>
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
    /// <para>생성된 이미지 소스이며 입력이 아이콘이 아니면 런타임상 null입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The created image source, or runtime null when the input is not an icon.</para>
    /// \endif
    /// </returns>
    /// <exception cref="System.ComponentModel.Win32Exception">
    /// \if KO
    /// <para>네이티브 아이콘 핸들에서 비트맵 소스를 만들 수 없으면 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown when a bitmap source cannot be created from the native icon handle.</para>
    /// \endif
    /// </exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var icon = value as Icon;
        if (icon == null)
        {
            Trace.TraceWarning("Attempted to convert {0} instead of Icon object in IconToImageSourceConverter", value);
            return null!;
        }

        ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        return imageSource;
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
