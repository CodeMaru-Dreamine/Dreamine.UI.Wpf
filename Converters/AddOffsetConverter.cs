using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
    /// <summary>
    /// \if KO
    /// <para>실수 값에 변환기 매개변수로 지정한 오프셋을 더합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Adds an offset supplied as the converter parameter to a floating-point value.</para>
    /// \endif
    /// </summary>
    public class AddOffsetConverter : IValueConverter
    {
        /// <summary>
        /// \if KO
        /// <para>값이 <see cref="double"/>이고 매개변수를 불변 문화권 숫자로 해석할 수 있으면 두 값을 더합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Adds the values when the source is a <see cref="double"/> and the parameter can be parsed as an invariant-culture number.</para>
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
        /// <para>바인딩 대상 형식입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The binding target type.</para>
        /// \endif
        /// </param>
        /// <param name="parameter">
        /// \if KO
        /// <para>더할 오프셋입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The offset to add.</para>
        /// \endif
        /// </param>
        /// <param name="culture">
        /// \if KO
        /// <para>바인딩 문화권이며 이 변환에서는 사용하지 않습니다.</para>
        /// \endif
        /// \if EN
        /// <para>The binding culture, which is not used by this conversion.</para>
        /// \endif
        /// </param>
        /// <returns>
        /// \if KO
        /// <para>오프셋이 더해진 값이며, 입력을 변환할 수 없으면 원본 값입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The value with the offset added, or the original value when conversion is not possible.</para>
        /// \endif
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d && double.TryParse(parameter?.ToString(), CultureInfo.InvariantCulture, out double offset))
            {
                return d + offset;
            }
            return value;
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
