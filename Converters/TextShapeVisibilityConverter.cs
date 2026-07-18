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
    /// <para>값의 문자열 표현이 "Text"인지에 따라 가시성을 결정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Determines visibility according to whether a value's string representation is "Text".</para>
    /// \endif
    /// </summary>
    public class TextShapeVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// \if KO
        /// <para>값이 "Text"이면 Visible, 아니면 Collapsed를 반환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Returns Visible when the value is "Text"; otherwise, Collapsed.</para>
        /// \endif
        /// </summary>
        /// <param name="value">
        /// \if KO
        /// <para>검사할 원본 값입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The source value to inspect.</para>
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
        /// <para>Visible 또는 Collapsed입니다.</para>
        /// \endif
        /// \if EN
        /// <para>Visible or Collapsed.</para>
        /// \endif
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value?.ToString() == "Text") ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// \if KO
        /// <para>역변환 결과로 런타임상 null을 반환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Returns runtime null for reverse conversion.</para>
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
        /// <para>런타임상 null입니다.</para>
        /// \endif
        /// \if EN
        /// <para>Runtime null.</para>
        /// \endif
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null!;
        }
    }

}
