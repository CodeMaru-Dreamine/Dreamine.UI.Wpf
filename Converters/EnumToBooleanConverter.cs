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
    /// <para>열거형 값과 매개변수로 지정한 열거형 멤버의 선택 상태를 양방향 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts between an enumeration value and selection state for a member named by the parameter.</para>
    /// \endif
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// \if KO
        /// <para>매개변수 이름을 원본 열거형 형식으로 해석하여 현재 값과 비교합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Parses the parameter name as the source enumeration type and compares it with the current value.</para>
        /// \endif
        /// </summary>
        /// <param name="value">
        /// \if KO
        /// <para>원본 열거형 값입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The source enumeration value.</para>
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
        /// <para>비교할 열거형 멤버 이름입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The enumeration member name to compare.</para>
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
        /// <para>현재 값이 지정 멤버와 같으면 <see langword="true"/>입니다.</para>
        /// \endif
        /// \if EN
        /// <para><see langword="true"/> when the current value equals the named member.</para>
        /// \endif
        /// </returns>
        /// <exception cref="ArgumentException">
        /// \if KO
        /// <para>매개변수가 유효한 열거형 멤버 이름이 아니면 발생합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Thrown when the parameter is not a valid enumeration member name.</para>
        /// \endif
        /// </exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            return value.Equals(Enum.Parse(value.GetType(), parameter.ToString()!));
        }

        /// <summary>
        /// \if KO
        /// <para>대상 선택 값이 참이면 매개변수 이름을 원본 열거형 값으로 변환합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Converts the parameter name to the source enumeration value when target selection is true.</para>
        /// \endif
        /// </summary>
        /// <param name="value">
        /// \if KO
        /// <para>대상 선택 값입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The target selection value.</para>
        /// \endif
        /// </param>
        /// <param name="targetType">
        /// \if KO
        /// <para>원본 열거형 형식입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The source enumeration type.</para>
        /// \endif
        /// </param>
        /// <param name="parameter">
        /// \if KO
        /// <para>반환할 열거형 멤버 이름입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The enumeration member name to return.</para>
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
        /// <para>열거형 값 또는 <see cref="Binding.DoNothing"/>입니다.</para>
        /// \endif
        /// \if EN
        /// <para>The enumeration value or <see cref="Binding.DoNothing"/>.</para>
        /// \endif
        /// </returns>
        /// <exception cref="ArgumentException">
        /// \if KO
        /// <para>대상 형식이 열거형이 아니거나 매개변수가 유효한 멤버 이름이 아니면 발생합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Thrown when the target type is not an enumeration or the parameter is not a valid member name.</para>
        /// \endif
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// \if KO
        /// <para>선택된 상태에서 매개변수가 null이면 발생합니다.</para>
        /// \endif
        /// \if EN
        /// <para>Thrown when the parameter is null while selected.</para>
        /// \endif
        /// </exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Enum.Parse(targetType, parameter.ToString()!);
            }
            return Binding.DoNothing;
        }
    }
}
