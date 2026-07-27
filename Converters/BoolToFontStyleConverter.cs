using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Dreamine.UI.Wpf.Converters
{
    /// <summary>
    /// \if KO
    /// <para>부울 값을 보통 또는 기울임꼴 글꼴 스타일로 양방향 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts Boolean values to and from normal or italic font styles.</para>
    /// \endif
    /// </summary>
    public class BoolToFontStyleConverter : IValueConverter
    {
        /// <summary>
        /// \if KO
        /// <para>참을 기울임꼴로, 그 밖의 값을 보통 스타일로 변환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Converts true to italic and all other values to normal style.</para>
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
        /// <para>기울임꼴 또는 보통 글꼴 스타일입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The italic or normal font style.</para>
        /// \endif
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isItalic && isItalic)
                return FontStyles.Italic;
            return FontStyles.Normal;
        }

        /// <summary>
        /// \if KO
        /// <para>기울임꼴 스타일인지 확인하여 부울 값으로 변환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Converts a font style to a Boolean indicating whether it is italic.</para>
        /// \endif
        /// </summary>
        /// <param name="value">
        /// \if KO
        /// <para>대상 스타일입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The target style.</para>
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
        /// <para>기울임꼴이면 <see langword="true"/>입니다.</para>
        /// \endif
        /// \if EN
        /// <para><see langword="true"/> when the style is italic.</para>
        /// \endif
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is FontStyle style && style == FontStyles.Italic;
        }
    }
}
