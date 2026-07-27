using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>개체와 속성 이름을 받아 공개 인스턴스 속성 값을 대소문자 구분 없이 읽습니다.</para>
/// \endif
/// \if EN
/// <para>Reads a public instance property value case-insensitively from an object and property name.</para>
/// \endif
/// </summary>
public class PropertyPathValueConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>첫 번째 입력 개체에서 두 번째 입력 이름의 속성 값을 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the property named by the second input from the object in the first input.</para>
    /// \endif
    /// </summary>
    /// <param name="values">
    /// \if KO
    /// <para>대상 개체와 속성 이름 순서의 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Values ordered as target object and property name.</para>
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
    /// <para>속성 값이며 입력이나 속성이 없으면 런타임상 null입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The property value, or runtime null when input or property is absent.</para>
    /// \endif
    /// </returns>
    /// <exception cref="System.Reflection.TargetInvocationException">
    /// \if KO
    /// <para>속성 getter가 예외를 던지면 내부 예외를 감싸 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown with the getter exception as its inner exception when the property getter fails.</para>
    /// \endif
    /// </exception>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2 || values[0] == null || values[1] == null) return null!;
        var item = values[0];
        var propName = values[1]?.ToString();
        if (string.IsNullOrWhiteSpace(propName)) return null!;
        var prop = item.GetType().GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        return prop?.GetValue(item)!;
    }

    /// <summary>
    /// \if KO
    /// <para>두 원본 바인딩을 모두 변경하지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Leaves both source bindings unchanged.</para>
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
    /// <param name="targetTypes">
    /// \if KO
    /// <para>원본 형식 배열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source-type array.</para>
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
    /// <para>두 개의 <see cref="Binding.DoNothing"/> 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Two <see cref="Binding.DoNothing"/> values.</para>
    /// \endif
    /// </returns>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return new object[] { Binding.DoNothing, Binding.DoNothing };
    }
}

/// <summary>
/// \if KO
/// <para>개체의 공개 인스턴스 속성을 동적으로 읽는 다중 값 변환기를 제공합니다.</para>
/// \endif
/// \if EN
/// <para>Provides a multivalue converter that dynamically reads an object's public instance property.</para>
/// \endif
/// </summary>
public class PropertyAccessorConverter : IMultiValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>첫 번째 입력 개체에서 두 번째 입력 이름의 속성 값을 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the property named by the second input from the object in the first input.</para>
    /// \endif
    /// </summary>
    /// <param name="values">
    /// \if KO
    /// <para>대상 개체와 속성 이름 순서의 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Values ordered as target object and property name.</para>
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
    /// <para>속성 값이며 입력이나 속성이 없으면 런타임상 null입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The property value, or runtime null when input or property is absent.</para>
    /// \endif
    /// </returns>
    /// <exception cref="System.Reflection.TargetInvocationException">
    /// \if KO
    /// <para>속성 getter가 예외를 던지면 내부 예외를 감싸 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown with the getter exception as its inner exception when the property getter fails.</para>
    /// \endif
    /// </exception>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2 || values[0] == null || values[1] == null) return null!;
        var item = values[0];
        var propName = values[1]?.ToString();
        if (string.IsNullOrWhiteSpace(propName)) return null!;
        var prop = item.GetType().GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        return prop?.GetValue(item)!;
    }

    /// <summary>
    /// \if KO
    /// <para>매개변수를 검사하지만 두 원본 바인딩을 모두 변경하지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Inspects the parameter but leaves both source bindings unchanged.</para>
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
    /// <param name="targetTypes">
    /// \if KO
    /// <para>원본 형식 배열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The source-type array.</para>
    /// \endif
    /// </param>
    /// <param name="parameter">
    /// \if KO
    /// <para>선택적 개체 배열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>An optional object array.</para>
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
    /// <para>두 개의 <see cref="Binding.DoNothing"/> 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Two <see cref="Binding.DoNothing"/> values.</para>
    /// \endif
    /// </returns>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        if (parameter is object[] p && p.Length >= 2) {  }
        return new object[] { Binding.DoNothing, Binding.DoNothing };
    }
}
