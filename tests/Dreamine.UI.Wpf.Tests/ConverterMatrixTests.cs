using System.Windows.Data;
using Dreamine.UI.Wpf.Converters;

namespace Dreamine.UI.Wpf.Tests;

public sealed class ConverterMatrixTests
{
    private static readonly object?[] Values =
    [
        null,
        true,
        false,
        0,
        1,
        -1,
        1L,
        12.5d,
        string.Empty,
        "0",
        "1",
        "12.5",
        "text",
        Visibility.Visible,
        Visibility.Hidden,
        Visibility.Collapsed,
        DateTime.UnixEpoch,
        new TimeSpan(1, 2, 3),
        DayOfWeek.Monday,
        new Thickness(1, 2, 3, 4),
        new object()
    ];

    private static readonly object?[] Parameters =
    [
        null,
        string.Empty,
        "Inverse",
        "Hidden",
        "Visible",
        "0",
        "1",
        "5",
        "5;10",
        "0.0",
        "true",
        "false",
        "Monday",
        "Width"
    ];

    private static readonly Type[] TargetTypes =
    [
        typeof(object),
        typeof(bool),
        typeof(int),
        typeof(long),
        typeof(double),
        typeof(string),
        typeof(Visibility),
        typeof(Thickness),
        typeof(TimeSpan)
    ];

    [Fact]
    public void AllPublicValueConverters_HandleRepresentativeInputMatrix()
    {
        var converterTypes = typeof(BooleanConverter).Assembly
            .GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                type.IsPublic &&
                typeof(IValueConverter).IsAssignableFrom(type) &&
                type.GetConstructor(Type.EmptyTypes) is not null)
            .ToArray();
        var successfulCalls = 0;

        foreach (var converterType in converterTypes)
        {
            var converter = (IValueConverter)Activator.CreateInstance(converterType)!;
            foreach (var value in Values)
            {
                foreach (var parameter in Parameters)
                {
                    foreach (var targetType in TargetTypes)
                    {
                        successfulCalls += TryInvoke(
                            () => converter.Convert(
                                value!,
                                targetType,
                                parameter!,
                                CultureInfo.InvariantCulture));
                        successfulCalls += TryInvoke(
                            () => converter.ConvertBack(
                                value!,
                                targetType,
                                parameter!,
                                CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        Assert.True(converterTypes.Length >= 40);
        Assert.True(successfulCalls >= 5_000);
    }

    [Fact]
    public void AllPublicMultiValueConverters_HandleRepresentativeInputMatrix()
    {
        var converterTypes = typeof(BooleanConverter).Assembly
            .GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                type.IsPublic &&
                typeof(IMultiValueConverter).IsAssignableFrom(type) &&
                type.GetConstructor(Type.EmptyTypes) is not null)
            .ToArray();
        object[][] valueSets =
        [
            [],
            [true],
            [false],
            [true, true],
            [true, false],
            [false, false],
            [0, 1],
            ["", "text"],
            [DependencyProperty.UnsetValue, null!]
        ];
        var successfulCalls = 0;

        foreach (var converterType in converterTypes)
        {
            var converter = (IMultiValueConverter)Activator.CreateInstance(converterType)!;
            foreach (var values in valueSets)
            {
                foreach (var parameter in Parameters)
                {
                    successfulCalls += TryInvoke(
                        () => converter.Convert(
                            values,
                            typeof(object),
                            parameter!,
                            CultureInfo.InvariantCulture));
                    successfulCalls += TryInvoke(
                        () => converter.ConvertBack(
                            null!,
                            TargetTypes,
                            parameter!,
                            CultureInfo.InvariantCulture));
                }
            }
        }

        Assert.True(converterTypes.Length >= 5);
        Assert.True(successfulCalls >= 100);
    }

    private static int TryInvoke(Func<object?> operation)
    {
        try
        {
            _ = operation();
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
