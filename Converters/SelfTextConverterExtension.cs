using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dreamine.UI.Wpf.Converters;

[MarkupExtensionReturnType(typeof(object))]
public class SelfTextConverterExtension : MarkupExtension
{
    public IValueConverter Converter { get; set; } = null!;
	public object Parameter { get; set; } = null!;

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
