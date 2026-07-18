using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>부울 값을 양방향으로 반전합니다.</para>
/// \endif
/// \if EN
/// <para>Negates Boolean values in both conversion directions.</para>
/// \endif
/// </summary>
public sealed class BooleanNegationConverter : IValueConverter
{
	/// <summary>
	/// \if KO
	/// <para>재사용 가능한 공유 변환기 인스턴스를 가져옵니다.</para>
	/// \endif
	/// \if EN
	/// <para>Gets a reusable shared converter instance.</para>
	/// \endif
	/// </summary>
	public static BooleanNegationConverter Instance { get;  } = new();
    /// <summary>
    /// \if KO
    /// <para>부울 입력을 반전하며, 다른 형식은 그대로 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Negates a Boolean input and returns other types unchanged.</para>
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
    /// <param name="t">
    /// \if KO
    /// <para>대상 형식입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target type.</para>
    /// \endif
    /// </param>
    /// <param name="p">
    /// \if KO
    /// <para>변환기 매개변수입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The converter parameter.</para>
    /// \endif
    /// </param>
    /// <param name="c">
    /// \if KO
    /// <para>변환 문화권입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The conversion culture.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>반전된 부울 값 또는 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The negated Boolean value or the original value.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type t, object p, CultureInfo c) => value is bool b ? !b : value;
    /// <summary>
    /// \if KO
    /// <para>대상 부울 값을 반전하며, 다른 형식은 그대로 반환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Negates a target Boolean value and returns other types unchanged.</para>
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
    /// <param name="t">
    /// \if KO
    /// <para>원본 형식입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source type.</para>
    /// \endif
    /// </param>
    /// <param name="p">
    /// \if KO
    /// <para>변환기 매개변수입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The converter parameter.</para>
    /// \endif
    /// </param>
    /// <param name="c">
    /// \if KO
    /// <para>변환 문화권입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The conversion culture.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>반전된 부울 값 또는 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The negated Boolean value or the original value.</para>
    /// \endif
    /// </returns>
    public object ConvertBack(object value, Type t, object p, CultureInfo c) => value is bool b ? !b : value;
}
