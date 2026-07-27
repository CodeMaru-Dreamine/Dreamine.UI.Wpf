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
    /// <para>부울 값을 보통 또는 굵은 글꼴 두께로 양방향 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts Boolean values to and from normal or bold font weights.</para>
    /// \endif
    /// </summary>
    public class BoolToFontWeightConverter : IValueConverter
    {
        /// <summary>
        /// \if KO
        /// <para>참을 굵은 두께로, 그 밖의 값을 보통 두께로 변환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Converts true to bold and all other values to normal weight.</para>
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
        /// <para>굵은 또는 보통 글꼴 두께입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The bold or normal font weight.</para>
        /// \endif
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isBold && isBold)
                return FontWeights.Bold;
            return FontWeights.Normal;
        }

        /// <summary>
        /// \if KO
        /// <para>굵은 두께인지 확인하여 부울 값으로 변환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Converts a font weight to a Boolean indicating whether it is bold.</para>
        /// \endif
        /// </summary>
        /// <param name="value">
        /// \if KO
        /// <para>대상 두께입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The target weight.</para>
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
        /// <para>굵은 두께이면 <see langword="true"/>입니다.</para>
        /// \endif
        /// \if EN
        /// <para><see langword="true"/> when the weight is bold.</para>
        /// \endif
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is FontWeight weight && weight == FontWeights.Bold;
        }
    }

}
