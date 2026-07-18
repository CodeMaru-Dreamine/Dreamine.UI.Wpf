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
    /// <para>너비 값에 8픽셀을 더한 왼쪽 여백을 생성합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates a left margin by adding eight pixels to a width value.</para>
    /// \endif
    /// </summary>
    public class OffsetMarginConverter : IValueConverter
    {
        /// <summary>
        /// \if KO
        /// <para>실수 너비를 왼쪽 여백으로 변환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Converts a floating-point width to a left margin.</para>
        /// \endif
        /// </summary>
        /// <param name="value">
        /// \if KO
        /// <para>원본 너비입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The source width.</para>
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
        /// <para>왼쪽이 너비+8인 여백이며 입력이 실수가 아니면 0 여백입니다.</para>
        /// \endif
        /// \if EN
        /// <para>A margin whose left side is width plus eight, or a zero margin for nondouble input.</para>
        /// \endif
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
                return new Thickness(width + 8, 0, 0, 0);
            return new Thickness(0);
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
            => throw new NotImplementedException();
    }

}
