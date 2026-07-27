using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>대상 종속성 속성의 현재 값을 지정한 변환기로 즉시 변환하는 XAML 태그 확장을 제공합니다.</para>
/// \endif
/// \if EN
/// <para>Provides a XAML markup extension that immediately converts a target dependency property's current value with a specified converter.</para>
/// \endif
/// </summary>
[MarkupExtensionReturnType(typeof(object))]
public class SelfTextConverterExtension : MarkupExtension
{
    /// <summary>
    /// \if KO
    /// <para>현재 값을 변환할 변환기를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets the converter applied to the current value.</para>
    /// \endif
    /// </summary>
    public IValueConverter Converter { get; set; } = null!;
	/// <summary>
	/// \if KO
	/// <para>변환기에 전달할 선택적 매개변수를 가져오거나 설정합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Gets or sets the optional parameter passed to the converter.</para>
	/// \endif
	/// </summary>
	public object Parameter { get; set; } = null!;

	/// <summary>
	/// \if KO
	/// <para>태그 확장의 대상 개체와 속성을 찾아 현재 속성값의 변환 결과를 제공합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Resolves the markup-extension target and provides the converted current property value.</para>
	/// \endif
	/// </summary>
	/// <param name="serviceProvider">
	/// \if KO
	/// <para>대상 정보 서비스를 제공하는 개체입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The provider used to obtain target information.</para>
	/// \endif
	/// </param>
	/// <returns>
	/// \if KO
	/// <para>변환된 값이며 대상이나 변환기가 없으면 런타임상 null입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The converted value, or runtime null when the target or converter is unavailable.</para>
	/// \endif
	/// </returns>
	/// <exception cref="ArgumentNullException">
	/// \if KO
	/// <para><paramref name="serviceProvider"/>가 null이면 발생할 수 있습니다.</para>
	/// \endif
	/// \if EN
	/// <para>May be thrown when <paramref name="serviceProvider"/> is null.</para>
	/// \endif
	/// </exception>
	public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is not IProvideValueTarget pvt)
            return null!;

        var targetObject = pvt.TargetObject;
        var targetProperty = pvt.TargetProperty as DependencyProperty;

        if (targetObject == null || targetProperty == null || Converter == null)
            return null!;

        if (targetObject is DependencyObject depObj)
        {
            var currentValue = depObj.GetValue(targetProperty);
            return Converter.Convert(currentValue, targetProperty.PropertyType, Parameter, CultureInfo.CurrentCulture);
        }

        return null!;
    }
}
