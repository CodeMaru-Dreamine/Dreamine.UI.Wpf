namespace Dreamine.UI.Wpf.Tests;

public sealed class ConverterTests
{
	private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

	[Theory]
	[InlineData("1", true)]
	[InlineData("0", false)]
	[InlineData(null, false)]
	public void BooleanConverter_converts_string_flags(string? value, bool expected)
	{
		var converter = new BooleanConverter();

		var result = converter.Convert(value!, typeof(bool), null!, Culture);

		Assert.Equal(expected, result);
		Assert.Equal(expected ? "1" : "0", converter.ConvertBack(expected, typeof(string), null!, Culture));
	}

	[Fact]
	public void BoolToVisibilityConverter_supports_inverse_and_hidden_options()
	{
		var converter = new BoolToVisibilityConverter();

		Assert.Equal(Visibility.Visible, converter.Convert(true, typeof(Visibility), null!, Culture));
		Assert.Equal(Visibility.Collapsed, converter.Convert(false, typeof(Visibility), null!, Culture));
		Assert.Equal(Visibility.Hidden, converter.Convert(false, typeof(Visibility), "Hidden", Culture));
		Assert.Equal(Visibility.Collapsed, converter.Convert(true, typeof(Visibility), "Inverse", Culture));
		Assert.Equal(true, converter.ConvertBack(Visibility.Visible, typeof(bool), null!, Culture));
		Assert.Equal(false, converter.ConvertBack(Visibility.Collapsed, typeof(bool), null!, Culture));
	}

	[Fact]
	public void UppercaseConverter_uppercases_text_and_ignores_convert_back()
	{
		var converter = new UppercaseConverter();

		Assert.Equal(string.Empty, converter.Convert(null!, typeof(string), null!, Culture));
		Assert.Equal("ABC", converter.Convert("abc", typeof(string), null!, Culture));
		Assert.Same(Binding.DoNothing, converter.ConvertBack("ABC", typeof(string), null!, Culture));
	}

	[Theory]
	[InlineData(2, "5;10", 5)]
	[InlineData(12, "5;10", 10)]
	[InlineData(7, "5;10", 7)]
	public void NumberClampConverter_clamps_numeric_values(int value, string range, int expected)
	{
		var converter = new NumberClampConverter();

		Assert.Equal(expected, converter.Convert(value, typeof(int), range, Culture));
		Assert.Same(Binding.DoNothing, converter.ConvertBack(value, typeof(int), range, Culture));
	}

	[Fact]
	public void StringToDoubleConverter_handles_invariant_text_and_formatting()
	{
		var converter = new StringToDoubleConverter { Fallback = -1, Format = "0.0" };

		Assert.Equal(12.5d, converter.Convert("12,5", typeof(double), null!, Culture));
		Assert.Equal(-1d, converter.Convert("not-number", typeof(double), null!, Culture));
		Assert.Equal("3.1", converter.ConvertBack(3.14d, typeof(string), null!, Culture));
	}

	[Fact]
	public void NullToVisibilityConverter_maps_null_state_and_rejects_reverse_conversion()
	{
		var converter = new NullToVisibilityConverter();

		Assert.Equal(Visibility.Collapsed, converter.Convert(null!, typeof(Visibility), null!, Culture));
		Assert.Equal(Visibility.Visible, converter.Convert(new object(), typeof(Visibility), null!, Culture));
		converter.Inverse = true;
		Assert.Equal(Visibility.Visible, converter.Convert(null!, typeof(Visibility), null!, Culture));
		Assert.Throws<NotSupportedException>(() => converter.ConvertBack(Visibility.Visible, typeof(object), null!, Culture));
	}

	[Fact]
	public void BooleanNegationConverter_negates_only_boolean_values()
	{
		var converter = new BooleanNegationConverter();

		Assert.Equal(false, converter.Convert(true, typeof(bool), null!, Culture));
		Assert.Equal(true, converter.ConvertBack(false, typeof(bool), null!, Culture));
		Assert.Equal("x", converter.Convert("x", typeof(string), null!, Culture));
	}
}
