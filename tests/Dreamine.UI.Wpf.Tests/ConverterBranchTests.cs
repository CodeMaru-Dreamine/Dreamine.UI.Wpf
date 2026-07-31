using System.Windows.Markup;
using Dreamine.UI.Wpf.Controls;
using Dreamine.UI.Wpf.Localization;

namespace Dreamine.UI.Wpf.Tests;

public sealed class ConverterBranchTests
{
    [Theory]
    [InlineData(LedCorner.TopLeft, "OuterLeft", 5)]
    [InlineData(LedCorner.TopRight, "OuterLeft", 75)]
    [InlineData(LedCorner.BottomLeft, "OuterTop", 75)]
    [InlineData(LedCorner.BottomRight, "OuterTop", 75)]
    [InlineData(LedCorner.TopLeft, "InnerLeft", 9)]
    [InlineData(LedCorner.TopLeft, "InnerTop", 9)]
    [InlineData(LedCorner.TopLeft, "unknown", 0)]
    public void LedPositionConverter_CalculatesEveryPositionMode(
        LedCorner corner,
        string mode,
        double expected)
    {
        var converter = new LedPositionConverter();

        var result = converter.Convert(
            [corner, 100, 20, 5, 0.6],
            typeof(double),
            mode,
            CultureInfo.InvariantCulture);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void LedPositionConverter_HandlesInvalidAndAlternateNumericInputs()
    {
        var converter = new LedPositionConverter();

        Assert.Equal(
            0d,
            converter.Convert([], typeof(double), null!, CultureInfo.InvariantCulture));
        Assert.Equal(
            85d,
            converter.Convert(
                [new object(), 100f, "20", 5, -1d],
                typeof(double),
                "InnerLeft",
                CultureInfo.InvariantCulture));
        Assert.Equal(
            5d,
            converter.Convert(
                [LedCorner.TopLeft, 100, 20, 5, 2d],
                typeof(double),
                "InnerLeft",
                CultureInfo.InvariantCulture));
        Assert.Throws<NotSupportedException>(
            () => converter.ConvertBack(
                0,
                [],
                null!,
                CultureInfo.InvariantCulture));
    }

    [Theory]
    [InlineData(Language.English, "en-US")]
    [InlineData(Language.Korean, "ko-KR")]
    [InlineData(Language.Chinese, "zh-CN")]
    [InlineData(Language.Vietnamese, "vi-VN")]
    public void LanguageConverter_RoundTripsEverySupportedLanguage(
        Language language,
        string tag)
    {
        var converter = new LanguageToXmlLanguageConverter();

        var xml = Assert.IsType<XmlLanguage>(
            converter.Convert(language, typeof(XmlLanguage), null!, CultureInfo.InvariantCulture));

        Assert.Equal(tag, xml.IetfLanguageTag, ignoreCase: true);
        Assert.Equal(
            language,
            converter.ConvertBack(xml, typeof(Language), null!, CultureInfo.InvariantCulture));
    }

    [Fact]
    public void LanguageConverter_DefaultsUnsupportedValuesToKorean()
    {
        var converter = new LanguageToXmlLanguageConverter();

        Assert.Equal(
            "ko-KR",
            Assert.IsType<XmlLanguage>(
                converter.Convert(null!, typeof(XmlLanguage), null!, CultureInfo.InvariantCulture))
                .IetfLanguageTag,
            ignoreCase: true);
        Assert.Equal(
            Language.Korean,
            converter.ConvertBack("invalid", typeof(Language), null!, CultureInfo.InvariantCulture));
    }

    [Theory]
    [InlineData("", 0, 0)]
    [InlineData("2:30", 2, 30)]
    [InlineData("-2:15", 0, 15)]
    [InlineData("2:99", 0, 0)]
    [InlineData("3", 3, 0)]
    [InlineData("-3", 0, 0)]
    [InlineData("125", 1, 25)]
    [InlineData("199", 1, 39)]
    [InlineData("invalid", 0, 0)]
    public void TimeSpanConverter_ParsesSupportedAndInvalidEditForms(
        string text,
        int hours,
        int minutes)
    {
        var converter = new TimeSpanHmEditConverter();

        Assert.Equal(
            new TimeSpan(hours, minutes, 0),
            converter.ConvertBack(text, typeof(TimeSpan), null!, CultureInfo.InvariantCulture));
    }

    [Theory]
    [InlineData(5d, "> 4", true)]
    [InlineData(5d, ">= 5", true)]
    [InlineData(5d, "< 4", false)]
    [InlineData(5d, "<= 5", true)]
    [InlineData(5d, "== 5", true)]
    [InlineData(5d, "!= 5", false)]
    [InlineData(5d, "> 10 || >= 5 && < 6", true)]
    [InlineData(5d, "bad", false)]
    [InlineData(5d, "?? 5", false)]
    [InlineData(5d, "> bad", false)]
    public void NumberComparisonConverter_EvaluatesOperatorsAndGroups(
        double value,
        string expression,
        bool expected)
    {
        var converter = new NumberComparisonConverter();

        Assert.Equal(
            expected,
            converter.Convert(value, typeof(bool), expression, CultureInfo.InvariantCulture));
    }
}
