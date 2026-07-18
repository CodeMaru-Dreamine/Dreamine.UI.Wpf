using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>부울 상태를 구성된 켜짐 또는 꺼짐 브러시로 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts a Boolean state to configured on or off brushes.</para>
	/// \endif
	/// </summary>
	public class BooleanToBrushConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>값이 참일 때 반환할 브러시를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the brush returned for a true value.</para>
		/// \endif
		/// </summary>
		public Brush OnBrush { get; set; } = Brushes.Lime;

		/// <summary>
		/// \if KO
		/// <para>값이 거짓일 때 반환할 브러시를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the brush returned for a false value.</para>
		/// \endif
		/// </summary>
		public Brush OffBrush { get; set; } = Brushes.Gray;

		/// <summary>
		/// \if KO
		/// <para>부울 원본 값을 해당 상태 브러시로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a Boolean source value to its corresponding state brush.</para>
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
		/// <para>참이면 <see cref="OnBrush"/>, 아니면 <see cref="OffBrush"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see cref="OnBrush"/> for true; otherwise, <see cref="OffBrush"/>.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = false;
			if (value is bool b)
				flag = b;
			return flag ? OnBrush : OffBrush;
		}

		/// <summary>
		/// \if KO
		/// <para>역변환을 수행하지 않고 바인딩 갱신을 건너뜁니다.</para>
		/// \endif
		/// \if EN
		/// <para>Skips the binding update instead of performing reverse conversion.</para>
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
		/// <para>항상 <see cref="Binding.DoNothing"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Always <see cref="Binding.DoNothing"/>.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}

	/// <summary>
	/// \if KO
	/// <para>부울 상태를 브러시로 변환하거나 WPF 기본값 우선 순위를 복원합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts a Boolean state to a brush or restores WPF default-value precedence.</para>
	/// \endif
	/// </summary>
	public class BrushOrUnsetConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>켜짐 상태의 브러시를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the brush for the on state.</para>
		/// \endif
		/// </summary>
		public Brush? OnBrush { get; set; } = new BrushConverter().ConvertFromString("#A2DED0") as Brush ?? Brushes.Transparent;

		/// <summary>
		/// \if KO
		/// <para>꺼짐 상태의 선택적 브러시를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets the optional brush for the off state.</para>
		/// \endif
		/// </summary>
		public Brush? OffBrush { get; set; } = null;

		/// <summary>
		/// \if KO
		/// <para>꺼짐 상태에서 <see cref="DependencyProperty.UnsetValue"/>를 반환할지 여부를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets whether the off state returns <see cref="DependencyProperty.UnsetValue"/>.</para>
		/// \endif
		/// </summary>
		public bool UseUnsetWhenFalse { get; set; } = true;

		/// <summary>
		/// \if KO
		/// <para>부울 상태를 구성된 브러시 또는 설정되지 않음 표식으로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a Boolean state to a configured brush or the unset-value sentinel.</para>
		/// \endif
		/// </summary>
		/// <param name="v">
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
		/// <para>상태 브러시 또는 <see cref="DependencyProperty.UnsetValue"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>A state brush or <see cref="DependencyProperty.UnsetValue"/>.</para>
		/// \endif
		/// </returns>
		public object Convert(object v, Type t, object p, CultureInfo c)
		{
			bool isOn = v is bool b && b;

			if (isOn)
				return OnBrush ?? DependencyProperty.UnsetValue;

			if (!UseUnsetWhenFalse && OffBrush != null)
				return OffBrush;

			// 기본색(스타일/템플릿 기본값)로 복귀
			return DependencyProperty.UnsetValue;
		}

		/// <summary>
		/// \if KO
		/// <para>역변환을 수행하지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>Does not perform reverse conversion.</para>
		/// \endif
		/// </summary>
		/// <param name="v">
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
		/// <para>항상 <see cref="Binding.DoNothing"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Always <see cref="Binding.DoNothing"/>.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object v, Type t, object p, CultureInfo c) => Binding.DoNothing;
	}
}
