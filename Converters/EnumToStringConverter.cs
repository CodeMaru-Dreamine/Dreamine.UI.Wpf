using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>열거형 값과 멤버 이름 문자열 사이를 변환합니다.</para>
/// \endif
/// \if EN
/// <para>Converts between enumeration values and member-name strings.</para>
/// \endif
/// </summary>
public class EnumToStringConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>출력 문자열을 대문자로 변환할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether output text is converted to uppercase.</para>
    /// \endif
    /// </summary>
    public bool IsUpper { get; set; }
    /// <summary>
    /// \if KO
    /// <para>원본 값을 문자열로 변환하고 선택적으로 대문자로 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts the source value to text and optionally uppercases it.</para>
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
    /// <para>열거형 이름 문자열이며 원본이 null이면 런타임상 null입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The enumeration-name text, or runtime null when the source is null.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return IsUpper ? value?.ToString()?.ToUpper()! : value?.ToString()!;
    }

    /// <summary>
    /// \if KO
    /// <para>정의된 멤버 이름 문자열을 지정한 열거형 값으로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts a defined member-name string to the specified enumeration value.</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>대상 멤버 이름입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target member name.</para>
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
    /// <para>열거형 값 또는 <see cref="Binding.DoNothing"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The enumeration value or <see cref="Binding.DoNothing"/>.</para>
    /// \endif
    /// </returns>
    /// <exception cref="ArgumentException">
    /// \if KO
    /// <para><paramref name="targetType"/>이 열거형이 아니면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="targetType"/> is not an enumeration.</para>
    /// \endif
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str && Enum.IsDefined(targetType, str))
        {
            return Enum.Parse(targetType, str);
        }
        return Binding.DoNothing;
    }
}
